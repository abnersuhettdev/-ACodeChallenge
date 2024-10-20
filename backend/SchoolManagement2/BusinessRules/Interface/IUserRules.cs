using SchoolManagement.Models;
using SchoolManagement.ServiceResult;

namespace SchoolManagement.BusinessRules.Interface
{
    public interface IUserRules
    {
        Result<User> CreateUser(UserCreateVO User);

        Result<User> GetUserByEmail(string email);

        Result<List<User>> GetAllUsers();

        Result<string> Login(UserLogin user);

    }
}
