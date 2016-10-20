namespace Library.Controllers
{
    using Library.Domain;
    using Library.Models;
    using Library.Service.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Security.Claims;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;

    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        private readonly IAccountService _accountService;

        private readonly IAuthorService _authorService;

        public BookController(IBookService bookService,
                              IAccountService accountService,
                              IAuthorService authorService)
        {
            this._bookService = bookService;
            this._accountService = accountService;
            this._authorService = authorService;
        }

        public ActionResult Create()
        {
            return this.View(new BookViewModel());
        }

        [HttpPost]
        public ActionResult Create(BookViewModel bookViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Book book = new Book { Title = bookViewModel.Title, Number = bookViewModel.Number };
            this._bookService.Create(book);

            return Redirect("Index");
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            List<Book> d = this._bookService.GetAllBooks();
            return View(this._bookService.GetAllBooks());
        }
        
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            Book book = this._bookService.GetBookById(id);
            book.Authors = this._bookService.GetAuthorsByBookId(id);
            return View(book);
        }

        public ActionResult Edit(int id)
        {
            return View(this._bookService.GetBookById(id));
        }

        [HttpPost]
        public ActionResult Edit(Book book)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            this._bookService.Edit(book);
            return Redirect("../Index");
        }

        public ActionResult Delete(int id)
        {
            return View(this._bookService.GetBookById(id));
        }

        [HttpPost]
        public ActionResult Delete(Book book)
        {
            this._bookService.Delete(book.Id);
            return Redirect("../Index");
        }

        [Authorize(Roles = "reader")]
        public ActionResult TakeBook(int id)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            Reader reader = this._accountService.GetReaderByEmail(claimsIdentity.FindFirst(ClaimTypes.Email).Value);

            this._bookService.TakeBookByReader(id, reader.Id);
            
            try
            {
                SentMail();
            }
            catch
            {
                return View("EmailError");
            }

            return View();
        }

        private void SentMail()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(claimsIdentity.FindFirst(ClaimTypes.Email).Value);
            mailMessage.From = new MailAddress("librarytestmail2@gmail.com");
            mailMessage.Subject = "Library notifications.";
            mailMessage.Body = "You took the following books in our library";
            mailMessage.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "librarytestmail2@gmail.com",
                    Password = "librarytestmail22"
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Send(mailMessage);
            }
        }

        public ActionResult AddAuthor(int id)
        {
            Book book = this._bookService.GetBookById(id);
            return View(new BookAuthorViewModel() {BookId = book.Id, Title = book.Title });
        }

        [HttpPost]
        public ActionResult AddAuthor(BookAuthorViewModel bookAuthorModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                Author author = this._authorService.GetAuthorByName(bookAuthorModel.Name);
                this._bookService.AddAuthorToBook(bookAuthorModel.BookId, author.Id);
                return Redirect("../Index");
            }
            catch
            {
                ModelState.AddModelError("", "The author does not exist.");
                return View(bookAuthorModel);
            }
        }

        public ActionResult BookHistory(int id)
        {
            HistoryViewModel model = new HistoryViewModel() { BookId = id};
            model.Logs.AddRange(this._bookService.GetBookHistory(id));
            return View(model);            
        }
    }
}