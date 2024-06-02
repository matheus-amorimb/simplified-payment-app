using AutoMapper;
using SimplifiedPicPay.Dtos;
using SimplifiedPicPay.Models;

namespace SimplifiedPicPay.Mappings;

public class AuthMappingProfile : Profile
{
    public AuthMappingProfile()
    {
        CreateMap<User, UserRegisterRequestDto>().ReverseMap();
    }
}