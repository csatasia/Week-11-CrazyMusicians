using System.ComponentModel.DataAnnotations;

namespace Week_11_CrazyMusicians.Models
{
    public class CrazyMusician
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Name field is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters")]
        [RegularExpression(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ\s]+$", ErrorMessage = "Name can only contain letters")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Job field is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Job must be between 2 and 50 characters")]
        [RegularExpression(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ\s]+$", ErrorMessage = "Job can only contain letters")]
        public required string Job { get; set; }

        [Required(ErrorMessage = "Description field is required")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 500 characters")]
        public required string Description { get; set; }
    }
}
