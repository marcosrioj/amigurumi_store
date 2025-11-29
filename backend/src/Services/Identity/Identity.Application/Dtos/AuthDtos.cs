namespace AmigurumiStore.Identity.Application.Dtos;

public record RegisterDto(string Email, string Name, string Password);
public record LoginDto(string Email, string Password);
public record AuthResult(Guid UserId, string Email, string Name, string Token);
public record ProfileDto(string Email, string Name, DateTime CreatedAtUtc);
