using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceWebApp.Models
{
    public class Order
    {
        [Key]
        public int order_id { get; set; }
        public int cust_id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string country { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public int zip_code { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string payment_method { get; set; }
        public string order_note { get; set; }
        public string order_status { get; set; }

        [ForeignKey("cust_id")]
        public Customer? customer { get; set; }
        public List<CartItem>? cartItems { get; set; }
    }
}
