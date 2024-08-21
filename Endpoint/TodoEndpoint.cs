using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TpMinimalAPI.Data.Models;
using TpMinimalAPI.DTO;
using TpMinimalAPI.Services;

namespace TpMinimalAPI.Endpoint
{
    public static class TodoEndpoint
    {
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
                [FromServices] ITodoService services)
        {
            var todo = await services.GetAll();
            return Results.Ok(todo);
        }
        public static async Task<IResult> GetByIdAsync(
                [FromRoute] int id,
                [FromServices] ITodoService services)
        {
            var todo = await services.GetById(id);
            if (todo is null) return Results.NotFound();
            return Results.Ok(services.GetById(id));
        }
        public static async Task<IResult> GetActiveasync(
                [FromServices] ITodoService services)
        {
            var result = await services.GetActive();
            
            return Results.Ok(result);
        }
        public static async Task<IResult> PostAsync(
                [FromBody] TodoInPut todo,
                [FromServices] IValidator<TodoInPut> validator,
                [FromServices] ITodoService services)

        {
            if (!validator.Validate(todo).IsValid) return Results.BadRequest();
            var result = await services.Add(todo);
            return Results.Ok(result);
        }
        public static async Task<IResult> DeleteAsync(
                [FromRoute] int id,
                [FromServices] ITodoService services)
        {
            var result = await services.Delete(id);
            if (result) return Results.NoContent();
            return Results.NotFound();
        }
        public static async Task<IResult> UpdateAsync(
                [FromRoute] int id,
                [FromBody] TodoInPut todo,
                [FromServices] ITodoService services,
                [FromServices] IValidator<TodoInPut> validator)
        {
            if (!validator.Validate(todo).IsValid)
            {
                return Results.BadRequest(validator.Validate(todo).Errors.Select(e => new
                {
                    Message = e.ErrorMessage,
                    e.PropertyName
                }));
            }
            var result = await services.Update(id, todo);
            if (result) return Results.Ok(todo);
            return Results.NotFound();
        }

    }
}
