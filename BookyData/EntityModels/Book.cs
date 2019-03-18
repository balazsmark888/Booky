using System.ComponentModel.DataAnnotations;

namespace BookyData.Models
{
    public class Book
    {
        public int Id { get; set; }
        public Author Author { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int Price { get; set; }
    }
}
