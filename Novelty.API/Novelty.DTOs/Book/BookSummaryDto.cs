using System;
using System.Collections.Generic;
using System.Text;

namespace Novelty.DTOs.Book;

public class BookSummaryDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public double AverageRating { get; set; }
}
