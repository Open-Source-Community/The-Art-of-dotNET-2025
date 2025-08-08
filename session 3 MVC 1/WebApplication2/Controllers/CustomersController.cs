using Microsoft.AspNetCore.Mvc;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    public class CustomersController : Controller
    {

        AppDbcontext _context = new AppDbcontext();
        public IActionResult Index()
        {
            var customer = _context.customers.ToList();
            return View(customer);

        }

        #region viewdata vs viewbag
        public IActionResult setViewData()
        {
            var cust = _context.customers.ToList();
            ViewData["msg"] = "Hello from controller";
            ViewBag.hello = "hello from controller";

            #region
            ViewData["Customers"] = cust;

            ViewBag.Customerlist = cust;
            #endregion

            return View("setviewdata");

        }
        #endregion

        #region viewModel
        public IActionResult setViewModel()
        {
            var customers = _context.customers.ToList();

            var viewModel = new CustomerViewModel
            {
                PageTitle = "Customer Management",
                WelcomeMessage = "Welcome to our customer portal!",
                Customers = customers
            };

            return View(viewModel);
        }
        #endregion

        #region tempdata
        public IActionResult SetTempData()
        {
            TempData["Message"] = "This is a TempData message from customer!";
            TempData["Timestamp"] = DateTime.Now.ToString();

            return RedirectToAction("GetTempData");
        }

        // Action 2: Retrieves TempData and displays it
        public IActionResult GetTempData()
        {
            ViewData["Name"] = TempData["Name"]; //Delete After Reading
           // TempData.Keep("Name");
          
            //TempData is automatically available in the view
            return View("gettempData");
        }
        #endregion

        #region cookies
        public IActionResult SetCookie()
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1),  
                
            };

            Response.Cookies.Append("UserCookie", "CookieValue123", cookieOptions);

            return RedirectToAction("GetCookie");
        }

        // Read a cookie
        public IActionResult GetCookie()
        {
            var cookieValue = Request.Cookies["UserCookie"];

            ViewBag.CookieValue = cookieValue;

            return View();
        }
        #endregion

        #region sessions
        // Set session data
        public IActionResult SetSession()
        {
            HttpContext.Session.SetString("Username", "JohnDoe");
            HttpContext.Session.SetInt32("UserId", 123);

            return RedirectToAction("GetSession");
        }

        // Retrieve session data
        public IActionResult GetSession()
        {
            var username = HttpContext.Session.GetString("Username");
            var userId = HttpContext.Session.GetInt32("UserId");

            ViewBag.Username = username;
            ViewBag.UserId = userId;

            return View();
        }
        #endregion
    }
}
