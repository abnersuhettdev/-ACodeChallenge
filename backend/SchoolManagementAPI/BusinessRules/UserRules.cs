using Microsoft.AspNetCore.Identity;
using SchoolManagement.BusinessRules.Interface;
using SchoolManagement.Models;
using SchoolManagement.ServiceResult;
using SchoolManagement.Repository;
using SchoolManagement.Repository.Interface;
using System.Text.RegularExpressions;

namespace SchoolManagement.BusinessRules
{
    public class UserRules : IUserRules
    {
        private readonly IUserService _userService;
        public UserRules(IUserService userService)
        {
            _userService = userService;
        }

        public Result<List<User>> GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            if (users.IsNotOkResult())
            {
                return new Result<List<User>>().InvalidResult("Não foi possivel buscar os usuários");
            }

            return new Result<List<User>>().SuccessResult(users.Data);
        }


        public Result<User> GetUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return new Result<User>().InvalidResult("Email não pode ser nulo");
            }

            var user = _userService.GetUserByEmail(email);
            if (user.IsNotOkResult())
            {
                return new Result<User>().InvalidResult("Não foi possivel buscar o usuário.");
            }

            return new Result<User>().SuccessResult(user.Data);
        }

        public Result<User> CreateUser(UserCreateVO user)
        {
           if (string.IsNullOrEmpty(user.Username))
            {
                return new Result<User>().InvalidResult("O nome é obrigatório.");
             }

            if (string.IsNullOrEmpty(user.Email) || !IsValidEmail(user.Email))
            {
                return new Result<User>().InvalidResult("O email é obrigatório e deve ser válido.");
            }

            if (string.IsNullOrEmpty(user.Password) && user.Password.Length < 5)
            {
                return new Result<User>().InvalidResult("A senha é obrigatória, no minimo 5 digitos");
            }

            if (user.PasswordConfirm != user.Password)
            {
                return new Result<User>().InvalidResult("As senhas nao coincidem");
            }

            var validUserEmail = _userService.ValidateUserEmail(user.Email);
            if (validUserEmail.IsNotOkResult())
            {
                return new Result<User>().InvalidResult(validUserEmail.Error);
            }

            user.Password = HashPassword(user.Password);

            var createdUser = _userService.CreateUser(user);
            if (createdUser.IsNotOkResult())
            {
                return new Result<User>().InvalidResult(createdUser);
            }

            return new Result<User>().SuccessResult(createdUser.Data);
        }
        public Result<string> Login(UserLogin user)
        {
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return new Result<string>().InvalidResult("Campo não pode estar vazio.");
            }

            var existingUser = _userService.GetUserByEmail(user.Email);
            if (existingUser.IsNotOkResult())
            {
                return new Result<string>().InvalidResult(existingUser.Error);
            }

            var matchPasswords = VerifyPassword(user.Password, existingUser.Data.Password);
            if (!matchPasswords)
            {
                return new Result<string>().InvalidResult("Credenciais não reconhecidas.");
            }

            

            return new Result<string>().SuccessResult("Login autorizado");
        }


        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            var regex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, regex);
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
