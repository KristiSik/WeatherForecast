using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class City
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}