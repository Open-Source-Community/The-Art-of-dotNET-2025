using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MVC_Session2.Data;
using System.ComponentModel.DataAnnotations;

namespace MVC_Session2
{
    public class UniqueNameAttribute : ValidationAttribute
    {
        AppDbContext context = new AppDbContext();
        public override bool IsValid(object? value)
        {
            var books = context.Books.ToList();
            foreach(var book in books)
            {
                if(book.Title == (string) value)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
