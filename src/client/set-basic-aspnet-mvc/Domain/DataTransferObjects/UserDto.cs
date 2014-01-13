namespace set_basic_aspnet_mvc.Domain.DataTransferObjects
{
    public class UserDto
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Language { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public string PasswordHash { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}