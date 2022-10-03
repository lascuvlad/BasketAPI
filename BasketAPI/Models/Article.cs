using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasketAPI.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(200)]
        [Required]
        public string? ArticleName { get; set; }

        [Required]
        public decimal Price { get; set; }

        [ForeignKey("Basket")]
        public int BasketId { get; set; }
    }
}
