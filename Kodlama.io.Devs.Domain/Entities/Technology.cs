using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Domain.Entities
{
	public class Technology:Entity
	{
        public int Id { get; set; }
        public int ProgrammingLanguageId { get; set; }
        public string Name { get; set; }
        public Technology()
        {
            
        }
        public Technology(int id,int programmingLanguageId,string name):base(id)
        {
            Id = id;
            Name = name;
            ProgrammingLanguageId = programmingLanguageId;
        }
        public ProgrammingLanguage? ProgrammingLanguage { get; set; }
    }
}
