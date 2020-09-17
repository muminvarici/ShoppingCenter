using AutoMapper;
using Core.DependencyInjection.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ShoppingCenter.Api.PipelineBehaviors;
using ShoppingCenter.InfraStructure.Implementations;
using ShoppingCenter.InfraStructure.Settings;
using System.Collections.Generic;
using System.Reflection;

namespace ShoppingCenter.Api
{
	public class Startup
	{
		private readonly IHostingEnvironment environment;
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration, IHostingEnvironment environment)
		{
			Configuration = configuration;
			this.environment = environment;
		}


		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			var assemblies = new Assembly[] 
			{
				typeof(ShoppingCenter.AppLayer.Queries.GetCartByIdQuery).Assembly,
				Assembly.GetExecutingAssembly()
			};
			//add mediator
			services.AddMediatR(assemblies);

			services.AddValidatorsFromAssembly(assemblies[0]);
			services.AddValidatorsFromAssembly(assemblies[1]);

			//mongo db settings
			services.Configure<DbSettings>(Configuration.GetSection("DbSettings"));

			services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

			//AutoMapper
			services.AddAutoMapper(typeof(AutoMapperProfile));

			//automatically injects services depends on their lifetime attributes
			services.InjectServiceAssembly(environment.IsDevelopment(), environment.IsProduction());

			//add swagger with no authentication option
			ConfigurePostServicesAddSwagger(services, false);

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseSwagger();

			// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
			// specifying the Swagger JSON endpoint.
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("v1/swagger.json", "Shopping Center api v1");
				c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
				c.DocumentTitle = "Api Endpoints";
			});

			app.UseHttpsRedirection();
			app.UseMvc();
		}


		private static void ConfigurePostServicesAddSwagger(IServiceCollection services, bool enableAuthentication = false)
		{
			services.AddSwaggerGen(options =>
			{
				//UseFullTypeNameInSchemaIds replacement for .NET Core
				options.CustomSchemaIds(x => x.FullName);
				OpenApiSecurityScheme openApiSecurityScheme = null;

				if (enableAuthentication)
				{
					openApiSecurityScheme = new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = "Bearer"
						},
						Scheme = "oauth2",
						Name = "Bearer",
						In = ParameterLocation.Header,

					};

					options.AddSecurityRequirement(new OpenApiSecurityRequirement()
					{
						{
							openApiSecurityScheme,
							new List<string>()
						}
					});

					options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
					{
						Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
						Name = "Authorization",
						In = ParameterLocation.Header,
						Type = SecuritySchemeType.ApiKey,
					});
				}
			});
		}
	}
}
