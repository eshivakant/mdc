using MDC.AuthService.Domain;
using MDC.ContributionService.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MDC.AuthService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Domain.AuthService _authService;

        public UserController(Domain.AuthService authService)
        {
            this._authService = authService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] AuthRequest user)
        {
            var token = _authService.Authenticate(user.Login, user.Password);

            if (token == null)
                return BadRequest(new {message = "Username of password incorrect"});

            return Ok(token);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_authService.UserFromLogin(HttpContext.User.Identity.Name));
        }
    }
}