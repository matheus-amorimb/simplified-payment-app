using SimplifiedPicPay.Mappings;

namespace SimplifiedPicPay.Extensions;

public static class AutoMapperExtensions
{
    public static IServiceCollection AddAutoMappers(this IServiceCollection service)
    {
        service.AddAutoMapper(typeof(WalletMappingProfile));
        service.AddAutoMapper(typeof(AuthMappingProfile));
        service.AddAutoMapper(typeof(TransactionMappingProfile));

        return service;
    }
}