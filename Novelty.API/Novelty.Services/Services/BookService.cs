using Microsoft.EntityFrameworkCore;
using Novelty.DataAccess;
using Novelty.Domain.Models;
using Novelty.DTOs.Book;
using Novelty.Services.Interfaces;

namespace Novelty.Services.Services;

public class BookService : IBookService
{
    /// <summary>
    /// dependency injection
    /// </summary>
    private readonly AppDbContext _appDbContext;
    /// <summary>
    /// constructor
    /// </summary>
    public BookService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    /// <summary>
    /// Checks if book exists
    /// Doesn't exist: creates a book and adds to database and returns BookDetailDTO
    /// Exists: returns null
    /// </summary>
    public async Task<BookDetailDto?> CreateBook(CreateBookDto createBook)
    {
        var doesBookExist = await _appDbContext.Books
            .AnyAsync(b => b.Title == createBook.Title && b.Author == createBook.Author);
        if (doesBookExist) return null;
        var newBook = new Book
        {
            Author = createBook.Author,
            Title = createBook.Title
        };
        await _appDbContext.Books.AddAsync(newBook);
        await _appDbContext.SaveChangesAsync();
        return new BookDetailDto
        {
            Author = newBook.Author,
            Title = newBook.Title,
            Id = newBook.Id,
            Reviews = newBook.Reviews.Select(r => new ReviewDetailDto
            {
                Id = r.Id,
                Rating = r.Rating,
                Comment = r.Comment,
                UserName = r.User?.Name ?? "Unknown",
                BookTitle = newBook.Title
            }).ToList(),
            AverageRating = RatingHelper.CalculateAverageRating(newBook.Reviews)
        };
    }

    /// <summary>
    /// Checks if book exists
    /// Doesn't exist: returns false
    /// Exists: removes from database and returns true
    /// </summary>
    public async Task<bool> DeleteBook(int bookId)
    {
        var book = await _appDbContext.Books
            .Include(b => b.Reviews)
            .FirstOrDefaultAsync(b => b.Id == bookId);

        if (book == null || book.Reviews.Count > 0) { return false; }
        ;

        _appDbContext.Remove(book);
        await _appDbContext.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Returns a list of BookSummaryDto 's for all books
    /// </summary>
    public async Task<List<BookSummaryDto>> GetAllBooks()
    {
        var books = await _appDbContext.Books
            .Include(b => b.Reviews)
            .ToListAsync();

        return books.Select(b => new BookSummaryDto
        {
            Id = b.Id,
            Title = b.Title,
            Author = b.Author,
            AverageRating = RatingHelper.CalculateAverageRating(b.Reviews)
        }).ToList();
    }

    /// <summary>
    /// Checks if book exists
    /// Doesn't exist: returns null
    /// Exists: returns BookDetailDto for book
    /// </summary>
    public async Task<BookDetailDto?> GetBookById(int bookId)
    {
        var book = await _appDbContext.Books
            .Where(b => b.Id == bookId)
            .Include(b => b.Reviews)
                .ThenInclude(r => r.User)
            .FirstOrDefaultAsync();
        if (book == null) return null;

        return new BookDetailDto
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Reviews = book.Reviews.Select(r => new ReviewDetailDto
            {
                Id = r.Id,
                Rating = r.Rating,
                Comment = r.Comment,
                UserName = r.User?.Name ?? "Unknown",
                BookTitle = book.Title
            }).ToList(),
            AverageRating = RatingHelper.CalculateAverageRating(book.Reviews)
        };
    }
}

