namespace API.Models
{
    public enum TransactionMethodId
    {
        Cash = 1,
        Card
    }

    public class TransactionMethod
    {
        public TransactionMethodId Id { get; set; }
        public string Name { get; set; }
    }
}
