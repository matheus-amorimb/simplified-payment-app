using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PicPayBackendChallenge.Dtos;
using PicPayBackendChallenge.Models;
using PicPayBackendChallenge.Services;

namespace PicPayBackendChallenge.Controllers;

[Route("/[controller]")]
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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WalletResponseDto>>> GetWallets()
    {

        var wallets = await _walletService.GetWallets();
        var walletsGetDto = _mapper.Map<IEnumerable<WalletResponseDto>>(wallets);
        return Ok(walletsGetDto);
    }
    
    [HttpPost]
    public async Task<ActionResult<WalletResponseDto>> CreateWallet([FromBody] WalletRequestDto walletRequestDto)
    {
        try
        {
            Wallet wallet = _mapper.Map<Wallet>(walletRequestDto);
            var walletCreated = await _walletService.CreateWallet(wallet);
            WalletResponseDto walletResponseDto = _mapper.Map<WalletResponseDto>(walletCreated);
            return Ok(walletResponseDto);

        }
        catch (Exception e)
        {
            return BadRequest("Failed to create wallet: " + e.Message);
        }
    }
}