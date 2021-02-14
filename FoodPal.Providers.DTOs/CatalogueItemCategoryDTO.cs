using System.ComponentModel.DataAnnotations;

namespace FoodPal.Providers.DTOs
{
    public class CatalogueItemCategoryDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
