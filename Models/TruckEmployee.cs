namespace API.Models
{
    public class TruckEmployee
    {
        public int TruckId { get; set; }
        public Truck Truck { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
