using System.ComponentModel.DataAnnotations;

namespace FoodPal.Providers.DTOs
{
    public class ProviderCategoryDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
