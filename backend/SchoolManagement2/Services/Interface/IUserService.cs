using SchoolManagement.Models;
using SchoolManagement.ServiceResult;

namespace SchoolManagement.Services.Interface
{
    public interface IUserService
    {
        Result<List<User>> GetAllUsers();

        Result<User> GetUserByEmail(string email);

        Result ValidateUserEmail(string email);

        Result<User> CreateUser(User user);

    }
}
