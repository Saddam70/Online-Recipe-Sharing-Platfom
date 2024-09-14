using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Repository;
using DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

namespace Service_Layer
{
    public class Startup

    {
        private IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddScoped<IAdminRepo<Admin>, AdminRepo>();
            services.AddScoped<IUserRepo<User>, UserRepo>();
            services.AddScoped<IRecipeRepo<Recipe>, RecipeRepo>();
			services.AddScoped<IRecipeStepRepo<RecipeStep>, RecipeStepRepo>();
			services.AddScoped<IIngredientRepo<Ingredient>, IngredientRepo>();
			services.AddScoped<ICommentRepo<Comment>, CommentRepo>();
			services.AddScoped<IRatingRepo<Rating>, RatingRepo>();
            //services.AddScoped<IJwtTokenService, JwtTokenSerices>();
            //services.AddScoped<IUserService, UserService>();
            //configuring swagger services
            services.AddSwaggerGen();

			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "v1",
					Title = "Recipe API",
					Description = "Recipe Management API",
					TermsOfService = new Uri("https://wipro.com/Privacy.html"),
					Contact = new OpenApiContact
					{
						Name = "Saddam",
						Email = "saddam@tech.com",
						Url = new Uri("https://LinkedIn.com/wipro.com/contactus"),
					},
					License = new OpenApiLicense
					{
						Name = "Tech",
						Url = new Uri("https://wipro.com"),
					}
				});

				options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Description = "Please enter token",
					Name = "Authorization",
					Type = SecuritySchemeType.Http,
					BearerFormat = "JWT",
					Scheme = "bearer"
				});

				options.AddSecurityRequirement(new OpenApiSecurityRequirement
					{
						{
							new OpenApiSecurityScheme
							{
								Reference = new OpenApiReference
								{
									Type=ReferenceType.SecurityScheme,
									Id="Bearer"
								}
							},
							new string[]{}
						}
					});

			});
			//jwtAuthentication
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
			AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidIssuer = _configuration["Jwt:Issuer"],
					ValidAudience = _configuration["Jwt:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]))
				};
			});
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
			app.UseHttpsRedirection();//to use ssl port
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();
			//swagger middlewares

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
			});
			app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
