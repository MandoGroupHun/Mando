namespace MandoWebApp.Models.ViewModels
{
    public class PendingDonationModel
    {
        public string Category { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public SizeType? SizeType { get; set; }

        public string Size { get; set; }

        public string UnitName { get; set; }

        public DateTime RecordedAt { get; set; }

        public string UserName { get; set; }

        public bool IsProcessed { get; set; }
    }
}
