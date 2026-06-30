
namespace Novelty.DTOs.Book;

public class ReviewDetailDto
{
    public int Id { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string BookTitle { get; set; } = string.Empty;
}
