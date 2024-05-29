using Microsoft.AspNetCore.Mvc;
using PicPayBackendChallenge.Dtos;
using PicPayBackendChallenge.Models;
using PicPayBackendChallenge.Services;

namespace PicPayBackendChallenge.Controllers;

[Route("[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    private ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }
    
    [HttpPost]
    public async Task<ActionResult<Transaction>> NewTransaction([FromBody] Transaction transaction)
    {
        try
        {
            var walletCreated = await _transactionService.CreateTransaction(transaction);
            return Ok(walletCreated);

        }
        catch (Exception e)
        {
            return BadRequest("Failed during transaction: " + e.Message);
        }
    }
    
}