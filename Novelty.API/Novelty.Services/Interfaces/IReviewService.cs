using Novelty.DTOs.Book;

namespace Novelty.Services.Interfaces;

public interface IReviewService
{
    Task<List<ReviewDetailDto>> GetAllReviewsForUser(int userId);
    Task<List<ReviewDetailDto>> GetAllReviewsForBook(int bookId);
    Task<ReviewDetailDto?> CreateReview(CreateReviewDto createReview);
    Task<bool> DeleteReview(int reviewId, int userId);
}
