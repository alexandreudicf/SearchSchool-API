using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SearchSchool.Configuration;
using SearchSchool.Models;
using SearchSchool.Repositories;
using SearchSchool.Repositories.Contract;
using SearchSchool.Settings;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SearchSchool
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
            services.AddSingleton<CacheSchools>();
            services.AddTransient<ISchoolService, SchoolService>();
            services.Configure<AppSettings>(options => Configuration?.GetSection("AppSettings").Bind(options));

            AppSettings appSettings = Configuration.GetSection("AppSettings").Get<AppSettings>();
            services.AddHttpClient<DataPoaClient>().ConfigureHttpClient(client => {
                client.BaseAddress = new Uri(appSettings.DATAPOABaseURL);
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SearchSchool", Version = "v1" });


                string filePath = Path.Combine(AppContext.BaseDirectory, "SearchSchool.xml");
                c.IncludeXmlComments(filePath);

                string appPath = PlatformServices.Default.Application.ApplicationBasePath;
                string appName = PlatformServices.Default.Application.ApplicationName;
                string pathXmlDoc = Path.Combine(appPath, $"{appName}.xml");

                c.IncludeXmlComments(pathXmlDoc);
            });

            services.ConfigureCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable Cors
            app.UseCors("CorsPolicy");

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SearchSchool v1"));


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        
    }
}
