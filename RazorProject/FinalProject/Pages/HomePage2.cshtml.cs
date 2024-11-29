using FinalProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Pages
{
    public class HomePage2Model : PageModel
    {
        private readonly ApplicationDbContext _context;

        public string UserName { get; private set; }

        public HomePage2Model(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == LogInHelper.userID);
            UserName = user?.Name ?? "Guest";
        }

        public IActionResult OnPostLogout()
        {
            LogInHelper.isUserLogIn = false;
            LogInHelper.userID = 0;

            return RedirectToPage("/Index");
        }

        public IActionResult OnPostDeleteUser()
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == LogInHelper.userID);

            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();

                LogInHelper.isUserLogIn = false;
                LogInHelper.userID = 0;
            }

            return RedirectToPage("/Index");
        }


    }
}
