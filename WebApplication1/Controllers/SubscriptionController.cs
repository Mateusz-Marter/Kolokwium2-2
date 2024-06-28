using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTO;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/subscription")]
public class SubscriptionController : ControllerBase
{
    private readonly ISubscriptionService _subscriptionService;
    private readonly IUserService _userService;

    public SubscriptionController(ISubscriptionService subscriptionService, IUserService userService)
    {
        _subscriptionService = subscriptionService;
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> PostSubscription(NewServiceSubscription newServiceSubscription)
    {
        if (newServiceSubscription.leftToPay <= 0)
        {
            return BadRequest("leftToPay <= 0");
        }

        if (!await _subscriptionService.ServisExists(newServiceSubscription.idService))
        {
            return NotFound("Service doesn't exist");
        }

        if (!await _userService.UserExists(newServiceSubscription.idUser))
        {
            return NotFound("User doesn't exist");
        }

        if (!await _subscriptionService.IsUserSubscribedToService(newServiceSubscription.idUser, newServiceSubscription.idService))
        {
            return BadRequest("User already subscribed to service");
        }

        _subscriptionService.AddSubscribtion(newServiceSubscription);

        return Ok();
    }
}