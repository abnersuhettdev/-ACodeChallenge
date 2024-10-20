using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Models;
using SchoolManagement.ServiceResult;
using SchoolManagement.Services.Interface;

namespace SchoolManagement.Services
{
    public class StudentService : IStudentsService
    {
        private readonly SchoolContext _context;

        public StudentService(SchoolContext context)
        {
            _context = context;
        }

        public Result<List<Student>> GetAllStudents()
        {
            var students = _context.Students.ToList();
            return new Result<List<Student>>().SuccessResult(students);
        }

        public Result<Student> GetStudentByRA(string ra)
        {
            var student = _context.Students.SingleOrDefault(s => s.RA == ra);
            if (student == null)
            {
                return new Result<Student>().InvalidResult("Student not found.");
            }

            return new Result<Student>().SuccessResult(student);
        }

        public Result<Student> CreateStudent(Student student)
        {
            try
            {
                _context.Students.Add(student);
                _context.SaveChanges();
                return new Result<Student>().SuccessResult(student);
            }
            catch (DbUpdateException ex)
            {
                return new Result<Student>().InvalidResult("Failed to create user.", student);
            }
        }

        public Result<Student> UpdateStudent(Student student)
        {
            var existingStudent = _context.Students.SingleOrDefault(s => s.RA == student.RA);
            if (existingStudent == null)
            {
                return new Result<Student>().InvalidResult("Student not found.");
            }

            existingStudent.Name = student.Name;
            existingStudent.Email = student.Email;

            var saveResult = _context.SaveChanges();
            if (saveResult == 0)
            {
                return new Result<Student>().InvalidResult("Failed to update student.");
            }

            return new Result<Student>().SuccessResult(existingStudent);
        }

        public Result<Student> DeleteStudent(string ra)
        {
            var student = _context.Students.SingleOrDefault(s => s.RA == ra);
            if (student == null)
            {
                return new Result<Student>().InvalidResult("Student not found.");
            }

            try
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
                return new Result<Student>().SuccessResult(student);
            }
            catch (DbUpdateException ex)
            {
                return new Result<Student>().InvalidResult("Failed to delete student.", student);
            }
        }

        public Result ValidateStudentEmail(string email)
        {
            var existingStudents = _context.Students.ToList();

            var existingStudentByEmail = existingStudents.FirstOrDefault(s => s.Email == email);
            if (existingStudentByEmail != null)
            {
                return new Result().InvalidResult("Email já está em uso.");
            }

            return new Result().SuccessResult();
        }

        public Result ValidateStudentData(Student student)
        {
            var existingStudents = _context.Students.ToList();

            var existingStudentByRA = existingStudents.FirstOrDefault(s => s.RA == student.RA);
            if (existingStudentByRA != null)
            {
                return new Result().InvalidResult("RA já está em uso.");
            }

            var existingStudentByCPF = existingStudents.FirstOrDefault(s => s.CPF == student.CPF);
            if (existingStudentByCPF != null)
            {
                return new Result().InvalidResult("CPF já está em uso.");
            }

            return new Result().SuccessResult();
        }

    }
}
