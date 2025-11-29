using AmigurumiStore.Identity.Application.Data;
using AmigurumiStore.Identity.Application.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AmigurumiStore.Identity.Application.Queries;

public record GetProfileQuery(Guid UserId) : IRequest<ProfileDto?>;

public class GetProfileQueryHandler(IdentityDbContext dbContext) : IRequestHandler<GetProfileQuery, ProfileDto?>
{
    public async Task<ProfileDto?> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
        return user is null ? null : new ProfileDto(user.Email, user.Name, user.CreatedAtUtc);
    }
}
