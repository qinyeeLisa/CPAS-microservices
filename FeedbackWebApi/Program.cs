using Microsoft.EntityFrameworkCore;
using FeedbackWebApi.Data;
using FeedbackWebApi.Services;
using Microsoft.Extensions.Configuration;
using UserWebApi.Data;

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
builder.Services.AddScoped<FeedbackService>();
builder.Services.AddDbContext<FeedbackAPIDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
