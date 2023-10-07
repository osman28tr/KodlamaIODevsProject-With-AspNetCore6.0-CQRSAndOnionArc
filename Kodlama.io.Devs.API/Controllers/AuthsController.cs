using Core.Security.Dtos;
using Core.Security.Entities;
using Kodlama.io.Devs.Application.Features.Auths.Commands.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kodlama.io.Devs.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthsController : ControllerBase
	{
		private readonly IMediator _mediator;
		public AuthsController(IMediator mediator)
		{
			_mediator = mediator;
		}
		[HttpPost]
		public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
		{
			CreateUserCommand createUserCommand = new() { UserForRegisterDto = userForRegisterDto, IpAddress = GetIpAddress() };
			var result = await _mediator.Send(createUserCommand);

			SetRefreshTokenToCookie(result.RefreshToken);

			return Ok(result.AccessToken);
		}
		private void SetRefreshTokenToCookie(RefreshToken refreshToken)
		{
			CookieOptions cookieOptions = new() { HttpOnly = true, Expires = DateTime.Now.AddDays(1) };
			Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
		}
		private string? GetIpAddress()
		{
			if (Request.Headers.ContainsKey("X-Forwarded-For")) return Request.Headers["X-Forwarded-For"];
			return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
		}
	}
}
