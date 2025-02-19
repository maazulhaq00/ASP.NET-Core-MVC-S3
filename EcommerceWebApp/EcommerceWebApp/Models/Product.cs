using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceWebApp.Models
{
    public class Product
    {
        [Key]
        public int product_id { get; set; }
        public string product_name { get; set; }
        public string product_price { get; set; }
        public string product_description { get; set; }
        public string product_image { get; set; }

        public int product_catid { get; set; }

        //Navigational Property
        [ForeignKey("product_catid")]
        public Category? category { get; set; }

    }
}
