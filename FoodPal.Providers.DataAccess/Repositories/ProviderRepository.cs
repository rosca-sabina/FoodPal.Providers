using FoodPal.Providers.DomainModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodPal.Providers.DataAccess.Repositories
{
    public class ProviderRepository : Repository<Provider>, IProviderRepository
    {
        public ProviderRepository(ProvidersContext providersContext) : base(providersContext)
        {
        }

        public async Task<IEnumerable<Provider>> GetAllWithCatalogueItemsAsync()
        {
            return await _providersContext.Providers
                .Include(p => p.Category)
                .Include(p => p.Catalogue)
                .ThenInclude(c => c.Items)
                .ThenInclude(ci => ci.Category)
                .ToListAsync();
        }

        public async Task<Provider> GetWithCatalogueItemsByIdAsync(int providerId)
        {
            return await _providersContext.Providers
                .Include(p => p.Category)
                .Include(p => p.Catalogue)
                .ThenInclude(c => c.Items)
                .ThenInclude(ci => ci.Category)
                .SingleOrDefaultAsync(p => p.Id == providerId);
        }
    }
}
