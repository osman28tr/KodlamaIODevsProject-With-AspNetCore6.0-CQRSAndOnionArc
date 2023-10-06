using Core.Application.Requests;
using Kodlama.io.Devs.Application.Features.Technologies.Queries.GetListTechnology;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kodlama.io.Devs.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TechnologiesController : ControllerBase
	{
		private readonly IMediator _mediator;
        public TechnologiesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
		public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
		{
			GetListTechnologyQuery getListTechnologyQuery = new GetListTechnologyQuery { PageRequest = pageRequest };
			var result = await _mediator.Send(getListTechnologyQuery);
			return Ok(result);
		}
	}
}
