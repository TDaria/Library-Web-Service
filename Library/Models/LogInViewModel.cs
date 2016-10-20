using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace Library.Models
{
    public class LogInViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [HiddenInput]
        public string ReturnUrl { get; set; }
    }
}