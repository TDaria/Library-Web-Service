using Library.Domain;
namespace Library.Models
{
    using System.Collections.Generic;

    public class AuthorDetailsViewModel
    {
        public Author Author { get; set; }

        public List<Book> Books { get; set; }

        public AuthorDetailsViewModel()
        {
            Books = new List<Book>();
        }
    }
}