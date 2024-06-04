using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimplifiedPicPay.Dtos;
using SimplifiedPicPay.Models;
using SimplifiedPicPay.Services;

namespace SimplifiedPicPay.Controllers;

[Route("v1/matheuspay")]
[ApiController]
public class WalletController : ControllerBase
{
    private readonly IWalletService _walletService;
    private readonly IMapper _mapper;

    public WalletController(IWalletService walletService, IMapper mapper)
    {
        _walletService = walletService;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves all wallets. [Admin Only]
    /// </summary>
    [Authorize(Policy = "AdminOnly")]
    [HttpGet("wallets")]
    public async Task<ActionResult<IEnumerable<WalletResponseDto>>> GetWallets()
    {
        var wallets = await _walletService.GetWallets();
        var walletsGetDto = _mapper.Map<IEnumerable<WalletResponseDto>>(wallets);
        return Ok(walletsGetDto);
    }
    
    /// <summary>
    /// Creates a new wallet. [Admin Only]
    /// </summary>
    [Authorize(Policy = "AdminOnly")]
    [HttpPost("wallet")]
    public async Task<ActionResult<WalletResponseDto>> CreateWallet([FromBody] WalletRequestDto walletRequestDto)
    {
        if (!ModelState.IsValid)
        {
            var errorMessage = ModelState.FirstOrDefault().Value?.Errors.FirstOrDefault()?.ErrorMessage;
            throw new BadHttpRequestException(errorMessage);
        }
        var walletCreated = await _walletService.CreateWallet(walletRequestDto);
        return Ok(walletCreated);
    }
}