namespace Library.Service.Interfaces
{
    using Library.Domain;
    using System.Collections.Generic;

    public interface IAuthorService
    {
        void Create(Author author);

        void Edit(Author author);

        void Delete(int id);

        Author GetAuthorById(int id);

        List<Author> GetAllAuthors();

        List<Book> GetBooksByAuthorId(int id);

        Author GetAuthorByName(string name);
    }
}
