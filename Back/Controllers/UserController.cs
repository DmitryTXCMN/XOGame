using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Back.Controllers;

[ApiController]
[Route("[controller]"), OpenIdDictAuthorize]
public class UserController : ControllerBase
{
    private readonly UserManager<IdentityUser<Guid>> _userManager;

    public UserController(UserManager<IdentityUser<Guid>> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet("GetUserInfo")]
    public async Task<IActionResult> GetUserInfo()
    {
        var identity = HttpContext.User.Identity;

        if (identity?.Name is null)
            throw new Exception("User is not logged in");

        var user = await _userManager.FindByNameAsync(identity.Name) ?? throw new Exception($"User Name: {identity.Name} not found");

        return Ok(new { user.Id, user.UserName });
    }
}