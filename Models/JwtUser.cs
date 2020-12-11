namespace API.Models
{
    public class JwtUser
    {
        public string Email { get; set; }
        public DepartmentId roleId { get; set; }
    }
}
