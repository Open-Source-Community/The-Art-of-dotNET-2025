using Microsoft.AspNetCore.Mvc;
using MVC_Session2.Data;

namespace MVC_Session2.Controllers
{
    public class CategoryController : Controller
    {
        AppDbContext context = new AppDbContext();

        public IActionResult ShowCategoryWithBook()
        {
            var Categories = context.Categories.ToList();
            return View("ShowCategoryWithBook", Categories);
        }

        public IActionResult CategoryBooks(int id)
        {
            var books = context.Books.Where(b=>b.CategoryId == id).ToList();
            return Json(books);
        }
    }
}
