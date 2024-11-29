using FinalProject.Data;
using FinalProject.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Pages
{
    public class SignInButtonModel : PageModel
    {
        public List<TblUsers> Users { get; set; } = new List<TblUsers>();

        private readonly ApplicationDbContext _context;

        [BindProperty]
        public TblUsers newUser { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public SignInButtonModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Users = _context.Users.ToList();
        }

        public IActionResult OnPost()
        {
            if (newUser.Id == 0)
            {
                ErrorMessage = "ID is required. Please enter a valid ID.";
                Users = _context.Users.ToList();
                return Page();
            }

            // Check if the Id already exists in the database
            if (_context.Users.Any(user => user.Id == newUser.Id))
            {
                ErrorMessage = "This ID is already in use. Please choose a different ID.";
                Users = _context.Users.ToList();
                return Page();
            }

            // If ID is unique, add the new user
            _context.Users.Add(newUser);
            _context.SaveChanges();
            LogInHelper.isUserLogIn = true;
            LogInHelper.userID = newUser.Id;
            return RedirectToPage("/HomePage2", new { userId = newUser.Id }); // Refresh the page after submission
        }
    }
}