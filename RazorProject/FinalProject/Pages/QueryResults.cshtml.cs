using FinalProject.Data;
using FinalProject.Model;
using FinalProject.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace FinalProject.Pages
{
    public class QueryResultsModel : PageModel
    {

        private readonly ApplicationDbContext _context;



        public List<TblQueryResults> Querys { get; set; } = new List<TblQueryResults>();

        public IEnumerable<string> PlayerNames { get; set; } // Holds unique player names

        public List<TblUsers> Users { get; set; } = new List<TblUsers> { };


        public List<TblGames> Games { get; set; } = new List<TblGames> { };

        [BindProperty]
        public string SelectedPlayer { get; set; }


        public string ErrorMessage { get; set; }

        public IEnumerable<dynamic> QueryResults { get; set; }

        [BindProperty]
        public TblQueryResults newQuery { get; set; }

        public QueryResultsModel(ApplicationDbContext context)
        {
            _context = context;
        }




        public IActionResult OnPost(string buttonValue)
        {
            switch (buttonValue)
            {
                case "1":
                    // Fetch all players who have played, sorted by name in a case-insensitive manner
                    QueryResults = _context.Users
                        .Where(user => user.GamesPlayed > 0)  // Assuming GamesPlayed indicates if a user has played before
                        .OrderBy(user => user.Name.ToLower()) // Case-insensitive sorting by name
                        .ToList();

                    break;

                case "2":
                    // Fetch all players sorted by name in descending order with only name and start date of last game
                    QueryResults = _context.Users
                        .Where(user => user.GamesPlayed >= 0)
                        .OrderByDescending(user => user.Name) // Sort players in descending order by name
                        .Select(user => new
                        {
                            user.Name,
                            LastGameStartDate = _context.Games
                                .Where(g => g.TblUsersId == user.Id)
                                .OrderByDescending(g => g.StartDate)
                                .Select(g => g.StartDate)
                                .FirstOrDefault() // Get the most recent game's StartDate
                        })
                        .ToList();
                    break;


                case "3":
                    // Button 3: Show all games with full details from TblGames
                    QueryResults = _context.Games
                        .Include(g => g.User) // Include user details for each game
                        .Select(game => new
                        {
                            game.GameID,
                            game.StartDate,
                            game.EndDate,
                            game.GameDuration,
                            game.Moves,
                            game.Result,
                            UserName = game.User.Name // User's name
                        })
                        .ToList();
                    break;

                case "4":
                    // Fetch the first registered player from each country
                    QueryResults = _context.Users
                        .GroupBy(user => user.Country)
                        .Select(group => new
                        {
                            Country = group.Key,
                            FirstPlayer = group.OrderBy(user => user.RegisteritionDate).First().Name,
                            FirstRegisteritionDate = group.OrderBy(user => user.RegisteritionDate).First().RegisteritionDate
                        })
                        .ToList();
                    break;

                case "5":
                    ViewData["ShowComboBox"] = true;
                    PlayerNames = _context.Users
                     .Select(user => user.Name.ToLower())
                     .Distinct()
                     .OrderBy(name => name)
                     .ToList();


                    if (!string.IsNullOrEmpty(SelectedPlayer))
                    {
                        QueryResults = _context.Games
                              .Include(g => g.User)
                              .Where(g => g.User != null && g.User.Name.ToLower() == SelectedPlayer.ToLower())
                              .Select(game => new
                              {
                                  PlayerName = game.User.Name,
                                  GameID = game.GameID,
                                  StartDate = game.StartDate,
                                  EndDate = game.EndDate,
                                  GameDuration = game.GameDuration,
                                  Moves = game.Moves,
                                  Result = game.Result
                              })
                              .ToList();

                        ViewData["ShowComboBox"] = false; 
                    }


                    break;
                case "6":
                    // Fetch each user with their respective count of games played
                    QueryResults = _context.Users
                        .Select(user => new
                        {
                            UserName = user.Name,
                            GamesPlayed = user.GamesPlayed
                        })
                        .ToList();
                    break;
                case "7":
                    // CASE 7: Group players by game count in descending order
                    QueryResults = _context.Users
                        .Where(user => user.GamesPlayed >= 0)
                        .OrderByDescending(user => user.GamesPlayed)
                        .GroupBy(user => user.GamesPlayed)
                        .Select(group => new
                        {
                            GameCount = group.Key,
                            Players = group.Select(user => new
                            {
                                user.Id,
                                user.Name,
                                user.PhoneNumber,
                                user.Country,
                                user.GamesPlayed
                            }).ToList()
                        })
                        .ToList();
                    break;
                case "8":
                    // Group players by country and include all players from each country
                    QueryResults = _context.Users
                        .GroupBy(user => user.Country)
                        .Select(group => new
                        {
                            Country = group.Key,
                            Players = group.Select(user => new
                            {
                                user.Id,
                                user.Name,
                                user.PhoneNumber,
                                user.GamesPlayed
                            }).ToList() // List of players from this country
                        })
                        .ToList();
                    break;
            }

            return Page();
        }

        public void OnGet()
        {

            Querys = _context.QueryResults.ToList();
            Users = _context.Users.ToList();
            Games = _context.Games.ToList();

        }


    }
}