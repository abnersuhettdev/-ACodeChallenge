using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Models;
using SchoolManagement.ServiceResult;
using SchoolManagement.Services.Interface;

namespace SchoolManagement.Services
{
    public class UserService : IUserService
    {
        private readonly SchoolContext _context;

        public UserService(SchoolContext context)
        {
            _context = context;
        }

        public Result<List<User>> GetAllUsers()
        {
            var users = _context.Users.ToList();
            return new Result<List<User>>().SuccessResult(users);
        }

        public Result<User> GetUserByEmail(string email)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == email);
            if (user == null)
            {
                return new Result<User>().InvalidResult("Usuário não encontrado.");
            }
            return new Result<User>().SuccessResult(user);
        }

        public Result<User> CreateUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return new Result<User>().SuccessResult(user);
            }
            catch (DbUpdateException ex)
            {
                return new Result<User>().InvalidResult("Erro ao criar usuário.", user);
            }
        }

        public Result ValidateUserEmail(string email)
        {
            var existingUsers = _context.Users.ToList();

            var existingUserByEmail = existingUsers.FirstOrDefault(s => s.Email == email);
            if (existingUserByEmail != null)
            {
                return new Result().InvalidResult("Email já está em uso.");
            }

            return new Result().SuccessResult();
        }
    }
}
