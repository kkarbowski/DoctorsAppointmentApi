using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AppointmentApi.Business;
using AppointmentApi.DataAccess;
using AppointmentApi.DataAccess.Interfaces;
using AppointmentApi.Database;
using AppointmentApi.Tools;
using AppointmentApi.Tools.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AppointmentApi.Business.Interfaces;
using AppointmentApi.Authorization;
using Serilog;
using AppointmentApi.Filters.Action;
using AppointmentApi.Filters.Exception;
using DinkToPdf.Contracts;
using DinkToPdf;
using StackExchange.Redis;

namespace AppointmentApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;


            // Recreating db.
            AppDbContext.InitializeDatabase();
            Log.Information("Database initalized");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = Config.SwaggerApiTitle, Version = Config.SwaggerApiVersion });
            });

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config.JwtSecret)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddTransient<AppDbContext>();
            services.AddTransient<IPatientBusiness, PatientBusiness>();
            services.AddTransient<IPatientDataAccess, PatientDataAccess>();
            services.AddTransient<IUserBusiness, UserBusiness>();
            services.AddTransient<IUserDataAccess, UserDataAccess>();
            services.AddTransient<IAppointmentBusiness, AppointmentBusiness>();
            services.AddTransient<IAppointmentDataAccess, AppointmentDataAccess>();
            services.AddTransient<IReasonBusiness, ReasonBusiness>();
            services.AddTransient<IReasonDataAccess, ReasonDataAccess>();
            services.AddTransient<IDoctorBusiness, DoctorBusiness>();
            services.AddTransient<IDoctorDataAccess, DoctorDataAccess>();

            services.AddTransient<IHashGenerator, HashGeneratorSHA256>();
            services.AddTransient<ITokenGenerator, TokenGeneratorJWT>();
            services.AddTransient<IPdfGenerator, PdfGenerator>();

            services.AddTransient<IPatientAuthorization, PatientAuthorization>();

            services.AddScoped<LoggingFilter>();
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(CustomExceptionFilter));
            });

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = RedisConfiguration.GetRedisIp();
                options.InstanceName = "master";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                Log.Information("Environment = Development");
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(Config.SwaggerApiUrl, $"{Config.SwaggerApiTitle} - {Config.SwaggerApiVersion}");
            });

            //

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
