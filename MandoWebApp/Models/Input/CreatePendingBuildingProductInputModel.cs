namespace MandoWebApp.Models.Input
{
    public class CreatePendingBuildingProductInputModel
    {
        public int BuildingId { get; set; }
        public string? HuProductName { get; set; }
        public string? EnProductName { get; set; }
        public int Quantity { get; set; }
        public string? Size { get; set; }
        public string Category { get; set; }
        public SizeType? SizeType { get; set; }
        public int UnitId { get; set; }
    }
}
