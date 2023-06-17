using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace StarWarsAPI.Controllers;

public class BaseController: ControllerBase
{
    internal int? UserId => int.TryParse(User.Claims.SingleOrDefault(p => p.Type == ClaimTypes.NameIdentifier)?.Value, out int userId) ? userId : null;
}