using SchoolManagement.Models;
using SchoolManagement.ServiceResult;

namespace SchoolManagement.BusinessRules.Interface
{
    public interface IStudentRules
    {
        Result<Student> CreateStudent(Student student);

        Result<Student> UpdateStudent(Student student); 

        Result DeleteStudent(string ra); 

        Result<Student> GetStudentByRA(string ra);

        Result<List<Student>> GetAllStudents();

    }
}
