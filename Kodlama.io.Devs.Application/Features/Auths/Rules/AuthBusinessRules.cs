using Core.CrossCuttingConcerns.Exceptions;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Auths.Rules
{
	public class AuthBusinessRules
	{
		private readonly IUserRepository _userRepository;
		private readonly IAppUserRepository _appUserRepository;

		public AuthBusinessRules(IUserRepository userRepository, IAppUserRepository appUserRepository)
		{
			_userRepository = userRepository;
			_appUserRepository = appUserRepository;
		}

		public async Task EmailCanNotBeDuplicatedWhenRegistered(string email)
		{
			AppUser? user = await _appUserRepository.GetAsync(u => u.Email == email);

			if (user != null)
			{
				throw new BusinessException("Email exists already.");
			}
		}

		public async Task EmailMustExist(string email)
		{
			AppUser? user = await _appUserRepository.GetAsync(u => u.Email == email);

			if (user == null)
			{
				throw new BusinessException("No registered user for this email.");
			}
		}
	}
}
