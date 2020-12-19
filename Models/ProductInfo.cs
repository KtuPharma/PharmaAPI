namespace API.Models
{
    public class ProductInfo
    {
        public int Id { get; }

        public int MedicamentId { get; }

        public int Quantity { get; }

        public ProductInfo(ProductBalance pb)
        {
            Id = pb.Id;
            MedicamentId = pb.Medicament.Id;
            Quantity = pb.Quantity;
        }
    }
}
