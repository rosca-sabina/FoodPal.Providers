using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodPal.Providers.DTOs
{
    public class CatalogueDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        public List<CatalogueItemDTO> Items { get; set; }
    }
}
