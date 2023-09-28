using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Models
{
	public class ProgrammingLanguageListModel:BasePageableModel
	{
        public IList<Kodlama.io.Devs.Domain.Entities.ProgrammingLanguage> Items { get; set; }
    }
}
