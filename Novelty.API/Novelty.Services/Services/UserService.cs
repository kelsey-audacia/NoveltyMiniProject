
using Microsoft.EntityFrameworkCore;
using Novelty.DataAccess;
using Novelty.Domain.Models;
using Novelty.DTOs.Book;
using Novelty.DTOs.User;
using Novelty.Services.Interfaces;
namespace Novelty.Services.Services;

public class UserService : IUserService
{
    /// <summary>
    /// dependency injection
    /// </summary>
    private readonly AppDbContext _appDbContext;
    /// <summary>
    /// constructor
    /// </summary>
    public UserService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    /// <summary>
    /// Returns a list of UserDetailDto 's for all users
    /// </summary>
    public async Task<List<UserDetailDto>> GetAllUsers()
    {
        var users = await _appDbContext.Users
            .Include(u => u.Reviews)
                .ThenInclude(r => r.Book)
            .Include(u => u.Favourites)
                .ThenInclude(f => f.Book)
            .ToListAsync();

        return users.Select(u => new UserDetailDto
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email,
            Reviews = u.Reviews.Select(r => new ReviewDetailDto
            {
                Id = r.Id,
                Rating = r.Rating,
                Comment = r.Comment,
                UserName = r.User?.Name ?? "Unknown",
                BookTitle = r.Book?.Title ?? "Unknown"
            }).ToList(),
            Favourites = u.Favourites.Select(f => new FavouriteDetailDto
            {
                Id = f.Id,
                UserName = f.User?.Name ?? "Unknown",
                BookSummary = new BookSummaryDto
                {
                    Id = f.Book?.Id ?? 0,
                    Title = f.Book?.Title ?? "Unknown",
                    Author = f.Book?.Author ?? "Unknown"
                }
            }).ToList()
        }).ToList();
    }
    /// <summary>
    /// Checks if user exists
    /// Doesn't exist: returns null
    /// Exists: returns UserDetailDto for user
    /// </summary>
    public async Task<UserDetailDto?> GetUserById(int userId)
    {
        var user = await _appDbContext.Users
            .Where(u => u.Id == userId)
            .Include(u => u.Reviews)
                .ThenInclude(r => r.Book)
            .Include(u => u.Favourites)
                .ThenInclude(f => f.Book)
            .FirstOrDefaultAsync();
        if (user == null) return null;

        return new UserDetailDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Reviews = user.Reviews.Select(r => new ReviewDetailDto
            {
                Id = r.Id,
                Rating = r.Rating,
                Comment = r.Comment,
                UserName = r.User?.Name ?? "Unknown",
                BookTitle = r.Book?.Title ?? "Unknown"
            }).ToList(),
            Favourites = user.Favourites.Select(f => new FavouriteDetailDto
            {
                Id = f.Id,
                UserName = f.User?.Name ?? "Unknown",
                BookSummary = new BookSummaryDto
                {
                    Id = f.Book?.Id ?? 0,
                    Title = f.Book?.Title ?? "Unknown",
                    Author = f.Book?.Author ?? "Unknown"
                }
            }).ToList()
        };
    }
    /// <summary>
    /// Checks if user exists
    /// Doesn't exist: creates a user and adds to database and returns UserDetailDTO
    /// Exists: returns null
    /// </summary>
    public async Task<UserDetailDto?> CreateUser(CreateUserDto createUser)
    {
        var doesUserExist = await _appDbContext.Users
            .AnyAsync(u => u.Email == createUser.Email);
        if (doesUserExist) return null;
        var newUser = new User
        {
            Name = createUser.Name,
            Email = createUser.Email
        };
        await _appDbContext.Users.AddAsync(newUser);
        await _appDbContext.SaveChangesAsync();
        return new UserDetailDto
        {
            Id = newUser.Id,
            Name = newUser.Name,
            Email = newUser.Email,
            Reviews = new List<ReviewDetailDto>(),
            Favourites = new List<FavouriteDetailDto>()
        };
    }
    /// <summary>
    /// Checks if user exists
    /// Doesn't exist: returns false
    /// Exists: updates user and returns true
    /// </summary>
    public async Task<bool> UpdateUser(int userId, UpdateUserDto updateUserDto)
    {
        var doesUserExist = await _appDbContext.Users
                .AnyAsync(u => u.Id == userId);
        if (!doesUserExist) return false;
        var isEmailInUseByOtherUser = await _appDbContext.Users
            .AnyAsync(u => u.Email == updateUserDto.Email && u.Id != userId);
        if (isEmailInUseByOtherUser) return false;
        var user = await _appDbContext.Users
            .FirstOrDefaultAsync(u => u.Id == userId);

        user!.Name = updateUserDto.Name;
        user!.Email = updateUserDto.Email;

        await _appDbContext.SaveChangesAsync();
        return true;
    }
    public async Task<bool> DeleteUser(int userId)
    {
        var user = await _appDbContext.Users
            .Include(u => u.Reviews)
            .Include(u => u.Favourites)
            .FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null || user.Reviews.Count > 0 || user.Favourites.Count > 0) return false;
        _appDbContext.Remove(user);
        await _appDbContext.SaveChangesAsync();
        return true;
    }
};

