using FoodPal.Providers.DomainModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace FoodPal.Providers.DataAccess
{
    public static class DbSeedExtension
    {
        public static ModelBuilder SeedDatabase(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProviderCategory>()
                .HasData(
                    new ProviderCategory { Id = 1, CreatedOn = DateTime.Now, Name = "mediterranean Cuisine" },
                    new ProviderCategory { Id = 2, CreatedOn = DateTime.Now, Name = "Traditional Romanian Cuisine" },
                    new ProviderCategory { Id = 3, CreatedOn = DateTime.Now, Name = "Japanese Cuisine" },
                    new ProviderCategory { Id = 4, CreatedOn = DateTime.Now, Name = "Thai Cuisine" }
                );

            modelBuilder.Entity<CatalogueItemCategory>()
                .HasData(
                   new CatalogueItemCategory { Id = 1, CreatedOn = DateTime.Now, Name = "Dessert" },
                   new CatalogueItemCategory { Id = 2, CreatedOn = DateTime.Now, Name = "Main Course" },
                   new CatalogueItemCategory { Id = 3, CreatedOn = DateTime.Now, Name = "Soups" },
                   new CatalogueItemCategory { Id = 4, CreatedOn = DateTime.Now, Name = "Aperitives" }
                );

            return modelBuilder;
        }
    }
}
