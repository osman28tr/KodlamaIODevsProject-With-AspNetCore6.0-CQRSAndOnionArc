using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Persistance.Repositories
{
	public class Entity
	{
        public int Id { get; set; }
        public Entity(int id)
        {
            Id = id;
        }
    }
}
