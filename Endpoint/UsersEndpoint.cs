using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using TpMinimalAPI.Data.Models;
using TpMinimalAPI.DTO;
using TpMinimalAPI.Services;

namespace TpMinimalAPI.Endpoint
{
    public static class UsersEndpoint
    {
        private const string tokenChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public static IServiceCollection AddUserServices(this IServiceCollection services)
        {
            return services;
        }

        public static RouteGroupBuilder MapUserEndpoints(this RouteGroupBuilder builder)
        {
            builder.MapPost("", Create)
                .WithTags("UserManagement")
                .Produces(400)
                .Produces<UsersOutPut>(200, "application/json");
            return builder;
        }

        private static async Task<IResult> Create(
            [FromBody] UsersInPut model,
            [FromServices] ApiDbContext context,
            [FromServices] IValidator<UsersInPut> validator)
        {
            var result = validator.Validate(model);
            if (!result.IsValid) return Results.BadRequest(result.Errors);

            var sb = new StringBuilder(16);
            for (int i = 0; i < 16; i++)
            {
                sb.Append(tokenChars[Random.Shared.Next(0, tokenChars.Length)]);
            }
            var user = new Users
            {
                Name = model.Name,
                Token = sb.ToString()
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return Results.Ok(new UsersOutPut(user.Id, user.Name, user.Token));
        }
    }
}
