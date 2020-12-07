namespace API.Models
{
    public enum EmployeeStatusId
    {
        Employed = 1,
        Vacationing = 2,
        Fired = 3
    }

    public class EmployeeStatus
    {
        public EmployeeStatusId Id { get; set; }
        public string Name { get; set; }
    }
}
