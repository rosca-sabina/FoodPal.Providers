using FoodPal.Providers.DomainModels;

namespace FoodPal.Providers.DTOs.Profiles
{
    internal class CatalogueItemProfile: BaseProfile
    {
        public CatalogueItemProfile()
        {
            CreateMap<CatalogueItem, CatalogueItemDTO>().ReverseMap();
            CreateMap<CatalogueItem, NewCatalogueItemDTO>().ReverseMap();
            CreateMap<CatalogueItemCategory, CatalogueItemCategoryDTO>().ReverseMap();
        }
    }
}
