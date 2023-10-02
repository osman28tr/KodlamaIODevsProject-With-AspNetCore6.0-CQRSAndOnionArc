using AutoMapper;
using Kodlama.io.Devs.Domain.Entities;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;
using Core.Persistence.Paging;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Models;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Queries.GetByIdProgrammingLanguage;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage;

namespace Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Profiles
{
	public class MappingProfiles:Profile
	{
        public MappingProfiles()
        {
            CreateMap<ProgrammingLanguage,CreatedProgrammingLanguageDto>().ReverseMap();
			CreateMap<ProgrammingLanguage, CreateProgrammingLanguageCommand>().ReverseMap();
			CreateMap<IPaginate<ProgrammingLanguage>, ProgrammingLanguageListModel>().ReverseMap();
			CreateMap<ProgrammingLanguage, GetByIdProgrammingLanguageDto>().ReverseMap();
			CreateMap<ProgrammingLanguage, GetByIdProgrammingLanguageQuery>().ReverseMap();
			CreateMap<ProgrammingLanguage, DeletedProgrammingLanguageDto>().ReverseMap();
			CreateMap<ProgrammingLanguage, UpdatedProgrammingLanguageDto>().ReverseMap();
			CreateMap<UpdateProgrammingLanguageCommand, ProgrammingLanguage>().ReverseMap();
		}
    }
}
