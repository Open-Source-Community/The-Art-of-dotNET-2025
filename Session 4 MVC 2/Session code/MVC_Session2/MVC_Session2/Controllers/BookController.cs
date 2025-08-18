using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Session2.Data;
using MVC_Session2.Models;

namespace MVC_Session2.Controllers
{
    public class BookController : Controller
    {
        #region  Model Binding
            
        // /Controller/Action?id=3                                 Primitive Type

        // /Controller/Action?id=3&Name=Mahmoud&Salary=200000      Complex Type

        // /Controller/Action?color=red&color=blue                 Array

        // /Controller/Action?color["Red"]=red                         Dictionary
        
        #endregion
        
        AppDbContext context = new AppDbContext();
        
        public IActionResult Index()
        {
            var books = context.Books.Include(b=>b.Category).ToList();
            return View("Index",books);
        }

        #region Add New

        //[HttpGet]
        public IActionResult New()
        {
            ViewData["Categories"] = context.Categories.ToList();
            return View("New",new Book());
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult SaveNew(Book book)
        {
            if (book.Title != null&&ModelState.IsValid)
            {
                context.Books.Add(book);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["Categories"] = context.Categories.ToList();
            return View("New",book);
        }
        #endregion

        #region Edit Book

        public IActionResult Edit(int id)
        {
            Book book = context.Books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                ViewData["Categories"] = context.Categories.ToList();
                return View("Edit", book);
            }
            return NotFound();
        }

        public IActionResult SaveEdit(int id, Book newBook)
        {
            var book = context.Books.FirstOrDefault(b => b.Id == id);
            if (book != null && newBook.Title != null)
            {
                book.Title = newBook.Title;
                book.Description = newBook.Description;
                book.CategoryId = newBook.CategoryId;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["Categories"] = context.Categories.ToList();
            return View("Edit", newBook);
        }

        #endregion

        #region Remote

        public IActionResult check(string Username)
        {
            if (Username.Contains("M"))
            {
                return Json(true);
            }
            return Json(false);
        }

        #endregion
    }
}
