using Microsoft.AspNetCore.Mvc;
using Novelty.DataAccess;
using Novelty.DTOs.Book;
using Novelty.Services.Interfaces;

namespace Novelty.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    /// <summary>
    /// dependency injection
    /// </summary>
    private readonly IBookService _bookService;
    /// <summary>
    /// constructor
    /// </summary>
    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        var books = await _bookService.GetAllBooks();
        return Ok(books);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById(int id)
    {
        var book = await _bookService.GetBookById(id);
        if (book == null) return NotFound();
        return Ok(book);
    }
    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookDto createBook)
    {
        var book = await _bookService.CreateBook(createBook);
        if (book == null) return BadRequest();
        return CreatedAtAction("CreateBook", new {id = book.Id}, book);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _bookService.DeleteBook(id);
        if (!book) return NotFound();
        return NoContent();
    }
}
