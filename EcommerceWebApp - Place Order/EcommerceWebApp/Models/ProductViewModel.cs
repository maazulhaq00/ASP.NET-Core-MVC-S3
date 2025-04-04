using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcommerceWebApp.Models
{
    public class ProductViewModel
    {
        [Key]
        public int product_id { get; set; }
        public string product_name { get; set; }
        public string product_price { get; set; }
        public string product_description { get; set; }
        public IFormFile product_image { get; set; }

        public int product_catid { get; set; }

        //Navigational Property
        [ForeignKey("product_catid")]
        public Category? category { get; set; }

    }
}
