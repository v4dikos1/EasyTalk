using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using EasyTalk.Application.Common.Services;
using EasyTalk.Application.Interfaces;
using EasyTalk.Application.Users.Commands.Registration;

namespace EasyTalk.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => 
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
            );

            services.AddSingleton<IPasswordService, PasswordService>();
            services.AddSingleton<ITokenService, TokenService>();

            services.AddSingleton<ITranslateService, YandexTranslateService>();

            return services;
        }
    }
}
