using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Chess.ChessPieces
{
    internal class King : ChessPiece
    {
        int minRow = 0;
        int minCol = 0;
        int maxRow = 7;
        int maxCol = 3;



        public King(Color color)
        {
            this.Color = color;

        }

        /// <summary>
        /// calculating the valid moves of the King piece.
        /// </summary>
        /// <param name="board">the board game.</param>
        /// <param name="row">The row number the piece is standing on.</param>
        /// <param name="col">The col number the piece is standing on.</param>
        /// <returns>The array that contains all the kings moves.</returns>
        public List<int[]> GetValidKingMoves(squareChess[][] board, int row, int col)
        {
            // רשימת המהלכים האפשריים של המלך
            int[][] kingMoves = new int[][]
            {
                new int[] { 1, 1 }, new int[] { -1, -1 }, new int[] { 1, -1 }, new int[] { -1, 1 },
                new int[] { 1, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 0, -1 },
            };

            List<int[]> validMoves = new List<int[]>();

            foreach (var move in kingMoves)
            {
                int newRow = row + move[0];
                int newCol = col + move[1];

                // בדיקה אם המיקום החדש נמצא בתוך גבולות הלוח
                if (newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 4)
                {
                    // בדוק אם המשבצת ריקה או מכילה כלי של היריב
                    if (board[newRow][newCol].currentPiece == null ||
                        board[newRow][newCol].currentPiece.Color != board[row][col].currentPiece.Color)
                    {
                        // שמור את המצב הנוכחי של המשבצת
                        ChessPiece originalPiece = board[newRow][newCol].currentPiece;

                        // עדכון המשבצת עם המלך באופן זמני
                        board[newRow][newCol].currentPiece = board[row][col].currentPiece;
                        board[row][col].currentPiece = null;

                        // בדיקה אם המהלך לא מכניס את המלך לשח
                        
                        validMoves.Add(new int[] { newRow, newCol });
                        

                        // החזר את המצב המקורי
                        board[row][col].currentPiece = board[newRow][newCol].currentPiece;
                        board[newRow][newCol].currentPiece = originalPiece;
                    }
                }
            }

            return validMoves; //return an array of the king valid moves 
        }







        /// <summary>
        /// Gets the valid moves of the King and Updetes the square on the board that one of colered team(Black or White) can move there(Only King piece).
        /// </summary>
        /// <param name="board">The board game</param>
        /// <param name="row">The row number the piece is standing on.</param>
        /// <param name="col">The col number the piece is standing on.</param>
        public void UpdateIsPotential(squareChess[][] board, int row, int col)
        {
            
                List<int[]> validMoves = GetValidKingMoves(board, row, col);//The list valid moves gets the list of all the valid moves  

                foreach (var move in validMoves)
                {
                    int newRow = move[0];//contain the row 
                    int newCol = move[1];//contain the col
                    if (this.Color == Color.White)//if its a white piece
                    {
                        board[newRow][newCol].isWhitePotentialMove = true;//the white king piece can go to that square 
                    }
                    else
                    {
                        board[newRow][newCol].isBlackPotentialMove = true;//the Black king piece can go to that square 
                    }
                }
            
            
        }


        /// <summary>
        /// Gets all the valid moves of the King piece and choose a random move to play agains the player.
        /// the function is called only when there is any valid moves
        /// </summary>
        /// <param name="board">The board game</param>
        /// <param name="row">The row number the piece is standing on.</param>
        /// <param name="col">The col number the piece is standing on.</param>
        /// <returns>A new position as a point or the current position if the piece stayed inplace</returns>
        public Point KingRandomMove(squareChess[][] board, int row, int col)
        {
            // Get all valid moves
            List<int[]> validMoves = GetValidKingMoves(board, row, col);

            int targetCol = row;
            int targetRow = col;
            // Check if there are valid moves
            if (validMoves.Count > 0)
            {
                // Choose a random move from the list
                Random random = new Random();
                int[] chosenMove = validMoves[random.Next(validMoves.Count)];

                targetRow = chosenMove[0];
                targetCol = chosenMove[1];

                // Perform the random move
                board[targetRow][targetCol].currentPiece = board[row][col].currentPiece; // Move king
                board[row][col].currentPiece = null; // Clear the previous square

                
                // Return the new position as a Point
                return new Point(targetRow, targetCol);
            }

            
            // If there are no valid moves, return the current position
            return new Point(row, col);
        }



    }

}

