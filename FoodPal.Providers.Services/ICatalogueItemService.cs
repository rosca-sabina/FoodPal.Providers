using FoodPal.Providers.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodPal.Providers.Services
{
    public interface ICatalogueItemService
    {
        Task<IEnumerable<CatalogueItemDTO>> GetCatalogueItemsForProviderAsync(int providerId);
        Task<CatalogueItemDTO> GetCatalogueItemByIdAsync(int catalogueItemId);
        Task<bool> CatalogueItemExistsAsync(string catalogueItemName, int providerId);
        Task<int> CreateAsync(NewCatalogueItemDTO newCatalogueItem);
        Task UpdateAsync(CatalogueItemDTO catalogueItem);
        Task DeleteAsync(int catalogueItemId);
    }
}
