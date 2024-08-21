using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using TpMinimalAPI.Services;

namespace TpMinimalAPI.Endpoint
{
    public static class UsersEndpoint
    {
        public static WebApplication MapUserEndpoint(this WebApplication application)
        {
            var groupMap = application.MapGroup("/user").WithTags("UsersManagement");

            application.MapPost("", GetAcces)
                .WithTags("UserManager");

            return application;
        }
        public static async Task<IResult> GetAcces(
               [FromServices] IUsers services,
               [FromHeader] CancellationToken token)
        {
            await services.UsersAcces();
            return Results.Ok(token);
        }
    }
}
