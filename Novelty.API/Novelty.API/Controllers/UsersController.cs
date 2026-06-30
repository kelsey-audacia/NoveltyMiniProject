using Microsoft.AspNetCore.Mvc;
using Novelty.DTOs.User;
using Novelty.Services.Interfaces;
using Novelty.Services.Services;

namespace Novelty.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsers();
        return Ok(users);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _userService.GetUserById(id);
        if (user == null) return NotFound();
        return Ok(user);
    }
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUser)
    {
        var user = await _userService.CreateUser(createUser);
        if (user == null) return BadRequest();
        return CreatedAtAction("CreateUser", new { id = user.Id }, user);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto updateUser)
    {
        var user = await _userService.UpdateUser(id, updateUser);
        if (!user) return NotFound();
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _userService.DeleteUser(id);
        if (!user) return NotFound();
        return NoContent();
    }
}
