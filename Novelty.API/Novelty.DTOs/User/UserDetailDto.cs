
namespace Novelty.DTOs.Book;

public class UserDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<ReviewDetailDto> Reviews { get; set; } = new();
    public List<FavouriteDetailDto> Favourites { get; set; } = new();
}
