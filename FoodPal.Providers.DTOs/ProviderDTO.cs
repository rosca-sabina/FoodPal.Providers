using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPal.Providers.DTOs
{
    public class ProviderDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [MaxLength(200)]
        public string Location { get; set; }
        public ProviderCategoryDTO Category { get; set; }

        [Required]
        public CatalogueDTO Catalogue { get; set; }

        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "The field {0} must be greater than 0.")]
        public int CustomerId { get; set; }
    }
}
