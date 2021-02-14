using System.ComponentModel.DataAnnotations;

namespace FoodPal.Providers.DTOs
{
    public class NewCatalogueDTO
    {
        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
    }
}
