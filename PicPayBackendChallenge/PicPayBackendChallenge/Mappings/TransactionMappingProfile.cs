using AutoMapper;
using PicPayBackendChallenge.Dtos;
using PicPayBackendChallenge.Models;

namespace PicPayBackendChallenge.Mappings;

public class TransactionMappingProfile : Profile
{
    public TransactionMappingProfile()
    {
        CreateMap<Transaction, TransactionRequestDto>().ReverseMap();
        CreateMap<Transaction, TransactionResponseDto>().ReverseMap();
    }

}