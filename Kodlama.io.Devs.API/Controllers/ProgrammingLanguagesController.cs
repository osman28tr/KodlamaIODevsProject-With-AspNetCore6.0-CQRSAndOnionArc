using Core.Application.Requests;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Dtos;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Models;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Queries.GetByIdProgrammingLanguage;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Queries.GetListProgrammingLanguage;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
		[HttpGet]
		public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
		{
			GetListProgrammingLanguageQuery getListProgrammingLanguageQuery = new() { PageRequest = pageRequest };
			ProgrammingLanguageListModel result = await _mediator.Send(getListProgrammingLanguageQuery);
			return Ok(result);
		}
		[HttpGet("{Id}")]
		public async Task<IActionResult> Get([FromQuery] int id)
		{
			GetByIdProgrammingLanguageDto getByIdProgrammingLanguageDto = await _mediator.Send(new GetByIdProgrammingLanguageQuery { Id = id });
			return Ok(getByIdProgrammingLanguageDto);
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete([FromQuery] int id)
		{
			DeletedProgrammingLanguageDto deletedProgrammingLanguageDto = await _mediator.Send(new DeleteProgrammingLanguageCommand { Id = id });
			return Ok(deletedProgrammingLanguageDto);
		}
		[HttpPut]
		public async Task<IActionResult> Update([FromBody] UpdateProgrammingLanguageCommand updateProgrammingLanguageCommand)
		{
			UpdatedProgrammingLanguageDto updatedProgrammingLanguageDto = await _mediator.Send(updateProgrammingLanguageCommand);
			return Ok(updatedProgrammingLanguageDto);
		}
	}
}
