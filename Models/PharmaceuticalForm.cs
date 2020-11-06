namespace API.Models
{
    public enum PharmaceuticalFormId
    {
        Tablets = 1,
        Syrup,
        Suspension,
        Lozenge,
        Spray,
        Drops,
        Ointment,
        Injection
    }

    public class PharmaceuticalForm
    {
        public PharmaceuticalFormId Id { get; set; }
        public string Name { get; set; }
    }
}
