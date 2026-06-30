using System;
using System.Collections.Generic;
using System.Text;

namespace Novelty.Domain.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<Review> Reviews { get; set; } = new();
    public List<Favourite> Favourites { get; set; } = new();
}
