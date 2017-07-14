using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace DataLayer.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(450)]
        [Index(IsUnique = true)]
        [Remote("IsLoginAvailable", "Profile", ErrorMessage = "Login is in use")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage = "Password must contain 5 or more symbols")]
        public string Password { get; set; }

        [NotMapped]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Enter your Firstname")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Enter your Lastname")]
        public string Lastname { get; set; }

        public List<City> Favorites { get; set; }

        public List<Request> History { get; set; }

        public User()
        {
            Favorites = new List<City>();
            History = new List<Request>();
        }
    }
}