using FoodPal.Providers.DataAccess;
using FoodPal.Providers.DataAccess.Repositories;
using FoodPal.Providers.DomainModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests.FoodPal.DataAccess
{
    public class CatalogueItemRepositoryTest
    {
        private ProvidersContext _providersContext;
        private CatalogueItemRepository _catalogueItemRepository;
        public CatalogueItemRepositoryTest()
        {
            DbContextOptions<ProvidersContext> providersContextOptions = new DbContextOptionsBuilder<ProvidersContext>()
                .UseInMemoryDatabase("FoodPal.Providers")
                .Options;

            _providersContext = new ProvidersContext(providersContextOptions);
            _catalogueItemRepository = new CatalogueItemRepository(_providersContext);

            // Triggers seeding the data from DbSeedExtension
            _providersContext.Database.EnsureCreated();

            List<Provider> providers = new List<Provider>()
            {
                new Provider {
                    Id = 1,
                    Name = "Prov1",
                    Description = "Example Provider 1",
                    Location = "Bucharest",
                    CategoryId = 1,
                    CreatedOn = DateTime.Now,
                    CustomerId = 101
                },
                new Provider {
                    Id = 2,
                    Name = "Prov2",
                    Description = "Example Provider 2",
                    Location = "Constanta",
                    CategoryId = 2,
                    CreatedOn = DateTime.Now,
                    CustomerId = 102
                },
                new Provider {
                    Id = 3,
                    Name = "Prov3",
                    Description = "Example Provider 3",
                    Location = "Bucharest",
                    CategoryId = 3,
                    CreatedOn = DateTime.Now,
                    CustomerId = 103
                }
            };
            if (_providersContext.Providers.Count<Provider>() == 0)
            {
                _providersContext.Providers.AddRange(providers);
                _providersContext.SaveChanges();
            }

            List<Catalogue> catalogues = new List<Catalogue>()
            {
                new Catalogue { Id = 1, CreatedOn = DateTime.Now, Description = "Catalogue 1", ProviderId = 1 },
                new Catalogue { Id = 2, CreatedOn = DateTime.Now, Description = "Catalogue 2", ProviderId = 2 },
                new Catalogue { Id = 3, CreatedOn = DateTime.Now, Description = "Catalogue 3" }
            };
            if (_providersContext.Catalogues.Count<Catalogue>() == 0)
            {
                _providersContext.Catalogues.AddRange(catalogues);
                _providersContext.SaveChanges();
            }

            List<CatalogueItem> catalogueItems = new List<CatalogueItem>()
            {
                new CatalogueItem {
                    Id = 1,
                    CreatedOn = DateTime.Now,
                    Name = "Bulz",
                    Price = (decimal)15.4,
                    CatalogueId = 1,
                    CategoryId = 1
                },
                new CatalogueItem {
                    Id = 2,
                    CreatedOn = DateTime.Now,
                    Name = "Inghetata",
                    Price = (decimal)12.0,
                    CatalogueId = 1,
                    CategoryId = 1
                },
                new CatalogueItem {
                    Id = 3,
                    CreatedOn = DateTime.Now,
                    Name = "Ciorba de burta",
                    Price = (decimal)22,
                    CatalogueId = 1,
                    CategoryId = 3
                },
                new CatalogueItem {
                    Id = 4,
                    CreatedOn = DateTime.Now,
                    Name = "Salata bulgareasca",
                    Price = (decimal)17.0,
                    CatalogueId = 1,
                    CategoryId = 4
                },
                new CatalogueItem {
                    Id = 5,
                    CreatedOn = DateTime.Now,
                    Name = "No Provider",
                    Price = (decimal)17.0,
                    CatalogueId = 3,
                    CategoryId = 4
                }
            };
            if (_providersContext.CatalogueItems.Count<CatalogueItem>() == 0)
            {
                _providersContext.CatalogueItems.AddRange(catalogueItems);
                _providersContext.SaveChanges();
            }
        }

        [Fact]
        public async void Test_GetCatalogueItemByIdAsync()
        {
            int catalogueItemId = 3;
            string name = "Ciorba de burta";

            CatalogueItem item = await _catalogueItemRepository.GetCatalogueItemByIdAsync(catalogueItemId);
            Assert.Equal(name, item.Name);
        }

        [Fact]
        public async void Test_GetCatalogueItemsForProviderAsync()
        {
            int providerId = 1;
            IEnumerable<CatalogueItem> items = await _catalogueItemRepository.GetCatalogueItemsForProviderAsync(providerId);
            List<CatalogueItem> itemList = items.ToList();
            Assert.Equal(4, itemList.Count);
        }
    }
}
