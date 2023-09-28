using AutoMapper;
using Kodlama.io.Devs.Domain.Entities;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguage.Commands.CreateProgrammingLanguage;
using Core.Persistence.Paging;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Models;

namespace Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Profiles
{
	public class MappingProfiles:Profile
	{
        public MappingProfiles()
        {
            CreateMap<Kodlama.io.Devs.Domain.Entities.ProgrammingLanguage,CreatedProgrammingLanguageDto>().ReverseMap();
			CreateMap<Kodlama.io.Devs.Domain.Entities.ProgrammingLanguage, CreateProgrammingLanguageCommand>().ReverseMap();
			CreateMap<IPaginate<Kodlama.io.Devs.Domain.Entities.ProgrammingLanguage>, ProgrammingLanguageListModel>().ReverseMap();

		}
    }
}
