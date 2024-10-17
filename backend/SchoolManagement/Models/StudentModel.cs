using SchoolManagement.Enums;

namespace SchoolManagement.Models
{
    public class StudentModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string RA { get; set; }

        public string CPF { get; set; }

        public ProfileEnum Profile { get; set; }
    }
}
