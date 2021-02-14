using FoodPal.Providers.DataAccess.Repositories;
using FoodPal.Providers.DomainModels;
using System;
using System.Threading.Tasks;

namespace FoodPal.Providers.DataAccess.UnitsOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProvidersContext _providersContext;
        private ProviderRepository _providerRepository;
        private CatalogueItemRepository _catalogueItemRepository;
        private Repository<Catalogue> _catalogueRepository;

        public UnitOfWork(ProvidersContext providersContext)
        {
            _providersContext = providersContext ?? throw new ArgumentNullException(nameof(providersContext));
        }

        public IProviderRepository ProviderRepository => _providerRepository ??= new ProviderRepository(_providersContext);

        public ICatalogueItemRepository CatalogueItemRepository => _catalogueItemRepository ??= new CatalogueItemRepository(_providersContext);

        public IRepository<Catalogue> CatalogueRepository => _catalogueRepository ??= new Repository<Catalogue>(_providersContext);

        public async Task<int> CommitAsync()
        {
            return await _providersContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _providersContext.Dispose();
        }
    }
}
