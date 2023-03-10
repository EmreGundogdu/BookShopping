using System.ComponentModel.DataAnnotations;

namespace gNdgd.UI.Models
{
    public class Genre
    {
        public int Id { get; set; }
        [Required,MaxLength(40)]
        public string GenreName { get; set; }
        public List<Book> Books { get; set; }
    }
}
