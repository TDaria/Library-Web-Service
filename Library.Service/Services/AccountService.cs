namespace Library.Service.Services
{
    using Library.Data.MsSql.Interfaces;
    using Library.Domain;
    using Library.Service.Interfaces;

    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            this._accountRepository = accountRepository;
        }

        public void Create(Reader reader)
        {
            this._accountRepository.Add(reader);
        }

        public void Delete(int id)
        {
            this._accountRepository.Remove(id);
        }

        public Reader GetReaderById(int id)
        {
            return this._accountRepository.GetReaderById(id);
        }

        public bool IsReaderExist(string email)
        {
            if (this._accountRepository.GetReaderByEmail(email) == null)
            {
                return false;
            }
            return true;
        }

        public Reader GetReaderByEmail(string email)
        {
            return this._accountRepository.GetReaderByEmail(email);
        }
    }
}
