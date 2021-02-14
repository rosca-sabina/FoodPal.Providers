using System;

namespace FoodPal.Providers.DomainModels
{
    public class BaseModel
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
