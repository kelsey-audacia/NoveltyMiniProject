using Microsoft.AspNetCore.Mvc;
using Novelty.DTOs.Book;
using Novelty.Services.Interfaces;
using Novelty.Services.Services;
namespace Novelty.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FavouritesController : ControllerBase
{
    private readonly IFavouriteService _favouriteService;
    public FavouritesController(IFavouriteService favouriteService)
    {
        _favouriteService = favouriteService;
    }
    [HttpGet("/api/users/{userId}/favourites")]
    public async Task<IActionResult> GetAllFavouritesForUser(int userId)
    {
        var favourites = await _favouriteService.GetAllFavouritesForUser(userId);
        if (favourites == null) return NotFound();
        return Ok(favourites);
    }
    [HttpPost("/api/users/{userId}/favourites")]
    public async Task<IActionResult> CreateFavourite([FromBody] CreateFavouriteDto createFavourite)
    {
        var favourite = await _favouriteService.CreateFavourite(createFavourite);
        if (favourite == null) return BadRequest();
        return CreatedAtAction("CreateFavourite", new { id = favourite.Id }, favourite);
    }
    [HttpDelete("/api/users/{userId}/favourites/{bookId}")]
    public async Task<IActionResult> DeleteFavourite(int userId, int bookId)
    {
        var favourite = await _favouriteService.DeleteFavourite(userId, bookId);
        if (!favourite) return NotFound();
        return NoContent();
    }
}
