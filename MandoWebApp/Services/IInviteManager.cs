using CSharpFunctionalExtensions;
using MandoWebApp.Models;

namespace MandoWebApp.Services
{
    public interface IInviteManager
    {
        public Invite? GetInvite(string inviteId);
        public Task<Result> UpdateInviteStatusAsync(string inviteId, InviteStatus status);
    }
}
