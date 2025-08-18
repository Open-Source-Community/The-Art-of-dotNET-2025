using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Session2.Models;

namespace MVC_Session2.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        // GET
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel newRole)
        {
            if (ModelState.IsValid)
            {
                IdentityRole Role = new IdentityRole();
                Role.Name = newRole.Name;
                IdentityResult Result = await roleManager.CreateAsync(Role);
                if (Result.Succeeded)
                {
                    return RedirectToAction("Index", "Book");
                }

                foreach (var Error in Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, Error.Description);
                }
            }
            return View("Create", newRole);
        }
    }
}
