using FinalProject.Data;
using FinalProject.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FinalProject.Pages
{
    public class EditUserModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public TblUsers User { get; set; } // Bind property to ensure it is available in OnPost

        public string ErrorMessage { get; set; }

        public EditUserModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            // Fetch the user to be edited
            User = _context.Users.FirstOrDefault(u => u.Id == LogInHelper.userID);

            if (User == null)
            {
                ErrorMessage = "User not found";
                return RedirectToPage("/Users");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Please correct the errors.";
                return Page();
            }

            // Load existing user data
            var existingUser = _context.Users.AsNoTracking().FirstOrDefault(u => u.Id == User.Id);
            if (existingUser == null)
            {
                ErrorMessage = "User not found";
                return RedirectToPage("/Users");
            }

            // Preserve original password if it was not modified
            if (string.IsNullOrEmpty(User.Password))
            {
                User.Password = existingUser.Password;
            }

            // Attach the User entity to the context and mark it as modified
            _context.Users.Attach(User);
            _context.Entry(User).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"There was an error updating the database: {ex.Message}. Inner exception: {ex.InnerException?.Message}";
                Console.WriteLine($"Full error: {ex}");
                return Page();
            }

            return RedirectToPage("/HomePage2");
        }


    }
}