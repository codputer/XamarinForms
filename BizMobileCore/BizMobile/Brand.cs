using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BizMobile
{
    public class Brand
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public override string ToString()
        {
            return string.Format($"{Url}/{Name}");
        }
    }
}