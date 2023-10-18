using Application.Models.DTOs;
using FluentValidation.AspNetCore;
using Infrastructure.DependencyResolverService;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
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
builder.Services.AddSwaggerGen();

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
