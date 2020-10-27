using Licence.Service.Registration.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using LogHelper = Licence.Service.Registration.Infrastructure.LogHelper;

namespace Licence.Service.Registration
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<Setting>(Configuration.GetSection(nameof(Setting)));
            services.AddSingleton(Configuration);
            services.AddConsulConfig(Configuration);
            services.AddControllers();
            var setting = Configuration.GetSection(nameof(Setting)).Get<Setting>();
            services.Resolve(setting);

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseRouting();
            //app.UseSerilogRequestLogging(opts
            //    => opts.EnrichDiagnosticContext = LogHelper.EnrichFromRequest);
            app.UseAuthorization();
            app.UseConsul();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", " API V1");
            });
     

        }
    }
}
