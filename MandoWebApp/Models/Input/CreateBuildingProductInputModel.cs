namespace MandoWebApp.Models.Input
{
    public class CreateBuildingProductInputModel
    {
        public int BuildingID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public string? Size { get; set; }
    }
}
