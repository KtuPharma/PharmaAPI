namespace API.Models
{
    public enum DepartmentId
    {
        None = 0,
        Pharmacy = 1,
        Warehouse = 2,
        Transportation = 3,
        Admin = 4
    }

    public class Department
    {
        public DepartmentId Id { get; set; }
        public string Name { get; set; }
    }
}
