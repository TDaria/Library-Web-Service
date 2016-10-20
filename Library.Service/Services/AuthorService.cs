namespace Library.Service.Services
{
    using Library.Data.MsSql.Interfaces;
    using Library.Domain;
    using Library.Service.Interfaces;
using System.Collections.Generic;

    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            this._authorRepository = authorRepository;
        }

        public void Create(Author author)
        {
            this._authorRepository.Add(author);
        }

        public void Edit(Author author)
        {
            this._authorRepository.Update(author);
        }

        public void Delete(int id)
        {
            this._authorRepository.Remove(id);
        }

        public Author GetAuthorById(int id)
        {
            return this._authorRepository.GetAuthorById(id);
        }

        public List<Author> GetAllAuthors()
        {
            return this._authorRepository.GetAllAuthors();
        }

        public List<Book> GetBooksByAuthorId(int id)
        {
            return this._authorRepository.GetBooksByAuthorId(id);
        }

        public Author GetAuthorByName(string name)
        {
            return this._authorRepository.GetAuthorByName(name);
        }
    }
}
