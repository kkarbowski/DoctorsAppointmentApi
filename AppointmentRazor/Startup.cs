using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AppointmentRazor.Resources;
using AppointmentRazor.Utilities;
using AppointmentRazor.Utilities.Localization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;

namespace AppointmentRazor
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.ConfigureRequestLocalization();

            services.AddRazorPages();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddViewLocalization(o => o.ResourcesPath = "Resources")
                .AddDataAnnotationsLocalization(o => {
                     var type = typeof(ViewResource);
                     var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
                     var factory = services.BuildServiceProvider().GetService<IStringLocalizerFactory>();
                     var localizer = factory.Create("ViewResource", assemblyName.Name);
                     o.DataAnnotationLocalizerProvider = (t, f) => localizer;
                 })
                .AddRazorPagesOptions(o =>
                {
                    o.Conventions.Add(new CultureTemplateRouteModelConvention());
                });

            services.AddSingleton<CultureLocalizer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseRequestLocalization();
            app.UseCookiePolicy();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
