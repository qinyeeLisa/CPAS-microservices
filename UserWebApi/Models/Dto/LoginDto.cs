using System.ComponentModel.DataAnnotations.Schema;

namespace UserWebApi.Models.Dto
{
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
