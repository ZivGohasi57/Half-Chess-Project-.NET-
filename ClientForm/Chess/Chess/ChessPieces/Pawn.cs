using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Chess.ChessPieces
{
    internal class Pawn : ChessPiece
    {
        private int minCol = 0;
        private int maxCol = 3;
        private int maxRow = 7;
        private int minRow = 0;
        public bool isFirstmove = true;

        public Pawn(Color color)
        {
            this.Color = color;
        }
        
        /// <summary>
        /// calculating the valid moves of the Pawn piece.
        /// </summary>
        /// <param name="board">the board game.</param>
        /// <param name="row">The row number the piece is standing on.</param>
        /// <param name="col">The col number the piece is standing on.</param>
        /// <returns>The array that contains all the Pawn moves.</returns>
        public List<int[]> GetValidPawnMoves(squareChess[][] board, int row, int col)// also for black and white
        {

            int[][] BlackPawnMoves = new int[][]
            {
                new int[] { 1,0 },//up black 1
                new int[] {2,0 },//up black 2
                new int[] {0,1 },//right
                new int[] { 0,-1},//left
                new int[] {1,1 },//left diagonal
                new int[] { 1,-1 }//right diagonal

            };

            int[][] WhitePawnMoves = new int[][]
            {
                new int[] { -1,0 },//up white 1
                new int[] {-2,0 },//up white 2
                new int[] { 0,1 },//right
                new int[] { 0,-1 },//left
                new int[] { -1,1},//right diagonal
                new int[] { -1,-1}//left diagonal
            };

            int[][] WhitePawnMovessecound = new int[][]
            {
                new int[] { -1,0 },//up white 1
                new int[] { 0,1 },//right
                new int[] { 0,-1 },//left
                new int[] { -1,1},//right diagonal
                new int[] { -1,-1}//left diagonal
            };
            
            int[][] BlackPawnMovessecound = new int[][]
            {
                new int[] { 1,0 },//up white 1
                new int[] { 0,-1 },//right
                new int[] { 0,1 },//left
                new int[] { 1,-1},//right diagonal
                new int[] { 1,1}//left diagonal
            };


            List<int[]> validMoves = new List<int[]>();
            var moves = this.Color == Color.Black ? BlackPawnMovessecound : WhitePawnMovessecound;
            var firstMoves = this.Color == Color.Black ? BlackPawnMoves : WhitePawnMoves;

            


            foreach (var move in (isFirstmove ? firstMoves : moves))
            {
                int newRow = row + move[0];
                int newCol = col + move[1];


                if (newRow >= minRow && newRow <= maxRow && newCol >= minCol && newCol <= maxCol)
                {

                    if (Math.Abs(move[1]) == 1 && Math.Abs(move[0]) == 1 && board[newRow][newCol].currentPiece != null) //if the move is diagonal and there is a piece
                    {
                        if (board[newRow][newCol].currentPiece.Color != this.Color) // asks if on the square there is an enemy piece
                        {
                            validMoves.Add(new int[] { newRow, newCol });// then its a valid move
                        }
                    }
                    else if (Math.Abs(move[0]) == 2)//if you can move 2 squares
                    {
                        bool isBlocked = (this.Color == Color.Black && board[newRow - 1][newCol].currentPiece != null) ||
                                         (this.Color == Color.White && board[newRow + 1][newCol].currentPiece != null);// if there is a piece in front 

                        if (!isBlocked)// if false then its a valid move
                        {
                            validMoves.Add(new int[] { newRow, newCol });
                        }
                    }
                    else if (Math.Abs(move[1]) == 1 && Math.Abs(move[0]) == 1 && board[newRow][newCol].currentPiece == null)// if tne move is diagonal and there isnt a piece do nothing 
                    {
                       
                    }
                    else if (board[newRow][newCol].currentPiece == null) //if there 
                    {
                        validMoves.Add(new int[] { newRow, newCol });
                    }
                }
            }
            

            return validMoves;//return an array of the Pawn valid moves 
        }



        /// <summary>
        /// Gets the valid moves of the Pawn and Updetes the square on the board that one of colered team(Black or White) can move there(Only Pawn piece).
        /// </summary>
        /// <param name="board">The board game</param>
        /// <param name="row">The row number the piece is standing on.</param>
        /// <param name="col">The col number the piece is standing on.</param>
        public void UpdateIsPotential(squareChess[][] board, int row, int col)
        {
            // Get all valid moves
            List<int[]> validMoves = GetValidPawnMoves(board, row, col);

            // Mark all valid moves as potential moves
            foreach (var Updatemove in validMoves)
            {
                int UpdatNewRow = Updatemove[0];// contain the row
                int UpdatNewCol = Updatemove[1];// contain the col
                if (this.Color == Color.White)//if its a white piece
                {
                    board[UpdatNewRow][UpdatNewCol].isWhitePotentialMove = true;//the White Pawn piece can go to that square 
                }
                else
                {
                    board[UpdatNewRow][UpdatNewCol].isBlackPotentialMove = true;//the Black Pawn piece can go to that square 
                }
            }
        }


        /// <summary>
        /// Gets all the valid moves of the Pawn piece and choose a random move to play agains the player.
        ///the function is called only when there is any valid moves
        /// </summary>
        /// <param name="board">The board game</param>
        /// <param name="row">The row number the piece is standing on.</param>
        /// <param name="col">The col number the piece is standing on.</param>
        /// <returns>A new position as a point or the current position if the piece stayed inplace</returns>
        public Point PawnRandomMove(squareChess[][] board, int row, int col)
        {
            // Get all valid moves
            List<int[]> validMoves = GetValidPawnMoves(board, row, col);

            // Check if there are valid moves
            if (validMoves.Count > 0)
            {
                // Choose a random move from the list
                Random random = new Random();
                int[] chosenMove = validMoves[random.Next(validMoves.Count)];

                // Perform the random move
                int targetRow = chosenMove[0];
                int targetCol = chosenMove[1];

                // Move pawn to the new location
                board[targetRow][targetCol].currentPiece = board[row][col].currentPiece; // Move pawn
                board[row][col].currentPiece = null; // Clear the previous square
                this.currentRow = targetRow;
                this.currentCol = targetCol;

                isFirstmove = false; // Mark as no longer the first move

                // Return the new position as a Point
                return new Point(targetRow, targetCol);
            }

            // If there are no valid moves, return the current position
            return new Point(row, col);
        }

    }
}
