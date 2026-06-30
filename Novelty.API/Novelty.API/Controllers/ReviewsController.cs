using Microsoft.AspNetCore.Mvc;
using Novelty.Domain.Models;
using Novelty.DTOs.Book;
using Novelty.Services.Interfaces;
using Novelty.Services.Services;

namespace Novelty.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviewService;
    public ReviewsController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpGet("/api/books/{bookId}/reviews")]
    public async Task<IActionResult> GetAllReviewsForBook(int bookId)
    {
        var reviews = await _reviewService.GetAllReviewsForBook(bookId);
        if (reviews == null) return NotFound();
        return Ok(reviews);
    }
    [HttpGet("/api/users/{userId}/reviews")]
    public async Task<IActionResult> GetAllReviewsForUser(int userId)
    {
        var reviews = await _reviewService.GetAllReviewsForUser(userId);
        if (reviews == null) return NotFound();
        return Ok(reviews);
    }
    [HttpPost("/api/books/{bookId}/reviews")]
    public async Task<IActionResult> CreateReview([FromBody] CreateReviewDto createReview)
    {
        var review = await _reviewService.CreateReview(createReview);
        if (review == null) return BadRequest();
        return CreatedAtAction("CreateReview", new { id = review!.Id }, review);
    }
    [HttpDelete("/api/users/{userId}/reviews/{reviewId}")]
    public async Task<IActionResult> DeleteReview(int userId, int reviewId)
    {
        var review = await _reviewService.DeleteReview(reviewId, userId);
        if (!review) return NotFound();
        return NoContent();
    }
}
