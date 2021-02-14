using FoodPal.Providers.DTOs.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace FoodPal.Providers.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServicesConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(BaseProfile));
            services.AddTransient<IProviderSevice, ProviderService>();
            services.AddTransient<ICatalogueItemService, CatalogueItemService>();

            return services;
        }
    }
}
