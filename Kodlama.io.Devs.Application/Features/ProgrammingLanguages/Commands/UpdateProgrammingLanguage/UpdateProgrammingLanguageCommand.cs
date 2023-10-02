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

namespace Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage
{
	public class UpdateProgrammingLanguageCommand:IRequest<UpdatedProgrammingLanguageDto>
	{
        public int Id { get; set; }
        public string Name { get; set; }
		public class UpdateProgrammingLanguageHandler : IRequestHandler<UpdateProgrammingLanguageCommand, UpdatedProgrammingLanguageDto>
		{
			private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
			private readonly IMapper _mapper;

			public UpdateProgrammingLanguageHandler(IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper)
			{
				_programmingLanguageRepository = programmingLanguageRepository;
				_mapper = mapper;
			}
			public async Task<UpdatedProgrammingLanguageDto> Handle(UpdateProgrammingLanguageCommand request, CancellationToken cancellationToken)
			{
				ProgrammingLanguage mappedProgrammingLanguage = _mapper.Map<ProgrammingLanguage>(request);
				var updatedProgrammingLanguage = await _programmingLanguageRepository.UpdateAsync(mappedProgrammingLanguage);
				UpdatedProgrammingLanguageDto updatedProgrammingLanguageDto = _mapper.Map<UpdatedProgrammingLanguageDto>(updatedProgrammingLanguage);
				return updatedProgrammingLanguageDto;
			}
		}
	}
}
