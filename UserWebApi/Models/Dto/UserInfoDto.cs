using System.ComponentModel.DataAnnotations;

namespace UserWebApi.Models.Dto
{
    public class UserInfoDto
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int Role { get; set; }
    }
}
