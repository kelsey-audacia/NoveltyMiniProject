
namespace Novelty.DTOs.Book;

public class CreateReviewDto
{
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public int UserId { get; set; }
    public int BookId { get; set; }
}
