using System;
using System.ComponentModel.DataAnnotations;

namespace FoodPal.Providers.DTOs
{
    public class NewProviderDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [MaxLength(200)]
        public string Location { get; set; }
        public int CategoryId { get; set; }

        [Required]
        public NewCatalogueDTO Catalogue { get; set; }

        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "The field {0} must be greater than 0.")]
        public int CustomerId { get; set; }
    }
}
