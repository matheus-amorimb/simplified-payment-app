using SimplifiedPicPay.Repositories.Implementations;
using SimplifiedPicPay.Repositories.Interfaces;
using SimplifiedPicPay.Services;

namespace SimplifiedPicPay.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IWalletService, WalletService>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }    
    
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IWalletRepository, WalletRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        return services;
    }
    
    public static IServiceCollection AddRabbitMqService(this IServiceCollection services, IConfiguration configuration)
    {
        var hostName = configuration["RabbitMQ:HostName"];
        services.AddSingleton<RabbitMqService>(_ => new RabbitMqService(hostName));
        
        return services;
    }
}


