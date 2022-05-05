using CSharpFunctionalExtensions;
using FluentEmail.Core;
using FluentEmail.Razor;
using MandoWebApp.Data;
using MandoWebApp.Models;
using MandoWebApp.Options;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace MandoWebApp.Services.InviteService
{
    public class InviteService : IInviteService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<InviteService> _logger;
        private readonly IEmailSender _emailSender;
        private readonly EmailOptions _emailOptions;

        private readonly string _url;

        public InviteService(ApplicationDbContext dbContext, ILogger<InviteService> logger, IEmailSender emailSender, IConfiguration configuration,
            IOptions<EmailOptions> emailOptions)
        {
            _logger = logger;
            _dbContext = dbContext;
            _emailSender = emailSender;
            _emailOptions = emailOptions.Value;
            _url = configuration["Url"];
        }

        /// <summary>
        /// Adds a new invite
        /// </summary>
        /// <param name="newInvite"></param>
        /// <returns>Return result value is true if a new invite has been added and false if it was already added</returns>
        public async Task<Result<bool>> AddInvite(Invite newInvite, string lang)
        {
            try
            {
                await _dbContext.AddAsync(newInvite);

                await _dbContext.SaveChangesAsync();

                var (template, subject, buildingName) = GetInvitationData(lang);

                var body = await new RazorRenderer().ParseAsync(template, new { Email = newInvite.Email, BuildingName = buildingName, Link = $"https://{_url}/Identity/Account/Register?inviteId={newInvite.InviteId}" });

                await _emailSender.SendEmailAsync(newInvite.Email,
                    subject,
                    body);
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

        const string enTemplate = @"<!DOCTYPE html><html lang=""en"" xmlns:o=""urn:schemas-microsoft-com:office:office"" xmlns:v=""urn:schemas-microsoft-com:vml""><head> <title></title> <meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type""/> <meta content=""width=device-width, initial-scale=1.0"" name=""viewport""/> <link href=""https://fonts.googleapis.com/css?family=Lato"" rel=""stylesheet"" type=""text/css""/> <style>*{box-sizing: border-box;}body{margin: 0; padding: 0;}a[x-apple-data-detectors]{color: inherit !important; text-decoration: inherit !important;}#MessageViewBody a{color: inherit; text-decoration: none;}p{line-height: inherit}@@media (max-width:670px){.icons-inner{text-align: center;}.icons-inner td{margin: 0 auto;}.row-content{width: 100% !important;}.column .border{display: none;}table{table-layout: fixed !important;}.stack .column{width: 100%; display: block;}}</style></head><body style=""background-color: #F5F5F5; margin: 0; padding: 0; -webkit-text-size-adjust: none; text-size-adjust: none;""> <table border=""0"" cellpadding=""0"" cellspacing=""0"" class=""nl-container"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #F5F5F5;"" width=""100%""> <tbody> <tr> <td> <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""row row-1"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" width=""100%""> <tbody> <tr> <td> <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""row-content stack"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; color: #000000; width: 650px;"" width=""650""> <tbody> <tr> <td class=""column column-1"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top; padding-top: 5px; padding-bottom: 5px; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;"" width=""100%""> <div class=""spacer_block"" style=""height:30px;line-height:30px;font-size:1px;""> </div></td></tr></tbody> </table> </td></tr></tbody> </table> <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""row row-2"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" width=""100%""> <tbody> <tr> <td> <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""row-content stack"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #D6E7F0; color: #000000; width: 650px;"" width=""650""> <tbody> <tr> <td class=""column column-1"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top; padding-left: 25px; padding-right: 25px; padding-top: 5px; padding-bottom: 60px; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;"" width=""100%""> <table border=""0"" cellpadding=""0"" cellspacing=""0"" class=""text_block"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;"" width=""100%""> <tr> <td style=""padding-left:15px;padding-right:10px;padding-top:20px;""> <div style=""font-family: sans-serif""> <div class=""txtTinyMce-wrapper"" style=""font-size: 12px; font-family: Lato, Tahoma, Verdana, Segoe, sans-serif; mso-line-height-alt: 18px; color: #052d3d; line-height: 1.5;""> <p style=""margin: 0; font-size: 14px; text-align: center; mso-line-height-alt: 75px;""><span style=""font-size:50px;""><strong><span style=""font-size:50px;""><span style=""font-size:38px;"">Invitation</span></span></strong></span></p><p style=""margin: 0; font-size: 14px; text-align: center; mso-line-height-alt: 51px;""><span style=""font-size:34px;""><strong><span style=""font-size:34px;""><span style=""color:#2190e3;font-size:34px;""><span id=""e0f10cab-0852-4243-bbc9-5fa68b14550b"" style="""">@Model.Email</span></span></span></strong></span></p></div></div></td></tr></table> <table border=""0"" cellpadding=""10"" cellspacing=""0"" class=""text_block"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;"" width=""100%""> <tr> <td> <div style=""font-family: sans-serif""> <div class=""txtTinyMce-wrapper"" style=""font-size: 12px; mso-line-height-alt: 14.399999999999999px; color: #555555; line-height: 1.2; font-family: Lato, Tahoma, Verdana, Segoe, sans-serif;""> <p style=""margin: 0; font-size: 14px; text-align: center;""><span style=""font-size:18px;"">Please register to the donation manager application of @Model.BuildingName by clicking the button below. In case the button does not work, copy and paste this link into your browser's address bar:</span></p><p style=""margin: 0; font-size: 14px; text-align: center; mso-line-height-alt: 14.399999999999999px;""> </p><p style=""margin: 0; font-size: 14px; text-align: center;""><span style=""font-size:18px;""><span id=""6508c2ac-0d6e-4c51-aafb-d6c2070ab5e3"" style="""">@Model.Link</span></span></p></div></div></td></tr></table> <table border=""0"" cellpadding=""0"" cellspacing=""0"" class=""button_block"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" width=""100%""> <tr> <td style=""padding-bottom:10px;padding-left:10px;padding-right:10px;padding-top:20px;text-align:center;""> <div align=""center""> <a href=""@Model.Link"" style=""text-decoration:none;display:inline-block;color:#ffffff;background-color:#fc7318;border-radius:15px;width:auto;border-top:1px solid #fc7318;font-weight:400;border-right:1px solid #fc7318;border-bottom:1px solid #fc7318;border-left:1px solid #fc7318;padding-top:10px;padding-bottom:10px;font-family:Lato, Tahoma, Verdana, Segoe, sans-serif;text-align:center;mso-border-alt:none;word-break:keep-all;"" target=""_blank""><span style=""padding-left:40px;padding-right:40px;font-size:16px;display:inline-block;letter-spacing:normal;""><span style=""font-size: 16px; line-height: 2; word-break: break-word; mso-line-height-alt: 32px;""><strong>REGISTER</strong></span></span></a> </div></td></tr></table> </td></tr></tbody> </table> </td></tr></tbody> </table> </td></tr></tbody> </table></body></html>";
        const string huTemplate = @"<!DOCTYPE html><html lang=""en"" xmlns:o=""urn:schemas-microsoft-com:office:office"" xmlns:v=""urn:schemas-microsoft-com:vml""><head> <title></title> <meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type""/> <meta content=""width=device-width, initial-scale=1.0"" name=""viewport""/> <link href=""https://fonts.googleapis.com/css?family=Lato"" rel=""stylesheet"" type=""text/css""/> <style>*{box-sizing: border-box;}body{margin: 0; padding: 0;}a[x-apple-data-detectors]{color: inherit !important; text-decoration: inherit !important;}#MessageViewBody a{color: inherit; text-decoration: none;}p{line-height: inherit}@@media (max-width:670px){.icons-inner{text-align: center;}.icons-inner td{margin: 0 auto;}.row-content{width: 100% !important;}.column .border{display: none;}table{table-layout: fixed !important;}.stack .column{width: 100%; display: block;}}</style></head><body style=""background-color: #F5F5F5; margin: 0; padding: 0; -webkit-text-size-adjust: none; text-size-adjust: none;""> <table border=""0"" cellpadding=""0"" cellspacing=""0"" class=""nl-container"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #F5F5F5;"" width=""100%""> <tbody> <tr> <td> <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""row row-1"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" width=""100%""> <tbody> <tr> <td> <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""row-content stack"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; color: #000000; width: 650px;"" width=""650""> <tbody> <tr> <td class=""column column-1"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top; padding-top: 5px; padding-bottom: 5px; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;"" width=""100%""> <div class=""spacer_block"" style=""height:30px;line-height:30px;font-size:1px;""> </div></td></tr></tbody> </table> </td></tr></tbody> </table> <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""row row-2"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" width=""100%""> <tbody> <tr> <td> <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""row-content stack"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #D6E7F0; color: #000000; width: 650px;"" width=""650""> <tbody> <tr> <td class=""column column-1"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top; padding-left: 25px; padding-right: 25px; padding-top: 5px; padding-bottom: 60px; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;"" width=""100%""> <table border=""0"" cellpadding=""0"" cellspacing=""0"" class=""text_block"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;"" width=""100%""> <tr> <td style=""padding-left:15px;padding-right:10px;padding-top:20px;""> <div style=""font-family: sans-serif""> <div class=""txtTinyMce-wrapper"" style=""font-size: 12px; font-family: Lato, Tahoma, Verdana, Segoe, sans-serif; mso-line-height-alt: 18px; color: #052d3d; line-height: 1.5;""> <p style=""margin: 0; font-size: 14px; text-align: center; mso-line-height-alt: 75px;""><span style=""font-size:50px;""><strong><span style=""font-size:50px;""><span style=""font-size:38px;"">Meghívó</span></span></strong></span></p><p style=""margin: 0; font-size: 14px; text-align: center; mso-line-height-alt: 51px;""><span style=""font-size:34px;""><strong><span style=""font-size:34px;""><span style=""color:#2190e3;font-size:34px;""><span id=""e0f10cab-0852-4243-bbc9-5fa68b14550b"" style="""">@Model.Email</span></span></span></strong></span></p></div></div></td></tr></table> <table border=""0"" cellpadding=""10"" cellspacing=""0"" class=""text_block"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;"" width=""100%""> <tr> <td> <div style=""font-family: sans-serif""> <div class=""txtTinyMce-wrapper"" style=""font-size: 12px; mso-line-height-alt: 14.399999999999999px; color: #555555; line-height: 1.2; font-family: Lato, Tahoma, Verdana, Segoe, sans-serif;""> <p style=""margin: 0; font-size: 14px; text-align: center;""><span style=""font-size:18px;"">Kérlek regisztrálj a <span id=""42d4a275-0928-41fe-b383-27ea406f04b2"" style="""">@Model.BuildingName</span> adomány-karbantartó alkalmazásába a lenti gombra kattintva. Amennyiben a gomb nem működik, másold az alábbi linket a böngésződ címsávjába:</span></p><p style=""margin: 0; font-size: 14px; text-align: center; mso-line-height-alt: 14.399999999999999px;""> </p><p style=""margin: 0; font-size: 14px; text-align: center;""><span style=""font-size:18px;""><span id=""6508c2ac-0d6e-4c51-aafb-d6c2070ab5e3"" style="""">@Model.Link</span></span></p></div></div></td></tr></table> <table border=""0"" cellpadding=""0"" cellspacing=""0"" class=""button_block"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" width=""100%""> <tr> <td style=""padding-bottom:10px;padding-left:10px;padding-right:10px;padding-top:20px;text-align:center;""> <div align=""center""> <a href=""@Model.Link"" style=""text-decoration:none;display:inline-block;color:#ffffff;background-color:#fc7318;border-radius:15px;width:auto;border-top:1px solid #fc7318;font-weight:400;border-right:1px solid #fc7318;border-bottom:1px solid #fc7318;border-left:1px solid #fc7318;padding-top:10px;padding-bottom:10px;font-family:Lato, Tahoma, Verdana, Segoe, sans-serif;text-align:center;mso-border-alt:none;word-break:keep-all;"" target=""_blank""><span style=""padding-left:40px;padding-right:40px;font-size:16px;display:inline-block;letter-spacing:normal;""><span style=""font-size: 16px; line-height: 2; word-break: break-word; mso-line-height-alt: 32px;""><strong>REGISZRTÁCIÓ</strong></span></span></a> </div></td></tr></table> </td></tr></tbody> </table> </td></tr></tbody> </table> </td></tr></tbody> </table></body></html>";

        const string enSubject = "Invitation to donation manager application";
        const string huSubject = "Meghívó adomány kezelő alkalmazásba";

        private (string Template, string Subject, string BuildingName) GetInvitationData(string lang)
        {
            lang = lang.ToLower();
            var building = _dbContext.Buildings.First();

            if (lang == "en")
            {
                return (enTemplate, enSubject, building.ENName ?? building.HUName);
            }

            return (huTemplate, huSubject, building.HUName);
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
