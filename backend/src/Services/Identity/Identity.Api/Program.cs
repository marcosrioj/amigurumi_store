using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AmigurumiStore.Identity.Application.Commands;
using AmigurumiStore.Identity.Application.Data;
using AmigurumiStore.Identity.Application.Dtos;
using AmigurumiStore.Identity.Application.Extensions;
using AmigurumiStore.Identity.Application.Models;
using AmigurumiStore.Identity.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>()!;
builder.Services.AddIdentityApplication(
    builder.Configuration.GetConnectionString("IdentityDb")!,
    jwtOptions);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/api/auth/register", async ([FromBody] RegisterDto request, [FromServices] IMediator mediator, CancellationToken ct) =>
{
    try
    {
        var result = await mediator.Send(new RegisterUserCommand(request), ct);
        return Results.Ok(result);
    }
    catch (InvalidOperationException ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapPost("/api/auth/login", async ([FromBody] LoginDto request, [FromServices] IMediator mediator, CancellationToken ct) =>
{
    try
    {
        var result = await mediator.Send(new LoginUserCommand(request), ct);
        return Results.Ok(result);
    }
    catch (InvalidOperationException ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapGet("/api/auth/profile", async ([FromServices] IMediator mediator, ClaimsPrincipal principal, CancellationToken ct) =>
{
    var userId = principal.FindFirstValue(JwtRegisteredClaimNames.Sub);
    if (userId is null) return Results.Unauthorized();

    var profile = await mediator.Send(new AmigurumiStore.Identity.Application.Queries.GetProfileQuery(Guid.Parse(userId)), ct);
    return profile is null ? Results.NotFound() : Results.Ok(profile);
}).RequireAuthorization();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
    await db.Database.MigrateAsync();

    var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher<User>>();
    if (!await db.Users.AnyAsync())
    {
        var seedUser = new User
        {
            Id = Guid.NewGuid(),
            Email = "admin@amigurumi.local",
            Name = "Admin"
        };
        seedUser.PasswordHash = hasher.HashPassword(seedUser, "ChangeMe123!");
        await db.Users.AddAsync(seedUser);
        await db.SaveChangesAsync();
    }
}

app.Run();
