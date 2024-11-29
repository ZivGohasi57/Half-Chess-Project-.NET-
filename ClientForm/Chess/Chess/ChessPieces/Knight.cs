using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Chess.ChessPieces
{
    internal class Knight : ChessPiece
    {
        private int minRow = 0;
        private int minCol = 0;
        private int maxRow = 7;
        private int maxCol = 3;

        public Knight(Color color)
        {
            this.Color = color;
        }
        
        /// <summary>
        /// calculating the valid moves of the Knight piece.
        /// </summary>
        /// <param name="board">the board game.</param>
        /// <param name="row">The row number the piece is standing on.</param>
        /// <param name="col">The col number the piece is standing on.</param>
        /// <returns>The array that contains all the Knight moves.</returns>
        public List<int[]> GetValidKnightMoves(squareChess[][] board, int row, int col)
        {

            int[][] knightMoves = new int[][]
            {
                new int[] { 2, 1 },
                new int[] { 2, -1 },
                new int[] { 1, 2 },
                new int[] { 1, -2 },
                new int[] { -2, 1 },
                new int[] { -2, -1 },
                new int[] { -1, 2 },
                new int[] { -1, -2 }
            };

            List<int[]> validMoves = new List<int[]>();

            foreach (var move in knightMoves)
            {
                int newRow = row + move[0];
                int newCol = col + move[1];

                if (newRow >= minRow && newRow <= maxRow && newCol >= minCol && newCol <= maxCol)
                {
                    if (board[newRow][newCol].currentPiece == null)
                    {
                        validMoves.Add(new int[] { newRow, newCol });
                    }
                    else
                    {
                        if (board[newRow][newCol].currentPiece.Color != this.Color)
                        {
                            validMoves.Add(new int[] { newRow, newCol });

                        }
                    }
                }
            }

            return validMoves;//return an array of the knight valid moves 
        }




        /// <summary>
        /// Gets the valid moves of the Knight and Updetes the square on the board that one of colered team(Black or White) can move there(Only Knight piece).
        /// </summary>
        /// <param name="board">The board game</param>
        /// <param name="row">The row number the piece is standing on.</param>
        /// <param name="col">The col number the piece is standing on.</param>
        public void UpdateIsPotential(squareChess[][] board, int row, int col)
        {
            List<int[]> validMoves = GetValidKnightMoves(board, row, col);//The list valid moves gets the list of all the valid moves  

            foreach (var move in validMoves)
            {
                int newRow = move[0];//contain the row
                int newCol = move[1];//contain the col
                if (this.Color == Color.White)//if its a white piece
                {
                    board[newRow][newCol].isWhitePotentialMove = true;//the White knight piece can go to that square 
                }
                else
                {
                    board[newRow][newCol].isBlackPotentialMove = true;//the Black knight piece can go to that square 
                }
            }
        }


        /// <summary>
        /// Gets all the valid moves of the Knight piece and choose a random move to play agains the player.
        ///the function is called only when there is any valid moves
        /// </summary>
        /// <param name="board">The board game</param>
        /// <param name="row">The row number the piece is standing on.</param>
        /// <param name="col">The col number the piece is standing on.</param>
        /// <returns>A new position as a point or the current position if the piece stayed inplace</returns>
        public Point KnightRandomMove(squareChess[][] board, int row, int col)
        {
            // Get all valid moves
            List<int[]> validMoves = GetValidKnightMoves(board, row, col);

            // Check if there are valid moves
            if (validMoves.Count > 0)
            {
                // Choose a random move from the list
                Random random = new Random();
                int[] chosenMove = validMoves[random.Next(validMoves.Count)];

                int targetRow = chosenMove[0];
                int targetCol = chosenMove[1];

                // Perform the random move
                board[targetRow][targetCol].currentPiece = board[row][col].currentPiece; // Move knight
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
