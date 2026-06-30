using System;
using System.Collections.Generic;
using System.Text;

namespace Novelty.Domain.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public List<Review> Reviews { get; set; } = new();
}
