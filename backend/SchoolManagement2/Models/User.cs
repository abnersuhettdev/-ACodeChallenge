using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

    }

    public class UserCreateVO : User
    {
        public string PasswordConfirm { get; set; }

    }

    public class UserLogin {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
