using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RatingsWebApi;
using RatingsWebApi.Data;
using RatingsWebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(gen =>
{
    gen.DocumentFilter<CustomSwaggerFilter>();
    gen.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

builder.Services.AddAWSLambdaHosting(LambdaEventSource.RestApi);
builder.Services.AddScoped<RatingService>();
//builder.Services.AddDbContext<RatingsAPIDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Get the encrypted connection string from appsettings.json
var secretsHelper = new SecretsManagerHelper();
var secretJson = await secretsHelper.GetSecretAsync();
var encryptedConnectionString = secretJson["connectionString"];

// Decrypt the connection string
var decryptor = new StringDecryptor("Group6CampersitePassword");
var decryptedConnectionString = decryptor.Decrypt(encryptedConnectionString);

// Configure the DbContext with the decrypted connection string 
builder.Services.AddDbContext<RatingsAPIDbContext>(options =>
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

app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();
