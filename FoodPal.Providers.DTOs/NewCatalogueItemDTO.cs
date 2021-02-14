using System.ComponentModel.DataAnnotations;

namespace FoodPal.Providers.DTOs
{
    public class NewCatalogueItemDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^\d{1,3}(\.\d{1,2})?$", ErrorMessage = "Price must be between 0 and 999.99.")]
        public decimal Price { get; set; }

        [Required]
        public int CatalogueId { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
