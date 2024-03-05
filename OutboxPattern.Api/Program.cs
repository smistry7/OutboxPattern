using Amazon.DynamoDBv2;
using Amazon.Runtime.SharedInterfaces;
using Amazon.SQS;
using LocalStack.Client.Extensions;
using OutboxPattern.HmrcClient;
using OutboxPattern.Repository;
using OutboxPattern.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddSingleton<OutboxRepository>()
    .AddTransient<IHmrcClient, HmrcClient>()
    .AddTransient<EventService>();


var app = builder.Build();

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
