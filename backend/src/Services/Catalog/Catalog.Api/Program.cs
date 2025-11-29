using AmigurumiStore.Catalog.Application.Commands;
using AmigurumiStore.Catalog.Application.Data;
using AmigurumiStore.Catalog.Application.Dtos;
using AmigurumiStore.Catalog.Application.Extensions;
using AmigurumiStore.Catalog.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCatalogApplication(builder.Configuration.GetConnectionString("CatalogDb")!);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/api/products", async ([FromServices] IMediator mediator, CancellationToken ct) =>
    Results.Ok(await mediator.Send(new GetProductsQuery(), ct)));

app.MapGet("/api/products/{id:guid}", async (Guid id, [FromServices] IMediator mediator, CancellationToken ct) =>
{
    var product = await mediator.Send(new GetProductByIdQuery(id), ct);
    return product is null ? Results.NotFound() : Results.Ok(product);
});

app.MapPost("/api/products", async ([FromBody] UpsertProductDto dto, [FromServices] IMediator mediator, CancellationToken ct) =>
{
    var created = await mediator.Send(new CreateProductCommand(dto), ct);
    return Results.Created($"/api/products/{created.Id}", created);
});

app.MapPut("/api/products/{id:guid}", async (Guid id, [FromBody] UpsertProductDto dto, [FromServices] IMediator mediator, CancellationToken ct) =>
{
    await mediator.Send(new UpdateProductCommand(id, dto), ct);
    return Results.NoContent();
});

app.MapDelete("/api/products/{id:guid}", async (Guid id, [FromServices] IMediator mediator, CancellationToken ct) =>
{
    await mediator.Send(new DeleteProductCommand(id), ct);
    return Results.NoContent();
});

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
    await CatalogSeeder.SeedAsync(db);
}

app.Run();
