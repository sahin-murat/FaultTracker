using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using FaultTracker.Business.Interfaces;
using FaultTracker.Business.Services;
using FaultTracker.Data;
using AutoMapper;
using FaultTracker.Business.Mapping;

namespace FaultTracker.API
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>{

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AuthenticationOptions:Secret"]));

                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["AuthenticationOptions:Issuer"],
                        ValidAudience = Configuration["AuthenticationOptions:Audience"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateLifetime = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ClockSkew = Debugger.IsAttached ? TimeSpan.Zero : TimeSpan.FromMinutes(10)
                    };
                });

            //DB Context DI here
            services.AddDbContext<DatabaseContext>(opt =>
                opt.UseNpgsql(Configuration["ConnectionStrings:DataAccessPostgreSqlProvider"]));
            //Unit of Work DI
            services.AddTransient<IUnitOfWork, UnitOfWork>();           

            services.AddControllers();

            //Auto Mapper injection
            services.AddAutoMapper(typeof(Startup));
            services.AddSingleton(provider => new MapperConfiguration(cfg => {
                cfg.AddProfile(new DataTransferMapperProfile());
            }).CreateMapper());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //Add authentication to request pipeline
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
