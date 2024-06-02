using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SimplifiedPicPay.Dtos;
using SimplifiedPicPay.Models;

namespace SimplifiedPicPay.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public AuthService(UserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<UserRegisterRequestDto> Register(UserRegisterRequestDto userRegisterRequestDto)
    {
        ValidateCpfNotInUse(userRegisterRequestDto.Cpf);
        ValidateEmailNotInUse(userRegisterRequestDto.Email);

        var newUser = _mapper.Map<User>(userRegisterRequestDto);
        newUser.UserName = newUser.Email;
        var result = await _userManager.CreateAsync(newUser, userRegisterRequestDto.Password);
        
        if (!result.Succeeded)
        {
            string errors = string.Join(" ", result.Errors.Select(e => e.Description));
            throw new BadHttpRequestException(errors);
        }
        
        return userRegisterRequestDto;
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