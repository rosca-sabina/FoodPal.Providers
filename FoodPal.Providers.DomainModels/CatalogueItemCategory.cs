using System.ComponentModel.DataAnnotations;

namespace FoodPal.Providers.DomainModels
{
    public class CatalogueItemCategory: BaseModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
