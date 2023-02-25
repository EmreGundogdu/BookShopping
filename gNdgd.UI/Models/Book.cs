using System.ComponentModel.DataAnnotations;

namespace gNdgd.UI.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required,MaxLength(40)]
        public string BookName { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        [Required]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public List<OrderDetail> OrderDetails{ get; set; }
        public List<CartDetail> CartDetails{ get; set; }

    }
}
