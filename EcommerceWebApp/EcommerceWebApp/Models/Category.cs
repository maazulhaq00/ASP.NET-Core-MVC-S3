using System.ComponentModel.DataAnnotations;

namespace EcommerceWebApp.Models
{
    public class Category
    {
        [Key]
        public int category_id { get; set; }
        public string category_name { get; set; }

        // Navigational Property
        public List<Product>? products { get; set; }
    }
}
