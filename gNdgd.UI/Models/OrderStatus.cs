using System.ComponentModel.DataAnnotations;

namespace gNdgd.UI.Models
{
    public class OrderStatus
    {
        public int I { get; set; }
        [Required,MaxLength(20)]
        public string StatusName { get; set; }
    }
}
