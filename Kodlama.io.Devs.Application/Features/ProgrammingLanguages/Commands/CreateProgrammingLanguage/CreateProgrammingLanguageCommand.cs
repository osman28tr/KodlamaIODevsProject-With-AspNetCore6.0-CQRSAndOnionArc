using AutoMapper;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Dtos;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.ProgrammingLanguage.Commands.CreateProgrammingLanguage
{
	public class CreateProgrammingLanguageCommand:IRequest<CreatedProgrammingLanguageDto>
	{
        public string Name { get; set; }
		public class CreateProgrammingLanguageHandler : IRequestHandler<CreateProgrammingLanguageCommand, CreatedProgrammingLanguageDto>
		{
			private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
			private readonly IMapper _mapper;

			public CreateProgrammingLanguageHandler(IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper)
			{
				_programmingLanguageRepository = programmingLanguageRepository;
				_mapper = mapper;
			}

			public async Task<CreatedProgrammingLanguageDto> Handle(CreateProgrammingLanguageCommand request, CancellationToken cancellationToken)
			{
				var mappedProgrammingLanguage = _mapper.Map<Kodlama.io.Devs.Domain.Entities.ProgrammingLanguage>(request);
				var programmingLanguage =  await _programmingLanguageRepository.AddAsync(mappedProgrammingLanguage);
				CreatedProgrammingLanguageDto createdProgrammingLanguageDto = _mapper.Map<CreatedProgrammingLanguageDto>(programmingLanguage);
				return createdProgrammingLanguageDto;
			}
		}
	}
}
