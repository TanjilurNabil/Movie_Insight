using Microsoft.Extensions.DependencyInjection;
using Movies.Application.Database;
using Movies.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application
{
    public static class ApplicationServiceCollectionExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<IMovieRepository, MovieRepository>();
            return services;
        }
        public static IServiceCollection AddDatabase(this IServiceCollection services,string connectionString)
        {
            services.AddSingleton<IDbConnectionFactory>(_ =>new NpgSqlConnectionFactory(connectionString)) ;
            services.AddSingleton<DbInitializer>();
            return services;
        }
       
    }
}
