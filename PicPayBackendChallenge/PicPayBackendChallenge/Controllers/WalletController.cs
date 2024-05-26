using Microsoft.AspNetCore.Mvc;
using PicPayBackendChallenge.Models;
using PicPayBackendChallenge.Services;

namespace PicPayBackendChallenge.Controllers;

[Route("/[controller]")]
[ApiController]
public class WalletController : ControllerBase
{
    private readonly IWalletService _walletService;

    public WalletController(IWalletService walletService)
    {
        _walletService = walletService;
    }
    
    [HttpPost]
    public async Task<ActionResult<Wallet>> CreateWallet([FromBody] Wallet wallet)
    {
        var walletCreated = await _walletService.CreateWallet(wallet);
        
        return Ok(wallet);
    }
}