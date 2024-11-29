using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Model
{
    internal class TblMoves
    {
        public int MoveId { get; set; } = 0;
        public int GameId { get; set; }

        public string PieceType { get; set; }

        public int PlayerId { get; set; }

        public int TblGamesGameID { get; set; }

        public string FromPosition { get; set; }

        public string ToPosition { get; set; }

        public int? TimeTaken { get; set; }


        

    }
}
