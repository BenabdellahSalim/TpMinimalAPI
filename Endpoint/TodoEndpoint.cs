using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TpMinimalAPI.Data.Models;
using TpMinimalAPI.DTO;
using TpMinimalAPI.Services;

namespace TpMinimalAPI.Endpoint
{
    public static class TodoEndpoint
    {
        public static IServiceCollection AddTodoServices(this IServiceCollection services)
        {
            services.AddScoped<ITodoService, EfcoreTodoService>();
            return services;
        }
        public static WebApplication MapTodoListEndpoint(this WebApplication App)
        {
            var groupMap = App.MapGroup("/todos").WithTags("TodosManagement");


            groupMap.MapGet("", GetAllAsync)
                .Produces(200);

            groupMap.MapGet("/{id:int}", GetByIdAsync)
                .Produces(404)
                .Produces(200);

            groupMap.MapGet("/active", GetActiveasync);

            groupMap.MapPost("", PostAsync)
                .Produces(400)
                .Produces(200);

            groupMap.MapDelete("/{id:int}", DeleteAsync)
                .Produces(204)
                .Produces(404);

            groupMap.MapPut("/{id:int}", UpdateAsync)
                .Produces(400)
                .Produces(200)
                .Produces(404);


            return App;
        }

        public static async Task<IResult> GetAllAsync(
                [FromServices] ITodoService service,
                [FromServices] AuthService auth,
                 HttpContext httpContext)
        {
            var userId = await auth.GetUserIdFromToken(httpContext);
            if (!userId.HasValue) return Results.Unauthorized();

            return Results.Ok(await service.GetAll(userId.Value));
        }
        public static async Task<IResult> GetByIdAsync(
                [FromRoute] int id,
                [FromServices] ITodoService service,
                [FromServices] AuthService auth,
                 HttpContext httpContext)
        {
            var userId = await auth.GetUserIdFromToken(httpContext);
            if (!userId.HasValue) return Results.Unauthorized();

            var todo = await service.GetById(id, userId.Value);

            if (todo is null) return Results.NotFound();
            return Results.Ok(todo);
        }
        public static async Task<IResult> GetActiveasync(
                [FromServices] ITodoService service,
                [FromServices] AuthService auth,
                 HttpContext httpContext)
        {
            var userId = await auth.GetUserIdFromToken(httpContext);
            if (!userId.HasValue) return Results.Unauthorized();

            return Results.Ok(await service.GetActive(userId.Value));
        }
        public static async Task<IResult> PostAsync(
                [FromBody] TodoInPut todo,
                [FromServices] ITodoService service,
                [FromServices] AuthService auth,
                [FromServices] IValidator<TodoInPut> validator,
                HttpContext httpContext)
        {
            var result = validator.Validate(todo);
            if (!result.IsValid) return Results.BadRequest(result.Errors);

            var userId = await auth.GetUserIdFromToken(httpContext);
            if (!userId.HasValue) return Results.Unauthorized();

            return Results.Ok(await service.Add(todo, userId.Value));
        }
        public static async Task<IResult> DeleteAsync(
                [FromRoute] int id,
                [FromServices] ITodoService service,
                [FromServices] AuthService auth,
                HttpContext httpContext)
        {
            var userId = await auth.GetUserIdFromToken(httpContext);
            if (!userId.HasValue) return Results.Unauthorized();

            var result = await service.Delete(id, userId.Value);
            if (result)
            {
                return Results.NoContent();
            }
            return Results.NotFound();
        }
        public static async Task<IResult> UpdateAsync(
                [FromRoute] int id,
                [FromBody] TodoInPut item,
                [FromServices] ITodoService service,
                [FromServices] AuthService auth,
                [FromServices] IValidator<TodoInPut> validator,
                HttpContext httpContext)
        {
            var validationResult = validator.Validate(item);
            if (!validationResult.IsValid) return Results.BadRequest(validationResult.Errors);

            if (id <= 0) return Results.BadRequest();

            var userId = await auth.GetUserIdFromToken(httpContext);
            if (!userId.HasValue) return Results.Unauthorized();

            var result = await service.Update(id, userId.Value, item);
            if (result) return Results.NoContent();
            return Results.NotFound();
        }

    }
}
