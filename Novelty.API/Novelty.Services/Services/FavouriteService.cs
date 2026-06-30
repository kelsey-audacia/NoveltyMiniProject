using Microsoft.EntityFrameworkCore;
using Novelty.DataAccess;
using Novelty.Domain.Models;
using Novelty.DTOs.Book;
using Novelty.Services.Interfaces;
using Novelty.Services.Services;

namespace Novelty.Services.Services;

public class FavouriteService : IFavouriteService
{
    /// <summary>
    /// dependency injection
    /// </summary>
    private readonly AppDbContext _appDbContext;
    /// <summary>
    /// constructor
    /// </summary>
    public FavouriteService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<FavouriteDetailDto?> CreateFavourite(CreateFavouriteDto createFavourite)
    {
        var user = await _appDbContext.Users
            .Include(u => u.Favourites)
                .ThenInclude(f => f.Book)
            .FirstOrDefaultAsync(u => u.Id == createFavourite.UserId);
        if (user == null) return null;
        var book = await _appDbContext.Books
            .Include(b => b.Reviews)
            .FirstOrDefaultAsync(b => b.Id == createFavourite.BookId);
        if (book == null) return null;
        var doesFavouriteExistAlready = user.Favourites.Any(f => f.BookId == createFavourite.BookId);
        if (doesFavouriteExistAlready) return null;
        var newFavourite = new Favourite
        {
            BookId = createFavourite.BookId,
            UserId = createFavourite.UserId
        };
        await _appDbContext.Favourites.AddAsync(newFavourite);
        await _appDbContext.SaveChangesAsync();
        return new FavouriteDetailDto
        {
            Id = newFavourite.Id,
            UserName = user.Name,
            BookSummary = new BookSummaryDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                AverageRating = RatingHelper.CalculateAverageRating(book.Reviews)
            }
        };
    }

    public async Task<bool> DeleteFavourite(int userId, int bookId)
    {
        var favourite = await _appDbContext.Favourites
            .FirstOrDefaultAsync(f => f.UserId == userId && f.BookId == bookId);
        if (favourite == null) return false;
        _appDbContext.Remove(favourite);
        await _appDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<List<FavouriteDetailDto>?> GetAllFavouritesForUser(int userId)
    {
        var user = await _appDbContext.Users
            .Include(u => u.Favourites)
                .ThenInclude(f => f.Book)
                    .ThenInclude(b => b.Reviews)
            .FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null) return null;
        if (user.Favourites.Count < 1) return new List<FavouriteDetailDto>();
        return user.Favourites.Select(f => new FavouriteDetailDto
        {
            Id = f.Id,
            UserName = user.Name,
            BookSummary = new BookSummaryDto
            {
                Id = f.Book!.Id,
                Title = f.Book.Title,
                Author = f.Book.Author,
                AverageRating = RatingHelper.CalculateAverageRating(f.Book.Reviews)
            }
        }).ToList();
    }
}
