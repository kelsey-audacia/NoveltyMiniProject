
namespace Novelty.DTOs.Book;

public class FavouriteDetailDto
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public BookSummaryDto BookSummary { get; set; } = new();
}
