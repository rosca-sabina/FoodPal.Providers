using FoodPal.Providers.DomainModels;

namespace FoodPal.Providers.DTOs.Profiles
{
    internal class CatalogueProfile: BaseProfile
    {
        public CatalogueProfile()
        {
            CreateMap<Catalogue, CatalogueDTO>().ReverseMap();
            CreateMap<Catalogue, NewCatalogueDTO>().ReverseMap();
        }
    }
}
