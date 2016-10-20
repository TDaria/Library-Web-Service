using Library.Domain;
namespace Library.Data.MsSql.Interfaces
{
    public interface IAccountRepository
    {
        void Add(Reader reader);

        Reader GetReaderById(int id);

        Reader GetReaderByEmail(string email);
        
        void Remove(int id);
    }
}
