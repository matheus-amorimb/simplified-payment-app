using AutoMapper;
using PicPayBackendChallenge.Dtos;
using PicPayBackendChallenge.Models;

namespace PicPayBackendChallenge.Mappings;

public class WalletMappingProfile: Profile
{
    public WalletMappingProfile()
    {
        CreateMap<Wallet, WalletRequestDto>().ReverseMap();
        CreateMap<Wallet, WalletResponseDto>().ReverseMap();
    }
}