namespace MandoWebApp.Models.Input
{
    public class CreatePendingBuildingProductInputModel
    {
        public int BuildingId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string? Size { get; set; }
        public string Category { get; set; }
        public SizeType? SizeType { get; set; }
        public int UnitId { get; set; }
    }
}
