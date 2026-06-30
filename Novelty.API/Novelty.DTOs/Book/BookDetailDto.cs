namespace Novelty.DTOs.Book;

public class BookDetailDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public List<ReviewDetailDto> Reviews { get; set; } = new();
    public double AverageRating { get; set; }
}
