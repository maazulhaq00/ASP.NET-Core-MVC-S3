using System.ComponentModel.DataAnnotations;

namespace EcommerceWebApp.Models
{
    public class AdminViewModel
    {
        [Key]
        public int admin_id { get; set; }
        public string admin_name { get; set; }
        public string admin_email { get; set; }
        public string admin_password { get; set; }
        public IFormFile? admin_image { get; set; }
    }
}
