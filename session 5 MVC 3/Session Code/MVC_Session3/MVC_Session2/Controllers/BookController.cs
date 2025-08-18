using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Session2.Data;
using MVC_Session2.Models;
using System.ComponentModel.Design.Serialization;

namespace MVC_Session2.Controllers
{
    public class BookController : Controller
    {

        IBookRepository bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public IActionResult Index()
        {
            var books = bookRepository.GetAllAsync();
            return View("Index", books);
        }

        
        public IActionResult PartialDetails(int Id)
        {
            var book = bookRepository.GetByIdAsync(Id);
            return PartialView("_Details", book);
        }

    }
}
