using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimplifiedPicPay.Dtos;
using SimplifiedPicPay.Models;
using SimplifiedPicPay.Services;

namespace SimplifiedPicPay.Controllers;

[Route("v1/picpay")]
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

    [HttpGet("wallets")]
    public async Task<ActionResult<IEnumerable<WalletResponseDto>>> GetWallets()
    {
        var wallets = await _walletService.GetWallets();
        var walletsGetDto = _mapper.Map<IEnumerable<WalletResponseDto>>(wallets);
        return Ok(walletsGetDto);
    }
    
    [HttpPost("wallet")]
    public async Task<ActionResult<WalletResponseDto>> CreateWallet([FromBody] WalletRequestDto walletRequestDto)
    {
        if (!ModelState.IsValid)
        {
            var errorMessage = ModelState.FirstOrDefault().Value?.Errors.FirstOrDefault()?.ErrorMessage;
            throw new BadHttpRequestException(errorMessage);
        }
        Wallet wallet = _mapper.Map<Wallet>(walletRequestDto);
        var walletCreated = await _walletService.CreateWallet(wallet);
        WalletResponseDto walletResponseDto = _mapper.Map<WalletResponseDto>(walletCreated);
        return Ok(walletResponseDto);
    }
}