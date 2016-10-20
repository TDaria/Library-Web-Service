using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Library.Domain;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Library.Models
{
    public class BookAuthorViewModel
    {
        public int BookId { get; set; }
        
        public string Title { get; set; }
        
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }
    }
}