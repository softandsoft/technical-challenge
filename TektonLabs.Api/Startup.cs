
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using TektonLabs.Api.Extensions;
using TektonLabs.Api.Filters;
using TektonLabs.Service.EventHandlers;
using AutoMapper;
using TektonLabs.Service.EventHandlers.Mappers;

namespace TektonLabs.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        string CorsConfiguration = "_corsConfiguration";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services, IHostBuilder host)
        {
            services.AddMemoryCache();

            services.ConfigureRepositoryManager();
            services.ConfigureServicesManager();
            
            var mapperConfig = new MapperConfiguration(m =>
            {
                m.AddProfile(new ProductProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ExceptionFilter));
            });

            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "TektonLabs.Api",
                    Version = "v1",
                    Description = "API para TektonLabs",
                    Contact = new OpenApiContact
                    {
                        Email = "cesarvallejo09@gmail.com",
                        Name = "Tekton Labs",
                        Url = new Uri("https://www.tektonlabs.com/")
                    }
                });
            });

            services.AddApplicationServices();

            services.AddCors(options =>
            {
                options.AddPolicy(
                    name: CorsConfiguration,
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });            

            host.UseSerilog();
            
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "TektonLabs.Api v1");
            });

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors(CorsConfiguration);          

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
