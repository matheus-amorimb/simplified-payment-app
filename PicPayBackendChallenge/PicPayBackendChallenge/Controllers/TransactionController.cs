using Microsoft.AspNetCore.Mvc;
using PicPayBackendChallenge.Dtos;

namespace PicPayBackendChallenge.Controllers;

[Route("[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{

    [HttpPost]
    public async Task<ActionResult<TransactionCreateDto>> NewTransaction([FromBody] TransactionCreateDto transactionCreateDto)
    {
        
    }
    
}