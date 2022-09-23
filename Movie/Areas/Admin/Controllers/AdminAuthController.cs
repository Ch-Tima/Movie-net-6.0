using Movie.Context;
using Microsoft.AspNetCore.Mvc;

namespace Movie.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminAuthController : Controller
    {
        private readonly MovieContext _context;

        public AdminAuthController(MovieContext context)
        {
            _context = context;
        }

        public IActionResult SingIn() => View();

        [HttpPost]
        public IActionResult SingIn(string Login, string Password)
        {
            if(Login == null || Password == null)
                return View(viewName: nameof(SingIn), model: "Error: Not Found User!");

            var admin = _context.Admins.FirstOrDefault(x => x.Login == Login && x.Password == Helpers.HashPasswordHelpers.HashPassword(Password));

            if (admin == null)
                return View(viewName: nameof(SingIn), model: "Error: Not Found User!");

            //Add Cookies
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(1);
            this.Response.Cookies.Append("AdminId", admin.Id.ToString(), options);

            //Open Panel
            return Redirect("~/Admin/Panel");
        }
    }
}
