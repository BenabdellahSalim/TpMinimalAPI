using TpMinimalAPI;

var builder = WebApplication.CreateBuilder();



builder.Services.AddSingleton<TodoServices>();
var app = builder.Build();

app.MapGet("/get", () => "hi to web API");


app.MapGet("/Todo/activ", (TodoServices services) =>
{
    var todo = services;
    if (todo is not null) return Results.Ok(services.GetAll());
    return Results.NotFound();
});

app.MapPost("/Todo", (Todo doIt, TodoServices services) =>
{
    var result = services.TodoAdd(doIt.Title, doIt.DateStart, doIt.DateEnd);
    return Results.Ok(result);
});







app.Run();