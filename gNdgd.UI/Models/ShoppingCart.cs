using System.ComponentModel.DataAnnotations;

namespace gNdgd.UI.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        [Required]
        public string UserI { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
