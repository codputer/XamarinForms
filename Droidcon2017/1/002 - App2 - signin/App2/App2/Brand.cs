namespace App2
{
    public class Brand : Entity
    {

        [Required]
        [DataType(DataType.Text)]
        [MaxLength(128)]
        public string Name { get; set; }

        [MaxLength(512)]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [MaxLength(50)]
        public string Phone { get; set; }

        [MaxLength(1024)]
        [DataType(DataType.Text)]
        public string Remarks { get; set; }


        [MaxLength(100)]
        [DataType(DataType.Text)]
        public string ContactPersonName { get; set; }

        [MaxLength(50)]
        [DataType(DataType.Text)]
        public string Country { get; set; }

        [MaxLength(50)]
        [DataType(DataType.Text)]
        public string MadeInCountry { get; set; }
      
        [MaxLength(50)]
        [DataType(DataType.Text)]
        public string Email { get; set; }

        [MaxLength(20)]
        [DataType(DataType.Text)]
        public string BrandCode { get; set; }
    }
}
