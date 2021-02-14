using FoodPal.Providers.DataAccess.Repositories;
using FoodPal.Providers.DomainModels;
using System;
using System.Threading.Tasks;

namespace FoodPal.Providers.DataAccess.UnitsOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        public Task<int> CommitAsync();

        public IProviderRepository ProviderRepository { get; }
        public ICatalogueItemRepository CatalogueItemRepository { get; }
        public IRepository<Catalogue> CatalogueRepository { get; }
    }
}
