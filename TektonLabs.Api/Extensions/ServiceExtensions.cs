
using TektonLabs.Domain.Interfaces;
using TektonLabs.Persistence.Database.Repository;
using TektonLabs.Service.Queries.Discount;
using TektonLabs.Service.Queries.Product;

namespace TektonLabs.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
        }

        public static void ConfigureServicesManager(this IServiceCollection services)
        {
            services.AddScoped<IProductQueryService, ProductQueryService>();
            services.AddScoped<IDiscountQueryService, DiscountQueryService>();
        }
    }
}
