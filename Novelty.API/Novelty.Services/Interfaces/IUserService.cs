using Novelty.DTOs.Book;
using Novelty.DTOs.User;
namespace Novelty.Services.Interfaces;

public interface IUserService
{
    Task<List<UserDetailDto>> GetAllUsers();
    Task<UserDetailDto?> GetUserById(int userId);
    Task<UserDetailDto?> CreateUser(CreateUserDto createUser);
    Task<bool> UpdateUser(int userId, UpdateUserDto updateUser);
    Task<bool> DeleteUser(int userId);
}
