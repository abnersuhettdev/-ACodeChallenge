using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Models;
using SchoolManagement.ServiceResult;
using SchoolManagement.Repository.Interface;

namespace SchoolManagement.Repository
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
                return new Result<Student>().InvalidResult("Aluno não encontrado.");
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
                return new Result<Student>().InvalidResult("Erro ao criar aluno.", student);
            }
        }

        public Result<Student> UpdateStudent(Student student)
        {
            var existingStudent = _context.Students.SingleOrDefault(s => s.RA == student.RA);
            if (existingStudent == null)
            {
                return new Result<Student>().InvalidResult("Aluno não encontrado.");
            }

            existingStudent.Name = student.Name;
            existingStudent.Email = student.Email;

            var saveResult = _context.SaveChanges();
            if (saveResult == 0)
            {
                return new Result<Student>().InvalidResult("Erro ao atualizar aluno.");
            }

            return new Result<Student>().SuccessResult(existingStudent);
        }

        public Result<Student> DeleteStudent(string ra)
        {
            var student = _context.Students.SingleOrDefault(s => s.RA == ra);
            if (student == null)
            {
                return new Result<Student>().InvalidResult("Aluno não encontrado.");
            }

            try
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
                return new Result<Student>().SuccessResult(student);
            }
            catch (DbUpdateException ex)
            {
                return new Result<Student>().InvalidResult("Erro ao excluir aluno.", student);
            }
        }

        public Result ValidateStudentEmail(string email)
        {
            var exists = _context.Students.Any(s => s.Email == email);

            if (exists != false)
            {
                return new Result().InvalidResult("Email já está em uso.");
            }

            return new Result().SuccessResult();
        }

        public Result ValidateStudentData(Student student)
        {
            var existsRA = _context.Students.Any(s => s.RA == student.RA);
            if (existsRA != false)
            {
                return new Result().InvalidResult("RA já está em uso.");
            }
            var existsCPF = _context.Students.Any(s => s.CPF == student.CPF);
            if (existsCPF != false)
            {
                return new Result().InvalidResult("CPF já está em uso.");
            }

            return new Result().SuccessResult();
        }

    }
}
