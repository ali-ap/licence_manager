using System;
using AutoMapper;
using Licence.Service.Common.Mapper;
using Licence.Service.Registration.Proxy;
using Licence.Service.Registration.Service.Implementation;
using Licence.Service.Registration.Service.Protocol;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Licence.Service.Registration.Infrastructure
{
    public static class DependencyResolver
    {
        public static void Resolve(this IServiceCollection services, Setting setting)
        {


            services.AddTransient<IRegistrationService, RegistrationService>();
            services.AddTransient<SignatureGenerationProxy>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo() { Title = "Licence Registration Microservice", Version = "v1" });
            });



            //services.AddHttpClient(nameof(SignatureGenerationProxy), c =>
            //{
            //    c.BaseAddress = new Uri(Environment.GetEnvironmentVariable("Identity"));
            //    c.DefaultRequestHeaders.Add("Accept", "application/json");
            //});
            services.AddAutoMapper(
                configuration => configuration.AddProfile<LicenceProfile>(),
                AppDomain.CurrentDomain.GetAssemblies());



        }
        private static string GetXmlCommentsPath()
        {
            var app = System.AppContext.BaseDirectory;
            return System.IO.Path.Combine(app, "Licence.Registration.xml");
        }
    }
}

