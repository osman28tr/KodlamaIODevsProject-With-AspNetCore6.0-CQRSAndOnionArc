using Core.Security.Entities;
using Kodlama.io.Devs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Persistance.Contexts
{
	public class BaseDbContext:DbContext
	{
		protected IConfiguration Configuration { get; set; }
		public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
        public DbSet<Technology> Technologies { get; set; }
		public DbSet<AppUser> AppUsers { get; set; }
		public DbSet<User> Users { get; set; }

		public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
		{
			Configuration = configuration;
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<AppUser>(a =>
			{
				a.ToTable("AppUsers");
				a.Property(p => p.Id).HasColumnName("Id");
			});

			modelBuilder.Entity<ProgrammingLanguage>(a =>
			{
				a.ToTable("ProgrammingLanguages").HasKey(k => k.Id);
				a.Property(p => p.Id).HasColumnName("Id");
				a.Property(p => p.Name).HasColumnName("Name");
				a.HasMany(p => p.Technologies);
			});
			ProgrammingLanguage[] programmingLanguageSeedData = { new(1, "C#"), new(2, "JavaScript") };
			modelBuilder.Entity<ProgrammingLanguage>().HasData(programmingLanguageSeedData);

			modelBuilder.Entity<Technology>(t =>
			{
				t.ToTable("Technologies").HasKey(k => k.Id);
				t.Property(p => p.Id).HasColumnName("Id");
				t.Property(p => p.Name).HasColumnName("Name");
				t.Property(p => p.ProgrammingLanguageId).HasColumnName("ProgrammingLanguageId");
				t.HasOne(p => p.ProgrammingLanguage);
			});
			Technology[] technologiesSeedData = { new(1, 1, "Asp.Net"), new(2, 2, "Django") };
			modelBuilder.Entity<Technology>().HasData(technologiesSeedData);
		}
	}
}
