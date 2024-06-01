using AutoMapper;
using SimplifiedPicPay.Dtos;
using SimplifiedPicPay.Models;

namespace SimplifiedPicPay.Mappings;

public class TransactionMappingProfile : Profile
{
    public TransactionMappingProfile()
    {
        CreateMap<Transaction, TransactionRequestDto>().ReverseMap();
        CreateMap<Transaction, TransactionResponseDto>().ReverseMap();
    }

}