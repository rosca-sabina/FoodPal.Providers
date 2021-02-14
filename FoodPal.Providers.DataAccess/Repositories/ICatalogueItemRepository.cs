using FoodPal.Providers.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodPal.Providers.DataAccess.Repositories
{
    public interface ICatalogueItemRepository: IRepository<CatalogueItem>
    {
        Task<IEnumerable<CatalogueItem>> GetCatalogueItemsForProviderAsync(int providerId);
        Task<CatalogueItem> GetCatalogueItemByIdAsync(int catalogueItemId);
    }
}
