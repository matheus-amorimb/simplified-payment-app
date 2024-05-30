using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PicPayBackendChallenge.Dtos;
using PicPayBackendChallenge.Models;
using PicPayBackendChallenge.Services;

namespace PicPayBackendChallenge.Controllers;

[Route("[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;
    private readonly IMapper _mapper;

    public TransactionController(ITransactionService transactionService, IMapper mapper)
    {
        _transactionService = transactionService;
        _mapper = mapper;
    }
    
    [HttpPost]
    public async Task<ActionResult<TransactionResponseDto>> NewTransaction([FromBody] TransactionRequestDto transactionRequestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            Transaction transaction = _mapper.Map<Transaction>(transactionRequestDto);
            var transactionCreated = await _transactionService.CreateTransaction(transaction);
            TransactionResponseDto transactionResponseDto = _mapper.Map<TransactionResponseDto>(transaction);
            return Ok(transactionResponseDto);

        }
        catch (Exception e)
        {
            return BadRequest("Failed during transaction: " + e.Message);
        }
    }
    
}