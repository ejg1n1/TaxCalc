using Application.Interfaces;
using Application.Models.REST.Request;
using Application.Models.REST.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class UsersController : BaseController
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Get an ApplicationUser for given userId
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet("{userId:Guid}")]
    public async Task<ActionResult<UserResponse>> Get(Guid userId)
    {
        return Ok(await _userService.Get(userId));
    }

    /// <summary>
    /// Create a new application user
    /// </summary>
    /// <param name="userRequest"></param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<UserResponse>> Create([FromBody] UserRequest userRequest)
    {
        return Ok(await _userService.Create(userRequest));
    }

    /// <summary>
    /// Authenticate a applicationUser
    /// </summary>
    /// <returns></returns>
    [HttpPost("Authenticate")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthenticateResponse>> Authenticate(
        [FromBody] AuthenticateRequest authenticateRequest)
    {
        return Ok(await _userService.Login(authenticateRequest));
    }

    /// <summary>
    /// Patch an ApplicationUser properties
    /// </summary>
    /// <returns></returns>
    [HttpPatch("userId:Guid")]
    [Authorize("Admin")]
    public async Task<ActionResult<UserResponse>> UpdateProperties(Guid userId, 
        [FromBody] JsonPatchDocument<UserRequest> patchDocument)
    {
        return Ok(await _userService.UpdateProperties(userId, patchDocument));
    }
    
    /// <summary>
    /// Delete a User for a given userId
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>200 Status Code</returns>
    [HttpDelete("{userId:Guid}")]
    public async Task<IActionResult> Delete(Guid userId)
    {
        await _userService.Delete(userId);
        return Ok();
    }
}