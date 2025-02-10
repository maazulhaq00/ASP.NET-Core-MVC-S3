using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace CodeFirst.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        //Naviagtional Property
        public List<Product> Products { get; set; }

    }
}
