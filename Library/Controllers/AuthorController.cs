namespace Library.Controllers
{
    using Library.Domain;
    using Library.Models;
    using Library.Service.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            this._authorService = authorService;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            List<Author> authors =  this._authorService.GetAllAuthors();
            return View(authors);
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            AuthorDetailsViewModel authorViewModel = new AuthorDetailsViewModel();
            authorViewModel.Author = this._authorService.GetAuthorById(id);
            authorViewModel.Books = this._authorService.GetBooksByAuthorId(id);
            return View(authorViewModel);
        }

        public ActionResult Create()
        {
            return View(new Author());
        }

        [HttpPost]
        public ActionResult Create(Author author)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            this._authorService.Create(author);
            return Redirect("Index");
        }

        public ActionResult Edit(int id)
        {
            return View(this._authorService.GetAuthorById(id));
        }

        [HttpPost]
        public ActionResult Edit(Author author)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            this._authorService.Edit(author);
            return Redirect("../Index");
        }

        public ActionResult Delete(int id)
        {
            return View(this._authorService.GetAuthorById(id));
        }

        [HttpPost]
        public ActionResult Delete(Author author)
        {
            this._authorService.Delete(author.Id);
            return Redirect("../Index");
        }        
	}
}