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

namespace Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage
{
	public class DeleteProgrammingLanguageCommand:IRequest<DeletedProgrammingLanguageDto>
	{
        public int Id { get; set; }
		public class DeleteProgrammingLanguageHandler : IRequestHandler<DeleteProgrammingLanguageCommand, DeletedProgrammingLanguageDto>
		{
			private readonly IMapper _mapper;
			private readonly IProgrammingLanguageRepository _programmingLanguageRepository;

			public DeleteProgrammingLanguageHandler(IMapper mapper, IProgrammingLanguageRepository programmingLanguageRepository)
			{
				_mapper = mapper;
				_programmingLanguageRepository = programmingLanguageRepository;
			}
			public async Task<DeletedProgrammingLanguageDto> Handle(DeleteProgrammingLanguageCommand request, CancellationToken cancellationToken)
			{
				ProgrammingLanguage programmingLanguage = await _programmingLanguageRepository.GetAsync(p => p.Id == request.Id);
				var deletedProgrammingLanguage = await _programmingLanguageRepository.DeleteAsync(programmingLanguage);
				DeletedProgrammingLanguageDto deletedProgrammingLanguageDto = _mapper.Map<DeletedProgrammingLanguageDto>(deletedProgrammingLanguage);
				return deletedProgrammingLanguageDto;
			}
		}
	}
}
