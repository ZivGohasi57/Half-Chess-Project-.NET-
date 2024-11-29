using System.Collections.Generic;
using System.Drawing;
using System;
using System.Windows.Forms;
using System.Diagnostics.Eventing.Reader;


namespace Chess.ChessPieces
{
    internal class Bishop : ChessPiece
    {
        int minRow = 0;
        int minCol = 0;
        int maxRow = 7;
        int maxCol = 3;


        public Bishop(Color color)
        {
            this.Color = color;
        }



        /// <summary>
        /// calculating the valid moves of the Bishop piece.
        /// </summary>
        /// <param name="board">The board game</param>
        /// <param name="row">The col number the piece is standing on.</param>
        /// <param name="col">The col number the piece is standing on.</param>
        /// <returns>The array that contains all the Bishop moves.</returns>
        public List<int[]> GetValidBishopMoves(squareChess[][] board, int row, int col)
        {
            int[][] bishopMoves = new int[][] //valid moves of the bishopMoves
            {
                new int[] { 1, 1 },
                new int[] { 1, -1 },
                new int[] { -1, 1 },
                new int[] { -1, -1 },
            };

         

            List<int[]> validMoves = new List<int[]>();
            


            foreach (var move in bishopMoves)
            {
                int newRow = row + move[0]; //takes the move and adding it to row
                int newCol = col + move[1]; // takes the move and adding it to col
                ;

                while (newRow >= minRow && newRow <= maxRow && newCol >= minCol && newCol <= maxCol) //validete that the pieces wont pass the bord
                {
                    if (board[newRow][newCol].currentPiece == null) // if there isnt a piece its a valide move
                    {
                        validMoves.Add(new int[] { newRow, newCol });
                        newRow += move[0];
                        newCol += move[1];

                    }
                    else
                    {
                        if (board[newRow][newCol].currentPiece.Color != this.Color) //if its an anemy piece its a valide move
                        {
                            validMoves.Add(new int[] { newRow, newCol });
                            break;

                        }
                        break;
                    }
                    
                }
            }
            


            return validMoves;//return an array of the bishop valid moves points
        }



        /// <summary>
        ///Gets the valid moves of the Bishop and Updetes the square on the board that one of colered team(Black or White) can move there(Only Bishop piece). 
        /// </summary>
        /// <param name="board">The board game</param>
        /// <param name="row">The row number the piece is standing on.</param>
        /// <param name="col">The col number the piece is standing on.</param>
        
        public void UpdateIsPotential(squareChess[][] board, int row, int col)
        {
            List<int[]> validMoves = GetValidBishopMoves(board, row, col);//The list valid moves gets the list of all the valid moves  

            foreach (var move in validMoves)
            {
                int newRow = move[0];//contain the row 
                int newCol = move[1];//contain the col 
                if (this.Color == Color.White)//if its a white piece
                {
                    board[newRow][newCol].isWhitePotentialMove = true;//the white Bishop piece can go to that square 
                }
                else
                {
                    board[newRow][newCol].isBlackPotentialMove = true;//the Black Bishop piece can go to that square 
                }
            }
        }

        /// <summary>
        /// Gets all the valid moves of the Bishop piece and choose a random move to play agains the player.
        ///the function is called only when there is any valid moves
        /// </summary>
        /// <param name="board">The Board game</param>
        /// <param name="row">The row number the piece is standing on.</param>
        /// <param name="col">The col number the piece is standing on.</param>
        /// <returns>A new position as a point or the current position if the piece stayed inplace </returns>
        public Point BishopRandomMove(squareChess[][] board, int row, int col)
        {
            // get all valid moves by bishop
            List<int[]> validMoves = GetValidBishopMoves(board, row, col);

            if (validMoves.Count > 0)
            {
                // choose a random move
                Random random = new Random();
                int[] chosenMove = validMoves[random.Next(validMoves.Count)];

                int targetRow = chosenMove[0];
                int targetCol = chosenMove[1];

                // execute random move
                board[targetRow][targetCol].currentPiece = board[row][col].currentPiece; 
                board[row][col].currentPiece = null;
                this.currentRow = targetRow;
                this.currentCol = targetCol;

                // return the new position as a point
                return new Point(targetRow, targetCol);
            }

            // if no valid moves, return current position
            return new Point(row, col);
        }




    }
}
