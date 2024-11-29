using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Model
{
    public class TblUsers
    {

        
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime RegisteritionDate { get; set; } = DateTime.Now;

        public int GamesPlayed { get; set; } = 0;

        //public List<TblGames> Games { get; set; } = new List<TblGames>();



    }
}

