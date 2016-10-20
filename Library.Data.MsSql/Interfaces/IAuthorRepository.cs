namespace Library.Data.MsSql.Interfaces
{
    using Library.Domain;
    using System.Collections.Generic;

    public interface IAuthorRepository
    {
        void Add(Author newAuthor);

        void Update(Author newAuthor);

        void Remove(int id);

        Author GetAuthorById(int id);

        List<Author> GetAllAuthors();

        List<Book> GetBooksByAuthorId(int id);

        Author GetAuthorByName(string name);
    }
}
