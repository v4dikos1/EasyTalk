using EasyTalk.Application.Interfaces;
using EasyTalk.Application.Interfaces.Repositories;
using EasyTalk.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasyTalk.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DbConnection");
            services.AddDbContext<EasyTalkDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            services.AddScoped<IEasyTalkDbContext>(provider => provider.GetService<EasyTalkDbContext>());

            services.AddSingleton<IFileService, FIleService>();

            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<IInterestRepository, InterestRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
