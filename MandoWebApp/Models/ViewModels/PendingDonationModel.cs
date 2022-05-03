namespace MandoWebApp.Models.ViewModels
{
    public class PendingDonationModel
    {
        public long PendingDonationId { get; set; }

        public string Category { get; set; }

        public int CategoryId { get; set; }

        public string HuProductName { get; set; }

        public string EnProductName { get; set; }

        public int Quantity { get; set; }

        public SizeType? SizeType { get; set; }

        public string Size { get; set; }

        public int UnitId { get; set; }

        public string UnitName { get; set; }

        public DateTime RecordedAt { get; set; }

        public string UserName { get; set; }

        public bool IsProcessed { get; set; }
    }
}
