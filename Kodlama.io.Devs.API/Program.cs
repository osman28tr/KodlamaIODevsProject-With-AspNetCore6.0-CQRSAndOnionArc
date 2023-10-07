using Kodlama.io.Devs.Application;
using Kodlama.io.Devs.Persistance;
using Application;
using Core.CrossCuttingConcerns.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Core.Security.Encryption;
using Core.Security.JWT;
using Core.Security;
using Kodlama.io.Devs.Persistance.Contexts;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
	options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddApplicationServices();
builder.Services.AddSecurityServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

TokenOptions? tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}) // Authentication: "Bearer JWT_TOKEN"
	   .AddJwtBearer(options =>
	   {
		   options.TokenValidationParameters = new TokenValidationParameters
		   {
			   ValidateIssuer = true,
			   ValidIssuer = tokenOptions?.Issuer,
			   ValidateAudience = true,
			   ValidAudience = tokenOptions?.Audience,
			   ValidateLifetime = true,
			   ValidateIssuerSigningKey = true,
			   IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions?.SecurityKey),
			   ClockSkew = TimeSpan.Zero

			  
		   };

		   options.Events = new JwtBearerEvents
		   {
			   OnChallenge = context =>
			   {
				   Console.WriteLine("OnChallange: ");
				   return Task.CompletedTask;
			   },
			   OnAuthenticationFailed = context =>
			   {
				   Console.WriteLine("OnAuthenticationFailed:");
				   return Task.CompletedTask;
			   },
			   OnMessageReceived = context =>
			   {
				   Console.WriteLine("OnMessageReceived:");
				   return Task.CompletedTask;
			   },
			   OnTokenValidated = context =>
			   {
				   Console.WriteLine("OnTokenValidated:");
				   return Task.CompletedTask;
			   },
		   };
	   })
	   .AddCookie();

builder.Services.AddSwaggerGen(opt =>
{
	opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description =
			"JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345.54321\""
	});
	opt.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
				{ Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
			new string[] { }
		}
	});
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.ConfigureCustomExceptionMiddleware();
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
