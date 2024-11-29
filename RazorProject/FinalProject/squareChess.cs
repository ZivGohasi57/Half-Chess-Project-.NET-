using FinalProject.ChessPieces;

namespace FinalProject
{
    public class squareChess
    {
        public string squareName { get; set; }
        public ChessPiece currentPiece { get; set; } = null;

        public bool isWhitePotentialMove { get; set; } = false;
        public bool isBlackPotentialMove { get; set; } = false;



        public squareChess(string squareName)
        {
            this.squareName = squareName;
        }

    }
}
