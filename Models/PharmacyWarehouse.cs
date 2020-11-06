namespace API.Models
{
    public class PharmacyWarehouse
    {
        public int PharmacyId { get; set; }
        public Pharmacy Pharmacy { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
    }
}
