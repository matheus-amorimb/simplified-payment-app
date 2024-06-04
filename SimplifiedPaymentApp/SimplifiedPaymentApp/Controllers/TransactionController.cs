using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimplifiedPicPay.Dtos;
using SimplifiedPicPay.Models;
using SimplifiedPicPay.Services;

namespace SimplifiedPicPay.Controllers;

[Route("v1/matheuspay")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;
    private readonly IWalletService _walletService;
    private readonly IMapper _mapper;

    public TransactionController(ITransactionService transactionService, IMapper mapper, IWalletService walletService)
    {
        _transactionService = transactionService;
        _mapper = mapper;
        _walletService = walletService;
    }

    /// <summary>
    /// Retrieves transactions associated with the authenticated client. 
    /// Administrators can view all transactions, while regular clients can only access their own transaction history.
    /// </summary>
    [Authorize]
    [HttpGet("user-transactions")]
    public async Task<ActionResult<IEnumerable<TransactionResponseDto>>> GetTransactionsByClient()
    {;
        var userIdClaim = User.FindFirst("id");
        
        if (userIdClaim == null)
        {
            return Unauthorized();
        }

        var userIsAdmin = User.Claims.Any(claim => claim.Value.ToLower() == "admin");

        if (userIsAdmin)
        {
            IEnumerable<Transaction> allTransactions = await _transactionService.GetAllTransactions();
            IEnumerable<TransactionResponseDto> allTransactionsResponseDto = _mapper.Map<IEnumerable<TransactionResponseDto>>(allTransactions);
            return Ok(allTransactionsResponseDto);
        }
        
        var userWallet = await _walletService.GetWalletByUserId(Guid.Parse(userIdClaim.Value));
        
        IEnumerable<Transaction> transactions = await _transactionService.GetTransactionsByWallet(userWallet.WalletId);
        IEnumerable<TransactionResponseDto> transactionResponseDto = _mapper.Map<IEnumerable<TransactionResponseDto>>(transactions);
        return Ok(transactionResponseDto);
    }
    
    /// <summary>
    /// Retrieves transactions associated with the specified wallet. [Admin Only]
    /// </summary>
    [Authorize(Policy = "Admin")]
    [HttpGet("transactions/{wallet-Id}")]
    public async Task<ActionResult<IEnumerable<TransactionResponseDto>>> GetTransactionsByClient([FromRoute(Name = "wallet-Id")] Guid walletId)
    {;
        IEnumerable<Transaction> transactions = await _transactionService.GetTransactionsByWallet(walletId);
        IEnumerable<TransactionResponseDto> transactionResponseDto = _mapper.Map<IEnumerable<TransactionResponseDto>>(transactions);
        return Ok(transactionResponseDto);
    }
    
    /// <summary>
    /// Creates a new transaction. [Users and Admin Only]
    /// </summary>
    [Authorize(Policy = "UserOrAdmin")]
    [HttpPost("transaction")]
    public async Task<ActionResult<TransactionResponseDto>> NewTransaction([FromBody] TransactionUserRequestDto transactionUserRequestDto)
    {
        if (!ModelState.IsValid)
        {
            var errorMessage = ModelState.FirstOrDefault().Value?.Errors.FirstOrDefault()?.ErrorMessage;
            throw new BadHttpRequestException(errorMessage);
        }

        var userIdClaim = User.FindFirst("id");
        
        if (userIdClaim == null)
        {
            return Unauthorized();
        }
        
        var userWallet = await _walletService.GetWalletByUserId(Guid.Parse(userIdClaim.Value));
        TransactionRequestDto transactionRequestDto = _mapper.Map<TransactionRequestDto>(transactionUserRequestDto);
        transactionRequestDto.PayerWalletId = userWallet.WalletId;
        
        Transaction transaction = _mapper.Map<Transaction>(transactionRequestDto);
        
        var transactionCreated = await _transactionService.CreateTransaction(transaction);
        TransactionResponseDto transactionResponseDto = _mapper.Map<TransactionResponseDto>(transaction);
        return CreatedAtAction(nameof(NewTransaction), "transaction", transactionResponseDto);
    }
    
}