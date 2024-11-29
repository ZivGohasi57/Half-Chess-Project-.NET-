using System.Drawing;

namespace FinalProject.ChessPieces
{
    public class ChessPiece
    {
        public Color Color { get; set; }

        public int currentRow { get; set; }

        public int currentCol { get; set; }

        public string location { get; set; }

        public bool isAlive { get; set; } = true;



        public Point RandomMove(ChessPiece chosenPiece, int row, int col, squareChess[][] board)
        {
            if (chosenPiece is King king)
            {
                return king.KingRandomMove(board, row, col);
            }
            else if (chosenPiece is Bishop bishop)
            {
                return bishop.BishopRandomMove(board, row, col);
            }
            else if (chosenPiece is Rook rook)
            {
                return rook.RookRandomMove(board, row, col);
            }
            else if (chosenPiece is Knight knight)
            {
                return knight.KnightRandomMove(board, row, col);
            }
            else if (chosenPiece is Pawn pawn)
            {
                return pawn.PawnRandomMove(board, row, col);
            }
            return Point.Empty;

        }

        public void UpdatePotential(ChessPiece chosenPiece, int row, int col, squareChess[][] board)
        {
            if (chosenPiece is King king)
            {
                king.UpdateIsPotential(board, row, col);
            }
            else if (chosenPiece is Bishop bishop)
            {
                bishop.UpdateIsPotential(board, row, col);
            }
            else if (chosenPiece is Rook rook)
            {
                rook.UpdateIsPotential(board, row, col);
            }
            else if (chosenPiece is Knight knight)
            {
                knight.UpdateIsPotential(board, row, col);
            }
            else if (chosenPiece is Pawn pawn)
            {
                pawn.UpdateIsPotential(board, row, col);
            }
        }

        public List<int[]> FindValidPointsByPiece(ChessPiece chosenPiece, int row, int col, squareChess[][] board)
        {

            List<int[]> pieceValidPoints = new List<int[]>();

           
            if (chosenPiece is King king)
            {
                pieceValidPoints = king.GetValidKingMoves(board, row, col);
            }
            else if (chosenPiece is Bishop bishop)
            {
                pieceValidPoints = bishop.GetValidBishopMoves(board, row, col);
            }
            else if (chosenPiece is Knight knight)
            {
                pieceValidPoints = knight.GetValidKnightMoves(board, row, col);
            }
            else if (chosenPiece is Rook rook)
            {
                pieceValidPoints = rook.GetValidRookMoves(board, row, col);
            }
            else if (chosenPiece is Pawn pawn)
            {
                pieceValidPoints = pawn.GetValidPawnMoves(board, row, col);
            }

            return pieceValidPoints;


        }

    }
}
