namespace Library.Data.MsSql.Repositories
{
    using Library.Data.MsSql.Interfaces;
    using Library.Domain;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SqlClient;

    public class AuthorRepository : IAuthorRepository
    {
        private readonly string _connectionString;

        public AuthorRepository()
        {
            this._connectionString = "LibraryConnection";
        }

        public void Add(Author newAuthor)
        {
            var connection = ConfigurationManager.ConnectionStrings[_connectionString];
            using (SqlConnection conn = new SqlConnection(connection.ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "INSERT INTO Author VALUES (@Name)";
                cmd.Parameters.AddWithValue("@Name", newAuthor.Name);
                cmd.ExecuteReader();
            }
        }

        public void Update(Author newAuthor)
        {
            var connection = ConfigurationManager.ConnectionStrings[_connectionString];
            using (SqlConnection conn = new SqlConnection(connection.ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "UPDATE Author SET Name=@Name WHERE Id=@Id";
                cmd.Parameters.AddWithValue("@Name", newAuthor.Name);
                cmd.Parameters.AddWithValue("@Id", newAuthor.Id);
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
                cmd.CommandText = "DELETE FROM Author WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteReader();
            }             
        }

        public Author GetAuthorById(int id)
        {
            var connection = ConfigurationManager.ConnectionStrings[_connectionString];
            using (SqlConnection conn = new SqlConnection(connection.ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT a.* FROM Author a WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null;
                    }

                    Author author = new Author
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Name = reader.GetString(reader.GetOrdinal("Name"))
                    };

                    return author;
                }
            }
        }

        public Author GetAuthorByName(string name)
        {
            var connection = ConfigurationManager.ConnectionStrings[_connectionString];
            using (SqlConnection conn = new SqlConnection(connection.ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT a.* FROM Author a WHERE Name = @Name";
                cmd.Parameters.AddWithValue("@Name", name);

                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null;
                    }

                    Author author = new Author
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Name = reader.GetString(reader.GetOrdinal("Name"))
                    };

                    return author;
                }
            }
        }

        public List<Author> GetAllAuthors()
        {            
            var connection = ConfigurationManager.ConnectionStrings[_connectionString];
            using (SqlConnection conn = new SqlConnection(connection.ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT a.* FROM Author a";

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

        public List<Book> GetBooksByAuthorId(int id)
        {
            var connection = ConfigurationManager.ConnectionStrings[_connectionString];
            using (SqlConnection conn = new SqlConnection(connection.ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT ab.BookId, b.Title, b.IsAvailable FROM Book b inner join AuthorBook ab ON b.Id = ab.BookId inner join Author a ON a.Id = ab.AuthorId where a.Id = @Id";
                cmd.Parameters.AddWithValue("@Id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    List<Book> books = new List<Book>();

                    while (reader.Read())
                    {
           
                        Book book = new Book
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("BookId")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            IsAvailable = reader.GetBoolean(reader.GetOrdinal("IsAvailable"))
                        };

                        books.Add(book);
                    }

                    return books;
                }
            }
        }
    }
}