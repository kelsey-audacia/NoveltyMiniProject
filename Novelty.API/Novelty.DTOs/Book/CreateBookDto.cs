using System;
using System.Collections.Generic;
using System.Text;

namespace Novelty.DTOs.Book;

public class CreateBookDto
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
}
