using System.ComponentModel.DataAnnotations;

namespace MyApp.Models
{
    public class Doctor
    {
        [Required]
        [Display(Name = "Doctor Name")]
        public String DoctorName { get; set; }
        [Required]
        [Display(Name = "Doctor Email")]
        public String DoctorEmail { get; set; }
        [Required]
        [Display(Name = "Doctor Salary")]
        public int DoctorSalary { get; set; }
        [Required]
        [Display(Name = "Doctor Specialization")]
        public String DoctorSpecialization { get; set; }
    }
}
