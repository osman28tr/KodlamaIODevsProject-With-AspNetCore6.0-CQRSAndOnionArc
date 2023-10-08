using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using Kodlama.io.Devs.Application.Features.Auths.Commands.CreateUser;
using Kodlama.io.Devs.Application.Features.Auths.Commands.LoginUser;
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

			SetAccessTokenToCookie(result.AccessToken);

			return Ok(result.AccessToken);
		}
		[HttpPost("Login")]
		public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
		{
			LoginUserCommand loginUserCommand = new() { UserForLoginDto = userForLoginDto, IpAddress = GetIpAddress() };
			var result = await _mediator.Send(loginUserCommand);
			SetAccessTokenToCookie(result.AccessToken);
			return Ok(result.AccessToken);
		}
		private void SetAccessTokenToCookie(AccessToken accessToken)
		{
			CookieOptions cookieOptions = new() { HttpOnly = true, Expires = DateTime.Now.AddDays(1) };
			Response.Cookies.Append("accessToken", accessToken.Token, cookieOptions);
		}
		private string? GetIpAddress()
		{
			if (Request.Headers.ContainsKey("X-Forwarded-For")) return Request.Headers["X-Forwarded-For"];
			return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
		}
	}
}
