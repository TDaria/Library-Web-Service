namespace Library.Service.Services
{
    using Library.Data.MsSql.Interfaces;
using Library.Domain;
using Library.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            this._bookRepository = bookRepository;
        }

        public void Create(Book book)
        {
            this._bookRepository.Add(book);
        }

        public Book GetBookById(int id)
        {
            Book book = this._bookRepository.GetBookById(id);
            return book;
        }

        public List<Book> GetAllBooks()
        {
            return this._bookRepository.GetAllBooks();
        }

        public List<Author> GetAuthorsByBookId(int id)
        {
            return this._bookRepository.GetAuthorsByBookId(id);
        }
        
        public void AddAuthorToBook(int id, int authorId)
        {
            this._bookRepository.AddAuthorToBook(id, authorId);
        }

        public void TakeBookByReader(int id, int readerId)
        {
            this._bookRepository.TakeBookByReader(id, readerId);
            Book book = this._bookRepository.GetBookById(id);

            book.Number--;
            if (book.Number == 0)
            {
                book.IsAvailable = false;
            }

            this._bookRepository.Update(book);
        }

        public void Edit(Book updatedBook)
        {
            this._bookRepository.Update(updatedBook);
        }

        public void Delete(int id)
        {
            this._bookRepository.Remove(id);
        }

        public List<string> GetBookHistory(int bookId)
        {
            return this._bookRepository.GetBookHistory(bookId);
        }
    }
}
