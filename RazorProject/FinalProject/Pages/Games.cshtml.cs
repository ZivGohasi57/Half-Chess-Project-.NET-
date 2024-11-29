using FinalProject.Data;
using FinalProject.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Pages
{

    public class GamesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public GamesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        private int LogInUserID;

        public List<TblGames> Games { get; set; }

        public void OnGet()
        {

            Games = _context.Games
                            .Where(g => g.TblUsersId == LogInHelper.userID)
                            .ToList();

        }

        public IActionResult OnPost()
        {
            return RedirectToPage("/HomePage2");
        }

        public IActionResult OnPostDeleteGame(int gameId)
        {
            var game = _context.Games.FirstOrDefault(g => g.GameID == gameId);

            if (game != null)
            {
                var moves = _context.Moves.Where(m => m.GameId == gameId).ToList();
                _context.Moves.RemoveRange(moves);

                _context.Games.Remove(game);

                _context.SaveChanges();
            }

            return RedirectToPage();
        }
    }

}