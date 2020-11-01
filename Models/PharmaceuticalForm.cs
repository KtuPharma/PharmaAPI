namespace API.Models.Enums
{
    public enum PharmaceuticalFormId : int
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
