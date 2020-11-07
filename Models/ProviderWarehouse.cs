namespace API.Models
{
    public class ProviderWarehouse
    {
        public int ProviderId { get; set; }
        public MedicineProvider Provider { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
    }
}
