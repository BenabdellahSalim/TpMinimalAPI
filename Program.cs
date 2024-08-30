using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using TpMinimalAPI;
using TpMinimalAPI.DTO;
using TpMinimalAPI.Services;
using TpMinimalAPI.Endpoint;
using Microsoft.Extensions.Hosting;
using TpMinimalAPI.Data;

var builder = WebApplication.CreateBuilder();

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<ApiMappingConfiguration>());



builder.Services.AddTodoServices(); 
builder.Services.AddUserServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<AuthService>();
//builder.Services
//.AddDbContext<ApiDbContext>(options => options
//.UseSqlite(builder.Configuration
//.GetConnectionString("sqlite")));
//.UseSqlServer(builder.Configuration.GetConnectionString(""))


var connectionString = builder.Configuration.GetConnectionString("SqlServer");
builder.Services.AddDbContext<ApiDbContext>(options => options.UseSqlServer(connectionString));


builder.Services.AddValidatorsFromAssemblyContaining<Program>();
 

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.Services
.CreateScope().ServiceProvider
.GetRequiredService<ApiDbContext>().Database
.EnsureCreated();

app.MapGroup("/users").MapUserEndpoints();
app.MapTodoListEndpoint();

app.Run();