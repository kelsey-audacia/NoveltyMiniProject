using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Novelty.DataAccess;
using Novelty.Domain.Models;
using Novelty.DTOs.Book;
using Novelty.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Novelty.Services.Services;

public class ReviewService : IReviewService
{
    /// <summary>
    /// dependency injection
    /// </summary>
    private readonly AppDbContext _appDbContext;
    /// <summary>
    /// constructor
    /// </summary>
    public ReviewService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<ReviewDetailDto?> CreateReview(CreateReviewDto createReview)
    {
        var user = await _appDbContext.Users
            .Include(u => u.Reviews)
                .ThenInclude(r => r.Book)
            .FirstOrDefaultAsync(u => u.Id == createReview.UserId);
        if (user == null) return null;
        var book = await _appDbContext.Books
            .FirstOrDefaultAsync(b => b.Id == createReview.BookId);
        if (book == null) return null;
        var doesReviewExistForBook = user.Reviews.FirstOrDefault(r => r.BookId == createReview.BookId);
        if (doesReviewExistForBook != null) return null;
        if (createReview.Rating < 1 || createReview.Rating > 5) return null;
        var newReview = new Review
        {
            Rating = createReview.Rating,
            Comment = createReview.Comment,
            UserId = createReview.UserId,
            BookId = createReview.BookId
        };
        await _appDbContext.Reviews.AddAsync(newReview);
        await _appDbContext.SaveChangesAsync();
        return new ReviewDetailDto
        {
            Id = newReview.Id,
            Rating = newReview.Rating,
            Comment = newReview.Comment,
            UserName = user.Name,
            BookTitle = book.Title
        };
    }

    public async Task<bool> DeleteReview(int reviewId, int userId)
    {
        var user = await _appDbContext.Users
            .Include(u => u.Reviews)
            .FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null) return false;
        var review = user.Reviews.FirstOrDefault(r => r.Id == reviewId);
        if (review == null) return false;
        _appDbContext.Remove(review);
        await _appDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<List<ReviewDetailDto>?> GetAllReviewsForBook(int bookId)
    {
        var book = await _appDbContext.Books
            .Include(b => b.Reviews)
                .ThenInclude(r => r.User)
            .FirstOrDefaultAsync(b => b.Id == bookId);
        if (book == null) return null;
        return book.Reviews.Select(r => new ReviewDetailDto
        {
            Id = r.Id,
            Rating = r.Rating,
            Comment = r.Comment,
            UserName = r.User?.Name ?? "Unknown",
            BookTitle = book.Title
        }).ToList();
    }
    public async Task<List<ReviewDetailDto>?> GetAllReviewsForUser(int userId)
    {
        var user = await _appDbContext.Users
            .Include(u => u.Reviews)
                .ThenInclude(r => r.Book)
            .FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null) return null;
        return user.Reviews.Select(r => new ReviewDetailDto
        {
            Id = r.Id,
            Rating = r.Rating,
            Comment = r.Comment,
            UserName = user.Name,
            BookTitle = r.Book?.Title ?? "Unknown"
        }).ToList();
    }
}
