using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public String ProductName { get; set; }
        [Required]
        public int ProductPrice { get; set; }

        public int ProductCategoryId { get; set; }

        //Navigational Property
        [ForeignKey("ProductCategoryId")]
        public Category Category { get; set; }
    }
}
