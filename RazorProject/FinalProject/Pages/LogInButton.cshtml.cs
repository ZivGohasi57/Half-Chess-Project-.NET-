using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FinalProject.Data;
using FinalProject.Model;

namespace FinalProject.Pages
{
    public class LogInButtonModel : PageModel
    {
        public List<TblUsers> Users { get; set; } = new List<TblUsers>();
        private readonly ApplicationDbContext _context;

        public LogInButtonModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Users = _context.Users.ToList();
        }

        public IActionResult OnPost(int id, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User ID or Password is incorrect.");
                return Page();
            }

            if (!user.Password.Equals(password))
            {
                ModelState.AddModelError(string.Empty, "User ID or Password is incorrect.");
                return Page();
            }

            LogInHelper.isUserLogIn = true;
            LogInHelper.userID = user.Id;

            return RedirectToPage("/HomePage2");
        }
    }
}
