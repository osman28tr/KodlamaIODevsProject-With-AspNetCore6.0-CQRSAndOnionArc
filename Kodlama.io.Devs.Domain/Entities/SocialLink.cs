using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Domain.Entities
{
	public class SocialLink:Entity
	{
		public int AppUserId { get; set; }
		public string Title { get; set; }
		public string Link { get; set; }

		public virtual AppUser AppUser { get; set; }

		public SocialLink()
		{

		}

		public SocialLink(int id, int appUserId, string title, string link) : this()
		{
			Id = id;
			AppUserId = appUserId;
			Title = title;
			Link = link;
		}
	}
}
