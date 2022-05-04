using CSharpFunctionalExtensions;
using MandoWebApp.Data;
using MandoWebApp.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace MandoWebApp.Services.InviteService
{
    public class InviteService : IInviteService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<InviteService> _logger;
        private readonly IEmailSender _emailSender;

        private readonly string _url;

        public InviteService(ApplicationDbContext dbContext, ILogger<InviteService> logger, IEmailSender emailSender, IConfiguration configuration)
        {
            _logger = logger;
            _dbContext = dbContext;
            _emailSender = emailSender;
            _url = configuration["Url"];
        }

        /// <summary>
        /// Adds a new invite
        /// </summary>
        /// <param name="newInvite"></param>
        /// <returns>Return result value is true if a new invite has been added and false if it was already added</returns>
        public async Task<Result<bool>> AddInvite(Invite newInvite)
        {
            try
            {
                _dbContext.Add(newInvite);

                await _dbContext.SaveChangesAsync();

                await _emailSender.SendEmailAsync(newInvite.Email,
                    "Meghívó a Mando alkalmazásba",
                    $"https://{_url}/Identity/Account/Register?inviteId={newInvite.InviteId}");
            }
            catch (DbUpdateException ex) when (ex.InnerException is MySqlException inner && inner.Message.Contains("Duplicate"))
            {
                return Result.Success(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception during creation of new invite");

                return Result.Failure<bool>("Error during invite creation");
            }

            return Result.Success(true);
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
