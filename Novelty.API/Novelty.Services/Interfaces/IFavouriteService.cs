using Novelty.DTOs.Book;

namespace Novelty.Services.Interfaces;

public interface IFavouriteService
{
    Task<List<FavouriteDetailDto>> GetAllFavouritesForUser(int userId);
    Task<FavouriteDetailDto?> CreateFavourite(CreateFavouriteDto createFavourite);
    Task<bool> DeleteFavourite(int userId, int bookId);
}
