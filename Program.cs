using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using TpMinimalAPI;
using TpMinimalAPI.Data.Models;
using TpMinimalAPI.DTO;
using TpMinimalAPI.Services;
using TpMinimalAPI.Endpoint;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder();

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<ApiMappingConfiguration>());

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services
    .AddDbContext<ApiDbContext>(options => options
    .UseSqlite(builder.Configuration
    .GetConnectionString("sqlite")));

builder.Services.AddScoped<ITodoService, EfcoreTodoService>();
builder.Services.AddScoped<IUsers, EfcoreTodoService>();

builder.Services.AddSwaggerGen();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

await app.Services.CreateScope().ServiceProvider
    .GetRequiredService<ApiDbContext>().Database
    .MigrateAsync();


app.UseSwagger();
app.UseSwaggerUI();
app.MapUserEndpoint();
app.MapTodoListEndpoint();

app.Run();