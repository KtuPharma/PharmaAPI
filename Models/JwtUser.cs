namespace API.Models
{
    public class JwtUser
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public DepartmentId roleId { get; set; }
    }
}
