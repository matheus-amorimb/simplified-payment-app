using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimplifiedPicPay.Dtos;
using SimplifiedPicPay.Models;
using SimplifiedPicPay.Services;

namespace SimplifiedPicPay.Controllers;

[Controller]
[Route("v1/picpay")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    private readonly IMapper _mapper;
    public AuthController(UserManager<User> userManager, IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }
    
    [HttpPost]
    [Route("register")]
    public async Task<ActionResult<UserRegisterResponseDto>> RegisterNewUser([FromBody] UserRegisterRequestDto userRegisterRequestDto)
    {
        if (!ModelState.IsValid)
        {
            var errorMessage = ModelState.FirstOrDefault().Value?.Errors.FirstOrDefault()?.ErrorMessage;
            throw new BadHttpRequestException(errorMessage);
        }

        var userCreated = await _authService.Register(userRegisterRequestDto);
        var responseDto = _mapper.Map<UserRegisterResponseDto>(userCreated);
        
        return Ok(responseDto);
    }
}