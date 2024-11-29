using System.ComponentModel.DataAnnotations;

namespace FinalProject.Model
{
    public class TblUsers
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Country { get; set; }

        public DateTime RegisteritionDate { get; set; } = DateTime.Now;

        public int GamesPlayed { get; set; } = 0;

        public List<TblGames> Games { get; set; } = new List<TblGames>();

        public List<TblQueryResults>? QueryResults { get; set; }


    }
}