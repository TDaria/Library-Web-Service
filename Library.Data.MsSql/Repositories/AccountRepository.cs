namespace Library.Data.MsSql.Repositories
{
    using Library.Data.MsSql.Interfaces;
    using Library.Domain;
    using System.Configuration;
    using System.Data.SqlClient;

    public class AccountRepository : IAccountRepository
    {
        private readonly string _connectionString;

        public AccountRepository()
        {
            this._connectionString = "LibraryConnection";
        }

        public void Add(Reader newReader)
        {
            var connection = ConfigurationManager.ConnectionStrings[_connectionString];
            using (SqlConnection conn = new SqlConnection(connection.ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "INSERT INTO Reader VALUES (@Email)";
                cmd.Parameters.AddWithValue("@Email", newReader.Email);
                cmd.ExecuteReader();
            }
        }

        public Reader GetReaderById(int id)
        {
            var connection = ConfigurationManager.ConnectionStrings[_connectionString];
            using (SqlConnection conn = new SqlConnection(connection.ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT r.* FROM Reader r WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null;
                    }

                    Reader Reader = new Reader
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Email = reader.GetString(reader.GetOrdinal("Email")),
                    };

                    return Reader;
                }
            }
        }

        public Reader GetReaderByEmail(string email)
        {
            var connection = ConfigurationManager.ConnectionStrings[_connectionString];
            using (SqlConnection conn = new SqlConnection(connection.ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT r.* FROM Reader r WHERE Email = @Email";
                cmd.Parameters.AddWithValue("@Email", email);

                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null;
                    }

                    Reader Reader = new Reader
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Email = reader.GetString(reader.GetOrdinal("Email"))
                    };

                    return Reader;
                }
            }
        }

        public void Remove(int id)
        {
            var connection = ConfigurationManager.ConnectionStrings[_connectionString];
            using (SqlConnection conn = new SqlConnection(connection.ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "DELETE FROM Reader WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteReader();
            }
        }
    }
}
