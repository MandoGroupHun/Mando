namespace MandoWebApp.Models.ViewModels
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public string Category { get; set; }
        public string UnitName { get; set; }
        public SizeType? SizeType { get; set; }
        public string Name { get; set; }
    }
}
