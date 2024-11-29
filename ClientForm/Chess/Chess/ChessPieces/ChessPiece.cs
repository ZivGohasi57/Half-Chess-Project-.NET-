using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Chess.ChessPieces
{
    public class ChessPiece
    {
        public Color Color { get; set; }


        public int currentRow { get; set; }
        public int currentCol { get; set; }

        public string location { get; set; }

        public bool isAlive { get; set; } = true;


        /// <summary>
        /// Gets a piece and search which type he is and then execute a random move from the list he have.
        /// </summary>
        /// <param name="chosenPiece">A type of piece from chessPiece hirarcy</param>
        /// <param name="row">The row number the piece is standing on.</param>
        /// <param name="col">The col number the piece is standing on.</param>
        /// <param name="board">The board game</param>
        /// <returns>A position that the chosenPiece will make a move to</returns>
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


        /// <summary>
        /// Gets the chosen Piece, search which piece it is and Updetes the square on the board that one of colered team(Black or White) can move there. 
        /// </summary>
        /// <param name="chosenPiece">A type of piece from chessPiece hirarcy</param>
        /// <param name="row">The row number the piece is standing on.</param>
        /// <param name="col">The row number the piece is standing on.</param>
        /// <param name="board">The board game</param>
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


        /// <summary>
        /// Collects all the points of the piece that are valid moves 
        /// </summary>
        /// <param name="chosenPiece">A type of piece from chessPiece hirarcy</param>
        /// <param name="row">The row number the piece is standing on.</param>
        /// <param name="col">The col number the piece is standing on.</param>
        /// <param name="board">The board game</param>
        /// <returns>A list that containes the points of the piece valid moves </returns>
        public List<int[]> FindValidPointsByPiece(ChessPiece chosenPiece, int row, int col, squareChess[][] board)
        {

            List<int[]> pieceValidPoints = new List<int[]>();

            // Check the type of the piece and activate the right function.
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

