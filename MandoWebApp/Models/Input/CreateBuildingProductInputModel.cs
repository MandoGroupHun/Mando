namespace MandoWebApp.Models.Input
{
    public class CreateBuildingProductInputModel
    {
        public int BuildingId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string? Size { get; set; }
    }
}
