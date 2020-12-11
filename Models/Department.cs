namespace API.Models
{
    public enum DepartmentId
    {
        None = 0,
        Pharmacy = 1,
        Warehouse,
        Transportation,
        Admin
    }

    public class Department
    {
        public DepartmentId Id { get; set; }
        public string Name { get; set; }
    }
}
