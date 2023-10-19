using System.Reflection;
using Api.Middlewares;
using Application.Models.DTOs;
using FluentValidation.AspNetCore;
using Infrastructure.DependencyResolverService;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ExceptionMiddleware>();
builder.Services.AddAutoMapper(typeof(HumourDto));
builder.Services.AddInfrastructureServices();
builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(typeof(HumourDto).Assembly);
});

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => 
{
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
