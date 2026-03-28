using System.ComponentModel.DataAnnotations;

namespace March28_assessment.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter product name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please enter product price")]
        [Range(0.01, 100000)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please enter product category")]
        public string? Category { get; set; }
    }
}