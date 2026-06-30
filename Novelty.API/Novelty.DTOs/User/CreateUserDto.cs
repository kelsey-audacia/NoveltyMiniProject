using System;
using System.Collections.Generic;
using System.Text;

namespace Novelty.DTOs.User;

public class CreateUserDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

}
