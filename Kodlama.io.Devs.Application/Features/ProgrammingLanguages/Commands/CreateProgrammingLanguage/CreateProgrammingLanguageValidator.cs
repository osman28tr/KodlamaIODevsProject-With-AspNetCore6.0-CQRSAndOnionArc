using FluentValidation;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage
{
	public class CreateProgrammingLanguageValidator:AbstractValidator<CreateProgrammingLanguageCommand>
	{
        public CreateProgrammingLanguageValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
        }
    }
}
