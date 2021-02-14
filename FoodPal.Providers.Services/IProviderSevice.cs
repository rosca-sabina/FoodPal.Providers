using FoodPal.Providers.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodPal.Providers.Services
{
    public interface IProviderSevice
    {
        Task<IEnumerable<ProviderDTO>> GetAllAsync(bool includeCatalogueItems);
        Task<ProviderDTO> GetByIdAsync(int providerId, bool includeCatalogueItems);
        Task<bool> ProviderExistsAsync(string providerName);
        Task<ProviderDTO> CreateAsync(NewProviderDTO newProvider);
        Task UpdateAsync(ProviderDTO provider);
        Task DeleteAsync(int providerId);
    }
}
