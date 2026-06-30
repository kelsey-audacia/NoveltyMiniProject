using Microsoft.AspNetCore.Mvc;
using Moq;
using Novelty.API.Controllers;
using Novelty.DTOs.Book;
using Novelty.Services.Interfaces;
using Xunit;

namespace Novelty.Tests.ControllerTests;

public class BooksControllerTests
{
    private readonly Mock<IBookService> _mockBookService;
    private readonly BooksController _booksController;

    public BooksControllerTests()
    {
        _mockBookService = new Mock<IBookService>();
        _booksController = new BooksController(_mockBookService.Object);
    }
    [Fact]
    public async Task GetAllBooks_WhenBooksExist_ReturnsListOfBooks()
    {
        // arrange
        _mockBookService
            .Setup(s => s.GetAllBooks())
            .ReturnsAsync(new List<BookSummaryDto>
            {
                new BookSummaryDto { Id = 1, Title = "Test Book", Author = "Test Author", AverageRating = 4.5 },
                new BookSummaryDto { Id = 2, Title = "Test Book 2", Author = "Test Author 2", AverageRating = 3.0 }

            });
        // act
        var result = await _booksController.GetAllBooks();
        // assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var books = Assert.IsType<List<BookSummaryDto>>(okResult.Value);
        Assert.Equal(2, books.Count);
    }
    [Fact]
    public async Task GetAllBooks_WhenNoBooksExist_ReturnsEmptyList()
    {
        // arrange
        _mockBookService
            .Setup(s => s.GetAllBooks())
            .ReturnsAsync(new List<BookSummaryDto>());
        // act
        var result = await _booksController.GetAllBooks();
        // assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var books = Assert.IsType<List<BookSummaryDto>>(okResult.Value);
        Assert.Empty(books);
    }
    [Fact]
    public async Task GetBookById_WhenBookExists_ReturnsBook()
    {
        // arrange
        _mockBookService
            .Setup(s => s.GetBookById(1))
            .ReturnsAsync(new BookDetailDto { Id = 1, Title = "Test Book", Author = "Test Author", AverageRating = 4.5 });
        // act
        var mockId = 1;
        var result = await _booksController.GetBookById(mockId);
        // assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var book = Assert.IsType < BookDetailDto>(okResult.Value);
        Assert.Equal(1, book.Id);
    }
    [Fact]
    public async Task GetBookById_WhenBookDoesNotExist_ReturnsNotFound()
    {
        // arrange
        _mockBookService
            .Setup(s => s.GetBookById(1))
            .ReturnsAsync((BookDetailDto?)null); 
        // act
        var mockId = 1;
        var result = await _booksController.GetBookById(mockId);
        // assert
        Assert.IsType<NotFoundResult>(result);
    }
    [Fact]
    public async Task CreateBook_WhenBookIsNew_ReturnsCreatedBook()
    {
        // arrange
        _mockBookService
            .Setup(s => s.CreateBook(It.IsAny<CreateBookDto>()))
            .ReturnsAsync(new BookDetailDto { Id = 1, Title = "Test Book", Author = "Test Author" }); 
        // act
        var result = await _booksController.CreateBook(new CreateBookDto { Title = "Test Book", Author = "Test Author" });
        // assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        var book = Assert.IsType<BookDetailDto>(createdResult.Value);
        Assert.Equal(1, book.Id);
    }
    [Fact]
    public async Task CreateBook_WhenBookAlreadyExists_ReturnsBadRequest()
    {
        // arrange
        _mockBookService
            .Setup(s => s.CreateBook(It.IsAny<CreateBookDto>()))
            .ReturnsAsync((BookDetailDto?)null);
        // act
        var result = await _booksController.CreateBook(new CreateBookDto());
        // assert
        Assert.IsType<BadRequestResult>(result);
    }
}
