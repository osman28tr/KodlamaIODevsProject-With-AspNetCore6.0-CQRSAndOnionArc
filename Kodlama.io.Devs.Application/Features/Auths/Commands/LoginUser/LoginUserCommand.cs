﻿using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Dtos;
using Core.Security.Hashing;
using Kodlama.io.Devs.Application.Features.Auths.Dtos;
using Kodlama.io.Devs.Application.Features.Auths.Rules;
using Kodlama.io.Devs.Application.Services.AuthService;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Auths.Commands.LoginUser
{
	public class LoginUserCommand:IRequest<LoggedInDto>
	{
        public UserForLoginDto UserForLoginDto { get; set; }
        public string IpAddress { get; set; }
		public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoggedInDto>
		{
			private readonly AuthBusinessRules _authBusinessRules;
			private readonly IUserRepository _userRepository;
			private readonly IAppUserRepository _appUserRepository;
			private readonly IAuthService _authService;

			public LoginUserCommandHandler(AuthBusinessRules authBusinessRules, IUserRepository userRepository, IAppUserRepository appUserRepository, IAuthService authService)
			{
				_authBusinessRules = authBusinessRules;
				_userRepository = userRepository;
				_appUserRepository = appUserRepository;
				_authService = authService;
			}

			public async Task<LoggedInDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
			{
				await _authBusinessRules.EmailMustExist(request.UserForLoginDto.Email);

				AppUser appUserToLogin = await _appUserRepository.GetAsync(u => u.Email == request.UserForLoginDto.Email);
				bool isPasswordCorrect = HashingHelper.VerifyPasswordHash(request.UserForLoginDto.Password, appUserToLogin.PasswordHash, appUserToLogin.PasswordSalt);
				if (!isPasswordCorrect)
					throw new BusinessException("Password is incorrect");

				LoggedInDto loggedInDto = new()
				{
					AccessToken = await _authService.CreateAccessToken(appUserToLogin),
					RefreshToken = await _authService.CreateRefreshToken(appUserToLogin, request.IpAddress)
				};

				return loggedInDto;
			}
		}
	}
}
