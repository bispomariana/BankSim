using Microsoft.EntityFrameworkCore;
using ProjetoDevTrail.Api.Middleware;
using ProjetoDevTrail.Application.Interfaces;
using ProjetoDevTrail.Application.Services;
using ProjetoDevTrail.Domain.Interfaces;
using ProjetoDevTrail.Infra.Data;
using ProjetoDevTrail.Infra.Repositories;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Repositories
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

// Services
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

//Context
builder.Services.AddDbContext<BankContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BankSimDataBase")));

var app = builder.Build();

//Middleware
app.UseMiddleware<HandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
