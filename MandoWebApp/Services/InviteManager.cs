using CSharpFunctionalExtensions;
using MandoWebApp.Data;
using MandoWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MandoWebApp.Services
{
    public class InviteManager : IInviteManager
    {
        private readonly ApplicationDbContext _dbContext;
        public InviteManager(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Invite? GetInvite(string inviteId)
        {
            if (inviteId == null)
            {
                return null;
            }

            return _dbContext.Invites.FirstOrDefault(invite => invite.InviteId.ToString().ToLower() == inviteId.ToLower());
        }

        public Task<List<Invite>> GetPendingInvites(int maxCount, DateTime? since = null)
        {
            var invitesQuery = _dbContext.Invites
                .Where(i => i.Status == InviteStatus.New || i.Status == InviteStatus.Sent);

            if (since != null)
            {
                invitesQuery = invitesQuery.Where(i => i.CreatedAt >= since);
            }

            return invitesQuery
                .OrderByDescending(i => i.CreatedAt)
                .Take(maxCount)
                .ToListAsync();
        }

        public async Task<Result> UpdateInviteStatusAsync(string inviteId, InviteStatus status)
        {
            if (inviteId == null)
            {
                return Result.Success();
            }

            var invite = GetInvite(inviteId);

            if (invite == null)
            {
                return Result.Failure("Invite not found");
            }

            invite.Status = status;

            await _dbContext.SaveChangesAsync();

            return Result.Success();
        }
    }
}
