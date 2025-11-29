using AmigurumiStore.Identity.Application.Data;
using AmigurumiStore.Identity.Application.Dtos;
using AmigurumiStore.Identity.Application.Models;
using AmigurumiStore.Identity.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AmigurumiStore.Identity.Application.Commands;

public record RegisterUserCommand(RegisterDto Register) : IRequest<AuthResult>;

public class RegisterUserCommandHandler(
    IdentityDbContext dbContext,
    TokenService tokenService,
    IPasswordHasher<User> hasher) : IRequestHandler<RegisterUserCommand, AuthResult>
{
    public async Task<AuthResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var email = request.Register.Email.Trim().ToLowerInvariant();
        var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        if (existingUser is not null) throw new InvalidOperationException("Email already registered.");

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            Name = request.Register.Name
        };

        user.PasswordHash = hasher.HashPassword(user, request.Register.Password);

        await dbContext.Users.AddAsync(user, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var token = tokenService.Create(user);
        return new AuthResult(user.Id, user.Email, user.Name, token);
    }
}
