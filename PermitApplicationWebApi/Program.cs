using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PermitApplicationWebApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAWSLambdaHosting(LambdaEventSource.RestApi); // will be ignored if run locally

// Dependency Injection of DbContext Class
builder.Services.AddDbContext<PermitAPIDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("/test", () => "Hello Biscuit!");

app.MapControllers();

app.Run();
