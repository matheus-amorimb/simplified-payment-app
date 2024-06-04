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
        services.AddScoped<ITransactionAuthService, TransactionAuthService>();

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

    public static IServiceCollection AddHttpClients(this IServiceCollection services)
    {
        services.AddHttpClient("TransactionAuthService", client =>
        {
            client.BaseAddress = new Uri("https://util.devi.tools/api/v2/");
        });

        return services;
    }
}


