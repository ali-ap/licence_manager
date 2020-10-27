using System;
using System.Linq;
using AutoMapper;
using Consul;
using Licence.Service.Common.Mapper;
using Licence.Service.SignatureGenerator.Service.Implementation;
using Licence.Service.SignatureGenerator.Service.Protocol;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Licence.Service.SignatureGenerator.Infrastructure
{
    public static class DependencyResolver
    {
        public static void Resolve(this IServiceCollection services, Setting setting)
        {
            services.AddTransient<ISignatureService, SignatureService>();
            //var consulClient = new ConsulClient(c => c.Address = new Uri("http://Consul:8500"));
            //var dis = consulClient.Agent.Services().Result.Response;
            //var address = dis.First(x => x.Value.Service == "Identity");
            //var address2 = dis.First(x => x.Value.Service == "Generator");
            services.AddAutoMapper(
                configuration => configuration.AddProfile<LicenceProfile>(),
                AppDomain.CurrentDomain.GetAssemblies());
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    //options.Authority = $"{address.Value.Address}:{address.Value.Port}";
                    options.Authority = $"http://Identity:8082";
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateIssuerSigningKey = false,

                    };
                });
        }

    }
}

