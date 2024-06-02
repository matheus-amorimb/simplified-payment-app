using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SimplifiedPicPay.Dtos;
using SimplifiedPicPay.Models;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace SimplifiedPicPay.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly IWalletService _walletService;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<User> userManager, IMapper mapper, IWalletService walletService, IConfiguration configuration, ITokenService tokenService)
    {
        _userManager = userManager;
        _mapper = mapper;
        _walletService = walletService;
        _configuration = configuration;
        _tokenService = tokenService;
    }
    
    public async Task<UserRegisterRequestDto> Register(UserRegisterRequestDto userRegisterRequestDto)
    {
        ValidateCpfNotInUse(userRegisterRequestDto.Cpf);
        ValidateEmailNotInUse(userRegisterRequestDto.Email);

        User newUser = _mapper.Map<User>(userRegisterRequestDto);
        newUser.UserName = newUser.Email;
        var result = await _userManager.CreateAsync(newUser, userRegisterRequestDto.Password);
        
        if (!result.Succeeded)
        {
            string errors = string.Join(" ", result.Errors.Select(e => e.Description));
            throw new BadHttpRequestException(errors);
        }

        WalletRequestDto walletRequestDto = _mapper.Map<WalletRequestDto>(userRegisterRequestDto);
        walletRequestDto.UserId = newUser.Id;
        walletRequestDto.FullName = $"{userRegisterRequestDto.FirstName} {userRegisterRequestDto.LastName}";

        await _walletService.CreateWallet(walletRequestDto);
        
        return userRegisterRequestDto;
    }
    
    public async Task<UserLoginResponseDto> LogIn(UserLoginRequestDto userLoginRequestDto)
    {
        User user = await _userManager.FindByNameAsync(userLoginRequestDto.Email);
        
        ValdiateUserName(user);
        
        await ValdiateUserPassword(user, userLoginRequestDto.Password);

        var authClaims = await GenerateAuthClaims(user);

        var token = _tokenService.GenerateAccessToken(authClaims);
        
        var refreshToken = _tokenService.GenerateRefreshToken();

        await UpdateUserRefreshToken(user, refreshToken);
        
        return new UserLoginResponseDto()
        {
            Status = "Logged successfully",
            Token = token,
            RefreshToken = refreshToken,
            Expiration = DateTime.Now
        };
    }

    private async Task UpdateUserRefreshToken(User user, string refreshToken)
    {
        user.RefreshToken = refreshToken;
        Int32.TryParse(_configuration["JWT:RefreshTokenValidityInMinutes"], out int refreshTokenValidityInMinutes);
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(refreshTokenValidityInMinutes);
        await _userManager.UpdateAsync(user);
    }

    private async Task<IEnumerable<Claim>> GenerateAuthClaims(User user)
    {
        IEnumerable<string> userRoles = await _userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>()
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("id", user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        return authClaims;

    }

    private async Task ValdiateUserPassword(User user, string password)
    {
        if (!(await _userManager.CheckPasswordAsync(user, password)))
        {
            throw new BadHttpRequestException("Password is incorrect");
        }
    }

    private void ValdiateUserName(User user)
    {
        if (user is null)
        {
            throw new BadHttpRequestException("User does not exist.");
        }
    }

    private void ValidateEmailNotInUse(string? email)
    {
        if (_userManager.Users.Any(u => u.Email == email))
        {
            throw new BadHttpRequestException("Email already in use");
        }
    }

    private void ValidateCpfNotInUse(string? cpf)
    {
        if (_userManager.Users.Any(u => u.Cpf == cpf))
        {
            throw new BadHttpRequestException("CPF already in use");
        }
    }
}