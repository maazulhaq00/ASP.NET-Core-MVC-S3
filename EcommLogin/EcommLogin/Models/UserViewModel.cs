namespace EcommLogin.Models
{
    public class UserViewModel
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = null!;

        public string UserEmail { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string UserPassword { get; set; } = null!;

        public IFormFile UserImage { get; set; } = null!;
    }
}
