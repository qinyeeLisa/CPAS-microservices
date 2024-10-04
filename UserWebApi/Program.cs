using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UserWebApi;
using UserWebApi.Data;
using UserWebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAWSLambdaHosting(LambdaEventSource.RestApi);
builder.Services.AddScoped<UserService>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
//builder.Services.AddDbContext<UserAPIDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Get the encryption key from environment variable
//var encryptionKey = Environment.GetEnvironmentVariable("ENCRYPTION_KEY");
//if (string.IsNullOrEmpty(encryptionKey))
//{
//    throw new Exception("Encryption key not found. Please set the ENCRYPTION_KEY environment variable.");
//}

// Get the encrypted connection string from appsettings.json
var encryptedConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Decrypt the connection string
var decryptor = new StringDecryptor("Group6CampersitePassword");
var decryptedConnectionString = decryptor.Decrypt(encryptedConnectionString);

// Configure the DbContext with the decrypted connection string 
builder.Services.AddDbContext<UserAPIDbContext>(options =>
    options.UseSqlServer(decryptedConnectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials());

app.UseAuthorization();

app.MapGet("/api", () => "Test!");

app.MapControllers();

app.Run();
