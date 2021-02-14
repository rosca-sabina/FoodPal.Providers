using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodPal.Providers.DomainModels
{
    public class CatalogueItem: BaseModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [Column(TypeName="decimal(5,2)")]
        public decimal Price { get; set; }
        public int CatalogueId { get; set; }
        public Catalogue Catalogue { get; set; }
        public int CategoryId { get; set; }
        public CatalogueItemCategory Category { get; set; }
        [NotMapped]
        public string ImageUrl { get; set; }
    }
}
