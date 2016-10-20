namespace Library.Domain
{
    using System;
    using System.Collections.Generic;

    public class Book
    {
        public Book()
        {
            this.Authors = new List<Author>();
            this.IsAvailable = true;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public bool IsAvailable { get; set; }

        public int Number { get; set; }

        public List<Author> Authors { get; set; }
    }
}
