using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly TokenService _tokenService;

		public AuthController(TokenService tokenService)
		{
			_tokenService = tokenService;
		}

		// Endpoint to login and get a JWT token
		[HttpPost("login")]
		public IActionResult Login([FromBody] LoginRequest request)
		{
			// For demonstration, we’re hardcoding the username and password.
			// In a real application, validate against a database or another user store.
			if (request.Username == "testuser" && request.Password == "password123")
			{
				var token = _tokenService.GenerateToken(request.Username);
				return Ok(new { Token = token });
			}

			return Unauthorized("Invalid credentials");
		}

		// A secure endpoint that requires a valid JWT token
		[Authorize]
		[HttpGet("secure-data")]
		public IActionResult GetSecureData()
		{
			return Ok("This is secure data accessible only with a valid token.");
		}
	}

	public class LoginRequest
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}
}
