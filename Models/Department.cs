namespace API.Models
{
    public enum DepartmentId
    {
        None = 0,
        Pharmacy,
        Warehouse,
        Transportation,
        Admin
    }

    public class Department
    {
        public DepartmentId Id { get; set; }
        public string Name { get; set; }

        public Department() { }

        public Department(Department d)
        {
            Id = d.Id;
            Name = d.Name;
        }
    }
}
