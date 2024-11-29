using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Model;

namespace Chess.Model
{
    public class TblGames
    {
        public int GameID { get; set; }

        public DateTime? StartDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; }
        public int? GameDuration { get; set; }
        public int? MovesCount { get; set; }

        public string Result { get; set; } = "DNF";
    }


}





