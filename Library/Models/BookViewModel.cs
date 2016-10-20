namespace Library.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Library.Domain;
    using System.ComponentModel;

    public class BookViewModel
    {
        public BookViewModel()
        {
            Authors = new List<Author>();
            this.IsAvailable = true;
        }

        public int Id { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, MinimumLength = 1)]
        public string Title { get; set; }

        public bool IsAvailable { get; set; }

        [Display(Name = "Number")]
        [Range(1, Int32.MaxValue)]
        [Required(ErrorMessage = "Number is required.")]
        [DefaultValue(1)]        
        public int Number { get; set; }

        public List<Author> Authors { get; set; }
    }
}