using Novelty.DTOs.Book;

namespace Novelty.Services.Interfaces;

public interface IBookService
{
    Task<List<BookSummaryDto>> GetAllBooks();
    Task<BookDetailDto?> GetBookById(int bookId);
    Task<BookDetailDto?> CreateBook(CreateBookDto createBook);
    Task<bool> DeleteBook(int bookId);
}
