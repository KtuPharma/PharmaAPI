namespace API.Models
{
    public enum EmployeeStatusId
    {
        Employed = 1,
        Vacationing,
        Fired
    }

    public class EmployeeStatus
    {
        public EmployeeStatusId Id { get; set; }
        public string Name { get; set; }

        public EmployeeStatus(){}

        public EmployeeStatus(EmployeeStatus e)
        {
            Id = e.Id;
            Name = e.Name;
        }
    }
}
