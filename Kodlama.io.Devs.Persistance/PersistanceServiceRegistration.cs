﻿using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Persistance.Contexts;
using Kodlama.io.Devs.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Persistance
{
	public static class PersistanceServiceRegistration
	{
		public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<BaseDbContext>(options =>
													 options.UseSqlServer(
														 configuration.GetConnectionString("PortfolioConnectionString")));

			services.AddScoped<IProgrammingLanguageRepository, ProgrammingLanguageRepository>();
			services.AddScoped<ITechnologyRepository, TechnologyRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IUserOperationClaimRepository, UserOperationClaimRepository>();
			services.AddScoped<IOperationClaimRepository, OperationClaimRepository>();
			services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
			services.AddScoped<IAppUserRepository, AppUserRepository>();
			services.AddScoped<ISocialLinkRepository, SocialLinkRepository>();

			return services;
		}
	}
}
