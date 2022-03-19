using CSharpFunctionalExtensions;
using MandoWebApp.Data;
using MandoWebApp.Models;

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
