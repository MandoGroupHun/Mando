namespace MandoWebApp.Options
{
    public class EmailOptions
    {
        public bool IsEnabled { get; set; }
        public string? SendGridKey { get; set; }
        public string? FromEmail { get; set; }
        public string? FromName { get; set; }
    }
}
