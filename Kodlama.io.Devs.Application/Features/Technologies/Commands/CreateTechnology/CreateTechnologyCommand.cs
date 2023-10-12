using AutoMapper;
using Kodlama.io.Devs.Application.Features.Technologies.Dtos;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Technologies.Commands.CreateTechnology
{
	public class CreateTechnologyCommand:IRequest<CreatedTechnologyDto>
	{
        public int ProgrammingLanguageId { get; set; }
        public string Name { get; set; }
		public class CreateTechnologyCommandHandler : IRequestHandler<CreateTechnologyCommand, CreatedTechnologyDto>
		{
			private readonly ITechnologyRepository _technologyRepository;
			private readonly IMapper _mapper;

			public CreateTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper)
			{
				_technologyRepository = technologyRepository;
				_mapper = mapper;
			}

			public async Task<CreatedTechnologyDto> Handle(CreateTechnologyCommand request, CancellationToken cancellationToken)
			{
				Technology technology = _mapper.Map<Technology>(request);
				var createdTechnology = await _technologyRepository.AddAsync(technology);
				CreatedTechnologyDto createdTechnologyDto = _mapper.Map<CreatedTechnologyDto>(createdTechnology);
				return createdTechnologyDto;
			}
		}
	}
}
