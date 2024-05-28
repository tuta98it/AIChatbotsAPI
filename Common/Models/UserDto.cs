namespace Erp.Common.Models
{
    public class UserDto
    {
        public long Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string HashedPassword { get; set; } = string.Empty;
    }
}
