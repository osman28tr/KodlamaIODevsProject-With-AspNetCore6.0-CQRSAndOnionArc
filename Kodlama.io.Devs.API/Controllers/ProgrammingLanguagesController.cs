using Kodlama.io.Devs.Application.Features.ProgrammingLanguage.Commands.CreateProgrammingLanguage;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kodlama.io.Devs.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProgrammingLanguagesController : ControllerBase
	{
		private readonly IMediator _mediator;

		public ProgrammingLanguagesController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		public async Task<IActionResult> Add([FromBody] CreateProgrammingLanguageCommand createProgrammingLanguageCommand)
		{
			CreatedProgrammingLanguageDto result = await _mediator.Send(createProgrammingLanguageCommand);
			return Created("", result);
		}
	}
}
