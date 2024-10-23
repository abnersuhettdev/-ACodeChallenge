
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Models
{
    public class Student
    {
        public string Name { get; set; }
        
        public string Email { get; set; }
        
        [Key]
        public string RA { get; set; }
        
        public string CPF { get; set; }
    }
}
