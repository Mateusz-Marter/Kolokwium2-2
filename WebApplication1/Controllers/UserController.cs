using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("commitment")]
    public async Task<IActionResult> getCommitments(int idUser, string orderBy)
    {
        if (!await _userService.UserExists(idUser))
        {
            return NotFound("User doesn't exist");
        }

        if (!(orderBy.Equals("PaymentDeadline") || orderBy.Equals("LeftToPay")))
        {
            return BadRequest("Order by is not \"PaymentDeadline\" or \"LeftToPay\"");
        }

        var data = await _userService.GetCommitments(idUser, orderBy);

        return Ok(data);
    }
}