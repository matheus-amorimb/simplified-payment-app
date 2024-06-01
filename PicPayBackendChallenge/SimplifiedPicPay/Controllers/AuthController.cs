using Microsoft.AspNetCore.Mvc;
using SimplifiedPicPay.Dtos;

namespace SimplifiedPicPay.Controllers;

[Controller]
[Route("v1/picpay")]
public class AuthController : ControllerBase
{
    [HttpPost]
    public ActionResult RegisterNewUser([FromBody] UserRegisterRequestDto userRegisterRequestDto)
    {
        if (!ModelState.IsValid)
        {
            var errorMessage = ModelState.FirstOrDefault().Value?.Errors.FirstOrDefault()?.ErrorMessage;
            throw new BadHttpRequestException(errorMessage);
        }

        return Ok(userRegisterRequestDto);
    }
}