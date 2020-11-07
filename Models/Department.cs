namespace API.Models
{
    public enum DepartmentId
    {
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
