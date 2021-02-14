using System.ComponentModel.DataAnnotations;

namespace FoodPal.Providers.DomainModels
{
    public class ProviderCategory: BaseModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
