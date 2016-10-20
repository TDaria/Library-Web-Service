namespace Library.Data.MsSql.Repositories
{
    using Library.Data.MsSql.Interfaces;
    using Library.Domain;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Web;

    public class BookRepository : IBookRepository
    {
        private readonly string _connectionString;

        public BookRepository()
        {
            this._connectionString = "LibraryConnection";
        }

        public void Add(Book newBook)
        {
            var connection = ConfigurationManager.ConnectionStrings[_connectionString];
            using (SqlConnection conn = new SqlConnection(connection.ConnectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Book VALUES (@Title, 1, @Number)";
                    cmd.Parameters.AddWithValue("@Title", newBook.Title);
                    cmd.Parameters.AddWithValue("@Number", newBook.Number);
                    cmd.ExecuteReader();
                }
            }
        }

        public void AddAuthorToBook(int id, int authorId)
        {
            var connection = ConfigurationManager.ConnectionStrings[_connectionString];
            using (SqlConnection conn = new SqlConnection(connection.ConnectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO AuthorBook VALUES (@AuthorId, @BookId)";
                    cmd.Parameters.AddWithValue("@AuthorId", authorId);
                    cmd.Parameters.AddWithValue("@BookId", id);
                    cmd.ExecuteReader();
                }
            }
        }

        public List<Book> GetAllBooks()
        {
            List<Book> books = new List<Book>();
            var connection = ConfigurationManager.ConnectionStrings[_connectionString];
            using (SqlConnection conn = new SqlConnection(connection.ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT b.* FROM BOOK b";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Book book = new Book
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Number = reader.GetInt32(reader.GetOrdinal("Number")),
                            IsAvailable = reader.GetBoolean(reader.GetOrdinal("IsAvailable"))
                        };

                        books.Add(book);
                    }

                    return books;
                }
            }
        }

        public Book GetBookById(int id)
        {
            var connection = ConfigurationManager.ConnectionStrings[_connectionString];
            using (SqlConnection conn = new SqlConnection(connection.ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT b.* FROM BOOK b WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null;
                    }

                    Book book = new Book
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Title = reader.GetString(reader.GetOrdinal("Title")),
                        IsAvailable = reader.GetBoolean(reader.GetOrdinal("IsAvailable")),
                        Number = reader.GetInt32(reader.GetOrdinal("Number"))
                    };

                    return book;
                }
            }
        }

        public List<Author> GetAuthorsByBookId(int id)
        {
            var connection = ConfigurationManager.ConnectionStrings[_connectionString];
            using (SqlConnection conn = new SqlConnection(connection.ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "select a.* from Author a inner join AuthorBook ab ON a.Id = ab.AuthorId where ab.BookId = @Id";
                cmd.Parameters.AddWithValue("@Id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    List<Author> authors = new List<Author>();

                    while (reader.Read())
                    {
                        Author author = new Author
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name"))
                        };

                        authors.Add(author);
                    }

                    return authors;
                }
            }
        }

        public void TakeBookByReader(int id, int ReaderId)
        {
            string Date = DateTime.Now.ToShortDateString();
            var connection = ConfigurationManager.ConnectionStrings[_connectionString];
            using (SqlConnection conn = new SqlConnection(connection.ConnectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO ReaderLog VALUES (@BookId, @ReaderId, @Date)";
                    cmd.Parameters.AddWithValue("@BookId", id);
                    cmd.Parameters.AddWithValue("@ReaderId", ReaderId);
                    cmd.Parameters.AddWithValue("@Date", Date);
                    cmd.ExecuteReader();
                }
            }
        }

        public void Update(Book updatedBook)
        {
            var connection = ConfigurationManager.ConnectionStrings[_connectionString];
            using (SqlConnection conn = new SqlConnection(connection.ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "UPDATE Book SET Title=@Title, IsAvailable=@IsAvailable, Number=@Number WHERE Id=@Id";
                cmd.Parameters.AddWithValue("@Title", updatedBook.Title);
                cmd.Parameters.AddWithValue("@IsAvailable", updatedBook.IsAvailable);
                cmd.Parameters.AddWithValue("@Number", updatedBook.Number);
                cmd.Parameters.AddWithValue("@Id", updatedBook.Id);
                cmd.ExecuteReader();
            }
        }

        public void Remove(int id)
        {
            var connection = ConfigurationManager.ConnectionStrings[_connectionString];
            using (SqlConnection conn = new SqlConnection(connection.ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "DELETE FROM Book WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteReader();
            }
        }

        public List<string> GetBookHistory(int bookId)
        {
            List<string> logs = new List<string>();
            var connection = ConfigurationManager.ConnectionStrings[_connectionString];
            using (SqlConnection conn = new SqlConnection(connection.ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT rl.DateOfIssue, r.Email FROM Reader r inner join ReaderLog rl on rl.ReaderId = r.Id inner join Book b on b.Id = rl.BookId where rl.BookId = @BookId";
                cmd.Parameters.AddWithValue("@BookId", bookId);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string log = reader.GetString(reader.GetOrdinal("DateOfIssue")) + "   "
                                            + reader.GetString(reader.GetOrdinal("Email"));

                        logs.Add(log);
                    }

                    return logs;
                }
            }
        }
    }
}