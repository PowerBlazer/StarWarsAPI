using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StarWarsAPI.DTOs;
using StarWarsAPI.Services;

namespace StarWarsAPI.Controllers.V1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]"),ApiVersion("1.0")]
public class AuthorizationController: BaseController
{
    private readonly AuthorizationService _authorizationService;
    
    public AuthorizationController(AuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]AuthorizationDto loginObj)
    {
        var authorizationResult = await _authorizationService
            .Login(loginObj.Email!, loginObj.Password!);

        if (authorizationResult.IsSuccess)
        {
            return Ok(new
            {
                AccessToken = authorizationResult.Token
            });
        }

        return BadRequest(new
        {
            Errors = authorizationResult.Errors
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody]AuthorizationDto regObj)
    {
        var authorizationResult = await _authorizationService
            .Register(regObj.Email!, regObj.Password!);
        
        if (authorizationResult.IsSuccess)
        {
            return Ok(new
            {
                AccessToken = authorizationResult.Token
            });
        }

        return BadRequest(new
        {
            Errors = authorizationResult.Errors
        });
    }
}