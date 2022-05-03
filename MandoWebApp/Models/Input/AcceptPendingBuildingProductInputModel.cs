namespace MandoWebApp.Models.Input
{
    public class AcceptPendingBuildingProductInputModel : CreatePendingBuildingProductInputModel
    {
        public long PendingBuildingProductId { get; set; }
        public int? ProductId { get; set; }
    }
}
