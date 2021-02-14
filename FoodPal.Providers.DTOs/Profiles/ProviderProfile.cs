using FoodPal.Providers.DomainModels;

namespace FoodPal.Providers.DTOs.Profiles
{
    internal class ProviderProfile: BaseProfile
    {
        public ProviderProfile()
        {
            CreateMap<Provider, ProviderDTO>().ReverseMap();
            CreateMap<Provider, NewProviderDTO>().ReverseMap();
            CreateMap<ProviderCategory, ProviderCategoryDTO>().ReverseMap();
        }
    }
}
