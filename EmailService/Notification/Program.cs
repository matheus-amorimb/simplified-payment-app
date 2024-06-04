using Notification.Models;
using Notification.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IEmailService, EmailService>();
builder.Services.AddSingleton<TransactionConsumerService>();
builder.Services.AddHostedService<TransactionConsumerService>();
builder.Services.AddHostedService<WalletConsumerService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();


