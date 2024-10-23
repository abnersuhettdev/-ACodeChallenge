using SchoolManagement.Models;
using SchoolManagement.ServiceResult;

namespace SchoolManagement.Repository.Interface
{
    public interface IStudentsService
    {
        Result<List<Student>> GetAllStudents();

        Result<Student> GetStudentByRA(string ra);

        Result<Student> CreateStudent(Student student);

        Result<Student> UpdateStudent(Student student);

        Result<Student> DeleteStudent(string ra);

        Result ValidateStudentData(Student student);

        Result ValidateStudentEmail(string email);
    }
}
