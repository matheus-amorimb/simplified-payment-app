using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimplifiedPicPay.Dtos;
using SimplifiedPicPay.Models;
using SimplifiedPicPay.Services;

namespace SimplifiedPicPay.Controllers;

[Route("v1/picpay")]
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

    [HttpGet("transactions/{client-Id}")]
    public async Task<ActionResult<IEnumerable<TransactionResponseDto>>> GetTransactionsByClient([FromRoute(Name = "client-Id")] Guid clientId)
    {;
        IEnumerable<Transaction> transactions = await _transactionService.GetTransactionsByClient(clientId);
        IEnumerable<TransactionResponseDto> transactionResponseDto = _mapper.Map<IEnumerable<TransactionResponseDto>>(transactions);
        return Ok(transactionResponseDto);
    }
    
    [HttpPost("transaction")]
    [Authorize(Policy = "User")]
    public async Task<ActionResult<TransactionResponseDto>> NewTransaction([FromBody] TransactionRequestDto transactionRequestDto)
    {
        if (!ModelState.IsValid)
        {
            var errorMessage = ModelState.FirstOrDefault().Value?.Errors.FirstOrDefault()?.ErrorMessage;
            throw new BadHttpRequestException(errorMessage);
        }

        Transaction transaction = _mapper.Map<Transaction>(transactionRequestDto);
        var transactionCreated = await _transactionService.CreateTransaction(transaction);
        TransactionResponseDto transactionResponseDto = _mapper.Map<TransactionResponseDto>(transaction);
        return Ok(transactionResponseDto);

    }
    
}