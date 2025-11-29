using AmigurumiStore.Identity.Application.Data;
using AmigurumiStore.Identity.Application.Dtos;
using AmigurumiStore.Identity.Application.Models;
using AmigurumiStore.Identity.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AmigurumiStore.Identity.Application.Commands;

public record LoginUserCommand(LoginDto Login) : IRequest<AuthResult>;

public class LoginUserCommandHandler(
    IdentityDbContext dbContext,
    TokenService tokenService,
    IPasswordHasher<User> hasher) : IRequestHandler<LoginUserCommand, AuthResult>
{
    public async Task<AuthResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var email = request.Login.Email.Trim().ToLowerInvariant();
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        if (user is null) throw new InvalidOperationException("Invalid credentials.");

        var verification = hasher.VerifyHashedPassword(user, user.PasswordHash, request.Login.Password);
        if (verification == PasswordVerificationResult.Failed) throw new InvalidOperationException("Invalid credentials.");

        var token = tokenService.Create(user);
        return new AuthResult(user.Id, user.Email, user.Name, token);
    }
}
