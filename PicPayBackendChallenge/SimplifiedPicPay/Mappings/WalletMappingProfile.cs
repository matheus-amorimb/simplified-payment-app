using AutoMapper;
using SimplifiedPicPay.Dtos;
using SimplifiedPicPay.Models;

namespace SimplifiedPicPay.Mappings;

public class WalletMappingProfile: Profile
{
    public WalletMappingProfile()
    {
        CreateMap<Wallet, WalletRequestDto>().ReverseMap();
        CreateMap<Wallet, WalletResponseDto>().ReverseMap();
    }
}