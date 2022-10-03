using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BasketAPI.Models
{
    public class Basket
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(200)]
        public string? Customer { get; set; }

        [DefaultValue(false)]
        [Required]
        public bool PaysVAT { get; set; }

        [Required]
        public string? Status { get; set; } = "opened";

        #region Navigation
        public virtual List<Article> Article { get; set; }
        #endregion Navigation
    }
}
