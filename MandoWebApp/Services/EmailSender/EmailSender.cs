using MandoWebApp.Options;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace MandoWebApp.Services.EmailSender;

public class EmailSender : IEmailSender
{
    private readonly ILogger _logger;
    public EmailOptions Options { get; }

    public EmailSender(IOptions<EmailOptions> optionsAccessor,
                       ILogger<EmailSender> logger)
    {
        Options = optionsAccessor.Value;
        _logger = logger;
    }


    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        if (Options.IsEnabled && string.IsNullOrEmpty(Options.SendGridKey) && string.IsNullOrEmpty(Options.FromName) && string.IsNullOrEmpty(Options.FromEmail))
        {
            throw new Exception("Some of the EmailSendingOptions are missing");
        }

        await Execute(Options.SendGridKey!, subject, message, toEmail);
    }

    public async Task Execute(string apiKey, string subject, string message, string toEmail)
    {
        if (!Options.IsEnabled)
        {
            _logger.LogInformation("Email sending is disabled");
            return;
        }

        var client = new SendGridClient(apiKey);
        var sendGridMessage = new SendGridMessage()
        {
            From = new EmailAddress(Options.FromEmail, Options.FromName),
            Subject = subject,
            PlainTextContent = message,
            HtmlContent = message
        };
        sendGridMessage.AddTo(new EmailAddress(toEmail));

        // Disable click tracking.
        // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
        sendGridMessage.SetClickTracking(false, false);

        var response = await client.SendEmailAsync(sendGridMessage);

        _logger.LogInformation(response.IsSuccessStatusCode
                               ? $"Email to {toEmail} queued successfully!"
                               : $"Failure Email to {toEmail}");
    }
}