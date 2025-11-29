using AmigurumiStore.Ordering.Application.Commands;
using AmigurumiStore.Ordering.Application.Data;
using AmigurumiStore.Ordering.Application.Dtos;
using AmigurumiStore.Ordering.Application.Extensions;
using AmigurumiStore.Ordering.Application.Queries;
using AmigurumiStore.Ordering.Api.Health;
using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOrderingApplication(builder.Configuration.GetConnectionString("OrderingDb")!);
builder.Services.AddProblemDetails(options =>
{
    options.Map<KeyNotFoundException>(ex => Results.Problem(title: "Not found", detail: ex.Message, statusCode: StatusCodes.Status404NotFound));
    options.Map<InvalidOperationException>(ex => Results.Problem(title: "Invalid operation", detail: ex.Message, statusCode: StatusCodes.Status400BadRequest));
    options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
});
builder.Services.AddExceptionHandler();
builder.Services.AddHealthChecks().AddCheck<DbHealthCheck>("ordering-db");
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.MapGet("/api/orders", async ([FromServices] IMediator mediator, CancellationToken ct) =>
    Results.Ok(await mediator.Send(new GetOrdersQuery(), ct)));

app.MapGet("/api/orders/{id:guid}", async (Guid id, [FromServices] IMediator mediator, CancellationToken ct) =>
{
    var order = await mediator.Send(new GetOrderByIdQuery(id), ct);
    return order is null ? Results.NotFound() : Results.Ok(order);
});

app.MapPost("/api/orders", async ([FromBody] CreateOrderDto request, [FromServices] IMediator mediator, CancellationToken ct) =>
{
    var created = await mediator.Send(new CreateOrderCommand(request), ct);
    return Results.Created($"/api/orders/{created.Id}", created);
});

app.MapPatch("/api/orders/{id:guid}/status", async (Guid id, UpdateStatusDto request, [FromServices] IMediator mediator, CancellationToken ct) =>
{
    await mediator.Send(new UpdateOrderStatusCommand(id, request.Status), ct);
    return Results.NoContent();
});

app.MapHealthChecks("/health");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<OrderingDbContext>();
    await db.Database.MigrateAsync();
}

app.Run();
