using Microsoft.EntityFrameworkCore;
using PicPayBackendChallenge;
using PicPayBackendChallenge.Context;
using PicPayBackendChallenge.Mappings;
using PicPayBackendChallenge.Repositories.Implementations;
using PicPayBackendChallenge.Repositories.Interfaces;
using PicPayBackendChallenge.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string postgresConnection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
{
    optionsBuilder.UseNpgsql(postgresConnection);
});
builder.Services.AddScoped<IWalletRepository, WalletRepository>();
builder.Services.AddScoped<IWalletService, WalletService>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

builder.Services.AddAutoMapper(typeof(WalletMappingProfile));
builder.Services.AddControllers();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseExceptionHandler(_ => {});
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
