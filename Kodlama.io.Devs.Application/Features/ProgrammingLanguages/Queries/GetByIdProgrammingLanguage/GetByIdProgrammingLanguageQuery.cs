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

namespace Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Queries.GetByIdProgrammingLanguage
{
	public class GetByIdProgrammingLanguageQuery:IRequest<GetByIdProgrammingLanguageDto>
	{
        public int Id { get; set; }
		public class GetByIdProgrammingLanguageHandler : IRequestHandler<GetByIdProgrammingLanguageQuery, GetByIdProgrammingLanguageDto>
		{
			private readonly IProgrammingLanguageRepository _repository;
			private readonly IMapper _mapper;

			public GetByIdProgrammingLanguageHandler(IProgrammingLanguageRepository repository, IMapper mapper)
			{
				_repository = repository;
				_mapper = mapper;
			}

			public async Task<GetByIdProgrammingLanguageDto> Handle(GetByIdProgrammingLanguageQuery request, CancellationToken cancellationToken)
			{
				ProgrammingLanguage programmingLanguage = await _repository.GetAsync(p => p.Id == request.Id);
				GetByIdProgrammingLanguageDto getByIdProgrammingLanguageDto = _mapper.Map<GetByIdProgrammingLanguageDto>(programmingLanguage);
				return getByIdProgrammingLanguageDto;
			}
		}
	}
}
