using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
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

namespace Kodlama.io.Devs.Application.Features.Auths.Commands.CreateUser
{
	public class CreateUserCommand:IRequest<RegisteredDto>
	{
		public UserForRegisterDto UserForRegisterDto { get; set; }
        public string IpAddress { get; set; }
        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, RegisteredDto>
		{
			private readonly IUserRepository _userRepository;
			private readonly IAuthService _authService;
			private readonly AuthBusinessRules _authBusinessRules;
			private readonly IAppUserRepository _appUserRepository;

			public CreateUserCommandHandler(IUserRepository userRepository, IAuthService authService, AuthBusinessRules authBusinessRules, IAppUserRepository appUserRepository)
			{
				_userRepository = userRepository;
				_authService = authService;
				_authBusinessRules = authBusinessRules;
				_appUserRepository = appUserRepository;
			}
			public async Task<RegisteredDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
			{
				await _authBusinessRules.EmailCanNotBeDuplicatedWhenRegistered(request.UserForRegisterDto.Email);

				byte[] passwordHash, passwordSalt;
				HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out passwordHash, out passwordSalt);

				AppUser newUser = new()
				{
					FirstName = request.UserForRegisterDto.FirstName,
					LastName = request.UserForRegisterDto.LastName,
					Email = request.UserForRegisterDto.Email,
					PasswordHash = passwordHash,
					PasswordSalt = passwordSalt,
					Status = true,
				};
				AppUser createdAppUser = await _appUserRepository.AddAsync(newUser);

				AccessToken createdAccessToken = await _authService.CreateAccessToken(createdAppUser);
				RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(createdAppUser, request.IpAddress);
				RefreshToken addedRefreshToken = await _authService.AddRefreshTokenToDb(createdRefreshToken);

				RegisteredDto registeredDto = new()
				{
					RefreshToken = addedRefreshToken,
					AccessToken = createdAccessToken,
				};
				return registeredDto;
			}
		}
	}
}
