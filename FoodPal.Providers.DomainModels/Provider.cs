using System.ComponentModel.DataAnnotations;

namespace FoodPal.Providers.DomainModels
{
    public class Provider: BaseModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        public string Status { get; set; }
        [Required]
        [MaxLength(200)]
        public string Location { get; set; }
        public int CustomerId { get; set; }
        public int CategoryId { get; set; }
        public ProviderCategory Category { get; set; }
        public Catalogue Catalogue { get; set; }
    }
}
