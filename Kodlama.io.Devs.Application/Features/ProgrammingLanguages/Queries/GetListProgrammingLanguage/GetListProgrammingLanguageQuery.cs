using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Dtos;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Models;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Queries.GetListProgrammingLanguage
{
    public class GetListProgrammingLanguageQuery:IRequest<ProgrammingLanguageListModel>
    {
        public PageRequest PageRequest { get; set; }
        public class GetListProgrammingLanguageHandler : IRequestHandler<GetListProgrammingLanguageQuery, ProgrammingLanguageListModel>
		{
			private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
			private readonly IMapper _mapper;

			public GetListProgrammingLanguageHandler(IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper)
			{
				_programmingLanguageRepository = programmingLanguageRepository;
				_mapper = mapper;
			}

			public async Task<ProgrammingLanguageListModel> Handle(GetListProgrammingLanguageQuery request, CancellationToken cancellationToken)
			{
				IPaginate<ProgrammingLanguage> paginate = await _programmingLanguageRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);

				ProgrammingLanguageListModel programmingLanguageListModel = _mapper.Map<ProgrammingLanguageListModel>(paginate);

				return programmingLanguageListModel;
			}
		}
	}
}
