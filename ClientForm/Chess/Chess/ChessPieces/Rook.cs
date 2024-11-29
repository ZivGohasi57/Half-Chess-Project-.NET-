using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Chess.ChessPieces
{
    internal class Rook : ChessPiece
    {
        int minRow = 0;
        int minCol = 0;
        int maxRow = 7;
        int maxCol = 3;


        public Rook(Color color)
        {
            this.Color = color;
        }
       
        /// <summary>
        /// calculating the valid moves of the Rook piece.
        /// </summary>
        /// <param name="board">the board game.</param>
        /// <param name="row">The row number the piece is standing on.</param>
        /// <param name="col">The col number the piece is standing on.</param>
        /// <returns>The array that contains all the Rook moves.</returns>
        public List<int[]> GetValidRookMoves(squareChess[][] board, int row, int col)
        {
            int[][] RookMoves = new int[][]
            {
               new int[] { -1, 0 }, // Move up
                new int[] { 1, 0 },  // Move down
                new int[] { 0, -1 }, // Move left
                new int[] { 0, 1 }   // Move right
              
            };

            List<int[]> validMoves = new List<int[]>();

            foreach (var move in RookMoves)
            {
                int newRow = row + move[0];
                int newCol = col + move[1];

                while (newRow >= minRow && newRow <= maxRow && newCol >= minCol && newCol <= maxCol)
                {
                    if (board[newRow][newCol].currentPiece == null)
                    {
                        validMoves.Add(new int[] { newRow, newCol }); // Valid move, empty square

                    }

                    else if (board[newRow][newCol].currentPiece.Color != this.Color)
                    {
                        validMoves.Add(new int[] { newRow, newCol }); // Opponent's piece, valid capture
                        break; // Stop moving further in this direction
                    }
                    else
                    {
                        break; // Stop if it's our own piece
                    }

                    // Continue in the same direction
                    newRow += move[0];
                    newCol += move[1];
                }
            }

            return validMoves;
        }



        /// <summary>
        ///  Gets the valid moves of the Rook and Updetes the square on the board that one of colered team(Black or White) can move there(Only Rook piece).
        /// </summary>
        /// <param name="board">The board game</param>
        /// <param name="row">The row number the piece is standing on.</param>
        /// <param name="col">The col number the piece is standing on.</param>
        public void UpdateIsPotential(squareChess[][] board, int row, int col)
        {
            // Get all valid moves
            List<int[]> validMoves = GetValidRookMoves(board, row, col);

            foreach (var move in validMoves)// Mark all valid moves as potential moves
            {
                int newRow = move[0];// contain the row
                int newCol = move[1];// contain the col
                if (this.Color == Color.White)
                {
                    board[newRow][newCol].isWhitePotentialMove = true;//the White Rook piece can go to that square 
                }
                else
                {
                    board[newRow][newCol].isBlackPotentialMove = true;//the Black Rook piece can go to that square 
                }
            }
        }





        /// <summary>
        /// Gets all the valid moves of the Rook piece and choose a random move to play agains the player.
        ///the function is called only when there is any valid moves 
        /// </summary>
        /// <param name="board">The board game</param>
        /// <param name="row">The row number the piece is standing on.</param>
        /// <param name="col">The col number the piece is standing on.</param>
        /// <returns>A new position as a point or the current position if the piece stayed inplace</returns>
        public Point RookRandomMove(squareChess[][] board, int row, int col)
        {
            // Get all valid moves
            List<int[]> validMoves = GetValidRookMoves(board, row, col);

            // Check if there are valid moves
            if (validMoves.Count > 0)
            {
                // Choose a random move from the list
                Random random = new Random();
                int[] chosenMove = validMoves[random.Next(validMoves.Count)];

                int targetRow = chosenMove[0];
                int targetCol = chosenMove[1];

                // Perform the random move
                board[targetRow][targetCol].currentPiece = board[row][col].currentPiece; // Move rook
                board[row][col].currentPiece = null; // Clear the previous square
                this.currentRow = targetRow;
                this.currentCol = targetCol;

                // Return the new position as a Point
                return new Point(targetRow, targetCol);
            }

            // If there are no valid moves, return the current position
            return new Point(row, col);
        }



    }
}
