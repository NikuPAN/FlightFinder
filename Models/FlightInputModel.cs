using System.ComponentModel.DataAnnotations;

namespace FlightFinder.Models
{
    public class FlightInputModel
    {
        [Required]
        [MaxLength(100)]
        public string? InputString { get; set; }
    }
}
