using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchSchool.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            string[] whiteListCors = { "http://localhost:4200", "https://search-school.azurewebsites.net" };
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins(whiteListCors)
                    .SetIsOriginAllowed(r =>
                    {
                        return true;
                    })
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            return services;
        }
    }
}
