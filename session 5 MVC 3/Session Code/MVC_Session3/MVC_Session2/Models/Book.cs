using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MVC_Session2.Models
{
    public class Book
    {
        public int Id { get; set; }

        //[Remote(action: "check", controller: "Book", ErrorMessage = "MSG")]
        //[Required(ErrorMessage = "Title is Required")]
        //[MaxLength(5)]
        public string Title { get; set; }

        public string Description { get; set; }

        public int? CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}
