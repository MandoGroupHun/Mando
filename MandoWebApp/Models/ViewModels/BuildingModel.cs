namespace MandoWebApp.Models.ViewModels
{
    public class BuildingModel
    {
        public int BuildingId { get; set; }
        public string Name { get; set; }
        public int Zip { get; set; }
        public string Address { get; set; }
        public string? Description { get; set; }
    }
}