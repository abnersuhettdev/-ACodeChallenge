using Microsoft.EntityFrameworkCore;
using SchoolManagement.BusinessRules.Interface;
using SchoolManagement.Data;
using SchoolManagement.Models;
using SchoolManagement.ServiceResult;
using SchoolManagement.Services.Interface;
using System.Text.RegularExpressions;

namespace SchoolManagement.BusinessRules
{
    public class StudentRules : IStudentRules
    {
       private readonly IStudentsService _studentsService;
        
        public StudentRules(IStudentsService studentsService)
        {
            _studentsService = studentsService;
        }

        public Result<List<Student>> GetAllStudents()
        {
            var students = _studentsService.GetAllStudents();
            if (students.IsNotOkResult())
            {
                return new Result<List<Student>>().InvalidResult("Não foi possivel buscar os alunos");
            }

            return new Result<List<Student>>().SuccessResult(students.Data);
        }

        public Result<Student> GetStudentByRA(string ra)
        {
            if (string.IsNullOrEmpty(ra))
            {
                return new Result<Student>().InvalidResult("Registro Acadêmico de busca não pode ser nulo");
            }

            var student = _studentsService.GetStudentByRA(ra);
            if (student.IsNotOkResult())
            {
                return new Result<Student>().InvalidResult("Não foi possivel buscar o aluno");
            }

            return new Result<Student>().SuccessResult(student.Data);
        }

        public Result<Student> CreateStudent(Student student)
        {
            if (string.IsNullOrEmpty(student.Name))
            {
                return new Result<Student>().InvalidResult("O nome é obrigatório.");
            }

            if (string.IsNullOrEmpty(student.Email) || !IsValidEmail(student.Email))
            {
                return new Result<Student>().InvalidResult("O email é obrigatório e deve ser válido.");
            }

            if (string.IsNullOrEmpty(student.RA) || student.RA.Length != 6)
            {
                return new Result<Student>().InvalidResult("O RA deve conter exatamente 6 dígitos.");
            }

            if (string.IsNullOrEmpty(student.CPF) || !IsValidCpf(student.CPF))
            {
                return new Result<Student>().InvalidResult("O CPF deve estar no formato 000.000.000-77.");
            }

            var validStudentEmail = _studentsService.ValidateStudentEmail(student.Email);
            if (validStudentEmail.IsNotOkResult())
            {
                return new Result<Student>().InvalidResult(validStudentEmail.Error);
            }

            var validStudent = _studentsService.ValidateStudentData(student);
            if (validStudent.IsNotOkResult())
            {
                return new Result<Student>().InvalidResult(validStudent.Error);
            }


            var createdStudent = _studentsService.CreateStudent(student);
            if(createdStudent.IsNotOkResult())
            {
                return new Result<Student>().InvalidResult("Erro ao criar aluno");
            }

            return new Result<Student>().SuccessResult(createdStudent.Data);
        }

        public Result<Student> UpdateStudent(Student student)
        {
            var existingStudent = _studentsService.GetStudentByRA(student.RA);
            if (existingStudent.IsNotOkResult())
            {
                return new Result<Student>().InvalidResult("Aluno não encontrado.");
            }

            if (string.IsNullOrEmpty(student.Name))
            {
                return new Result<Student>().InvalidResult("O nome é obrigatório.");
            }

            if (string.IsNullOrEmpty(student.Email) || !IsValidEmail(student.Email))
            {
                return new Result<Student>().InvalidResult("O email é obrigatório e deve ser válido.");
            }

            var validStudentEmail = _studentsService.ValidateStudentEmail(student.Email);
            if (validStudentEmail.IsNotOkResult())
            {
                return new Result<Student>().InvalidResult(validStudentEmail.Error);
            }

            var updatedStudent = _studentsService.UpdateStudent(student);
            if (updatedStudent.IsNotOkResult())
            {
                return new Result<Student>().InvalidResult(updatedStudent.Error);
            }

            return new Result<Student>().SuccessResult(updatedStudent.Data);

        }

        public Result DeleteStudent(string ra)
        {
            var existingStudent = _studentsService.GetStudentByRA(ra);
            if (existingStudent.IsNotOkResult())
            {
                return new Result().InvalidResult("Aluno não encontrado.");
            }

            var deletedStudent = _studentsService.DeleteStudent(ra);
            if (deletedStudent.IsNotOkResult())
            {
                return new Result().InvalidResult("Erro ao deletar aluno");
            }

            return new Result().SuccessResult();
        }

       

        private bool IsValidCpf(string cpf)
        {
            var regex = new Regex(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$");
            return regex.IsMatch(cpf);
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            var regex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, regex);
        }
    }
}
