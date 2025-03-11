using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceWebApp.Models
{
    public class CartItem
    {
        [Key]
        public int cart_id { get; set; }
        public int prod_id { get; set; }
        public int cust_id { get; set; }
        public int product_quantity { get; set; }
        public int order_id { get; set; }

        //Navigational Properties
        [ForeignKey("prod_id")]
        public Product? product { get; set; }

        [ForeignKey("cust_id")]
        public Customer? customer { get; set; }

    }
}
