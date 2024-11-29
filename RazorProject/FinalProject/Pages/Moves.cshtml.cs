using FinalProject.Data;
using FinalProject.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Pages
{
    public class MovesModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<TblMoves> Moves { get; set; } = new List<TblMoves>();

        public int GameID { get; set; }

        [BindProperty]

        [TempData]
        public string ErrorMessage { get; set; }
        public MovesModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet(int gameId)
        {
            GameID = gameId;

            Moves = _context.Moves.Where(m => m.GameId == GameID).ToList();
        }

        public IActionResult OnPost()
        {
             this.GameID = GetLastGameID() + 1;
            _context.SaveChanges();
            return RedirectToPage();
        }

        public int GetLastGameID()
        {
            if (_context.Games == null)
            {
                return 0;
            }

            int lastGameId = _context.Games.OrderByDescending(g => g.GameID).Select(g => g.GameID).FirstOrDefault();
            return lastGameId;
        }
    }
}