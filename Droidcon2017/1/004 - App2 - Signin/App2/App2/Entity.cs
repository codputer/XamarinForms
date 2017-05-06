using System;

namespace App2
{
    public abstract class Entity
    {
        public string Id { get; set; }
     
        public DateTime Created { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public string CreatedFrom { get; set; }
        [Required]
        public DateTime Modified { get; set; }
        [Required]
        public string ModifiedBy { get; set; }       
        [Required]
        public string ShopId { get; set; }

    }

}