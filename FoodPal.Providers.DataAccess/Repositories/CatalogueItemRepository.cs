using FoodPal.Providers.DomainModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPal.Providers.DataAccess.Repositories
{
    public class CatalogueItemRepository : Repository<CatalogueItem>, ICatalogueItemRepository
    {
        public CatalogueItemRepository(ProvidersContext providersContext) : base(providersContext)
        {
        }

        public async Task<CatalogueItem> GetCatalogueItemByIdAsync(int catalogueItemId)
        {
            return await _providersContext.CatalogueItems
                .Include(ci => ci.Category)
                .Include(ci => ci.Catalogue)
                .ThenInclude(c => c.Provider)
                .SingleOrDefaultAsync(ci => ci.Id == catalogueItemId);
        }

        public async Task<IEnumerable<CatalogueItem>> GetCatalogueItemsForProviderAsync(int providerId)
        {
            return await _providersContext.CatalogueItems
                .Include(ci => ci.Category)
                .Include(ci => ci.Catalogue)
                .ThenInclude(c => c.Provider)
                .Where(ci => !ci.Catalogue.Equals(null) && ci.Catalogue.ProviderId == providerId)
                .ToListAsync();
        }
    }
}
