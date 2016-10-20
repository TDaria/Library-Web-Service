using Library.Domain;
namespace Library.Service.Interfaces
{
    public interface IAccountService
    {
        void Create(Reader reader);

        Reader GetReaderById(int id);

        bool IsReaderExist(string email);

        void Delete(int id);

        Reader GetReaderByEmail(string email);
    }
}
