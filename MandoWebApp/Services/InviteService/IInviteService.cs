using CSharpFunctionalExtensions;
using MandoWebApp.Models;

namespace MandoWebApp.Services.InviteService
{
    public interface IInviteService
    {
        public Invite? GetInvite(string inviteId);
        public Task<Result> UpdateInviteStatusAsync(string inviteId, InviteStatus status);
        public Task<List<Invite>> GetPendingInvites(int maxCount, DateTime? since = null);
        public Task<Result<bool>> AddInvite(Invite newInvite, string lang);
    }
}
