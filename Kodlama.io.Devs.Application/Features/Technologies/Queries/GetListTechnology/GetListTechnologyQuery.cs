using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Kodlama.io.Devs.Application.Features.Technologies.Models;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Technologies.Queries.GetListTechnology
{
	public class GetListTechnologyQuery:IRequest<TechnologyListModel>
	{
        public PageRequest PageRequest { get; set; }
		public class GetListTechnologyHandler : IRequestHandler<GetListTechnologyQuery, TechnologyListModel>
		{
			private readonly ITechnologyRepository _technologyRepository;
			private readonly IMapper _mapper;

			public GetListTechnologyHandler(ITechnologyRepository technologyRepository, IMapper mapper)
			{
				_technologyRepository = technologyRepository;
				_mapper = mapper;
			}

			public async Task<TechnologyListModel> Handle(GetListTechnologyQuery request, CancellationToken cancellationToken)
			{
				IPaginate<Technology> paginate = await _technologyRepository.GetListAsync(include:
					t=>t.Include(c=>c.ProgrammingLanguage),
					index: request.PageRequest.Page,
					size: request.PageRequest.PageSize);
				TechnologyListModel technologyListModel = _mapper.Map<TechnologyListModel>(paginate);
				return technologyListModel;
			}
		}
	}
}
