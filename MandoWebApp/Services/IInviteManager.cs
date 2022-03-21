using CSharpFunctionalExtensions;
using MandoWebApp.Models;

namespace MandoWebApp.Services
{
    public interface IInviteManager
    {
        public Invite? GetInvite(string inviteId);
        public Task<Result> UpdateInviteStatusAsync(string inviteId, InviteStatus status);
        public Task<List<Invite>> GetPendingInvites(int maxCount, DateTime? since = null);
        public Task AddInvite(Invite newInvite);
    }
}
