using System;
using System.IO;
using System.Reflection;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MiTutorBEN.Converters;
using MiTutorBEN.Data;
using MiTutorBEN.Helpers;
using MiTutorBEN.Services;
using MiTutorBEN.ServicesImpl;
using Newtonsoft.Json;

namespace MiTutorBEN
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// Agregando los controladores
			services.AddControllers();

			// Configuración de los objetos
			IConfigurationSection appSettingsSection = Configuration.GetSection("AppSettings");
			services.Configure<AppSettings>(appSettingsSection);

			// Configuración de jwt
			var appSettings = appSettingsSection.Get<AppSettings>();
			var key = Encoding.ASCII.GetBytes(appSettings.Secret);
			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(x =>
			{
				x.RequireHttpsMetadata = false;
				x.SaveToken = true;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false
				};
			});

			// Configuración de injección de dependencias
			services.AddScoped<IAuthService, AuthServiceImpl>();
			services.AddScoped<ICourseService, CourseServiceImpl>();
			services.AddScoped<IPersonService, PersonServiceImpl>();
			services.AddScoped<ITopicService, TopicServiceImpl>();
			services.AddScoped<ITutoringOfferService, TutoringOfferServiceImpl>();
			services.AddScoped<ITutorService, TutorServiceImpl>();
			services.AddScoped<IUniversityService, UniversityServiceImpl>();
			services.AddScoped<IUserService, UserServiceImpl>();
			services.AddScoped<ITutoringSessionService, TutoringSessionServiceImpl>();
			services.AddScoped<ITopicTutoringOfferService, TopicTutoringOfferServiceImpl>();
			services.AddScoped<ITopicTutoringSessionService, TopicTutoringSessionServiceImpl>();



			// Converters
			services.AddScoped<AuthUserConverter>();
			services.AddScoped<CourseConverter>();
			services.AddScoped<PersonConverter>();
			services.AddScoped<TopicConverter>();
			services.AddScoped<TutoringOfferConverter>();
			services.AddScoped<TutorConverter>();
			services.AddScoped<UniversityConverter>();
			services.AddScoped<UserConverter>();
			services.AddScoped<TutoringOfferResponseConverter>();
			services.AddScoped<TutoringSessionResponseConverter>();
			services.AddScoped<TutoringOfferRequestConverter>();
			// Base de datos
			services.AddDbContext<MiTutorContext>(options =>
			{
				options.UseLazyLoadingProxies().
				UseNpgsql(Configuration.GetConnectionString("PostgresqlConnection"));
			});

			// Evitando ciclos en las queries
			services.AddMvc(options => options.EnableEndpointRouting = false)
				.SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
				.AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);


			// Configuracion del automapper
			var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });
			IMapper mapper = mappingConfig.CreateMapper();
			services.AddSingleton(mapper);

			// Swagger
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "v1",
					Title = "MiTutor",
				});

				// XML Documentation
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				c.IncludeXmlComments(xmlPath);
			});

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			// Global cors policy
			// Permitir cualquier origen
			app.UseCors(x => x
			.AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader());

			// Utilizar autenticación
			app.UseAuthentication();

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseSwagger();

			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "MiTutor V1");
			});


			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
