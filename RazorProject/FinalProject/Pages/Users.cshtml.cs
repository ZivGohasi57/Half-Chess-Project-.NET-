using FinalProject.Data;
using FinalProject.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FinalProject.Pages
{
    public class UsersModel : PageModel
    {
        private ApplicationDbContext _context;

        [BindNever] 
        public IEnumerable<dynamic> QueryResults { get; set; }

        public List<TblUsers> Users { get; set; } = new List<TblUsers>();



        [TempData]
        public string ErrorMessage { get; set; }

        public UsersModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Users = _context.Users.ToList();
        }

        public IActionResult OnPost()
        {
            _context.SaveChanges();

            return RedirectToPage();

        }

    }
}
    