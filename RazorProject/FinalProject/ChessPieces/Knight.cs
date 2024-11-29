using FinalProject;
using System.Drawing;

namespace FinalProject.ChessPieces
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

            return validMoves;
        }



        public void UpdateIsPotential(squareChess[][] board, int row, int col)
        {
            List<int[]> validMoves = GetValidKnightMoves(board, row, col);

            foreach (var move in validMoves)
            {
                int newRow = move[0];
                int newCol = move[1];
                if (this.Color == Color.White)
                {
                    board[newRow][newCol].isWhitePotentialMove = true;
                }
                else
                {
                    board[newRow][newCol].isBlackPotentialMove = true;
                }
            }
        }

        public Point KnightRandomMove(squareChess[][] board, int row, int col)
        {
            List<int[]> validMoves = GetValidKnightMoves(board, row, col);

            if (validMoves.Count > 0)
            {
                Random random = new Random();
                int[] chosenMove = validMoves[random.Next(validMoves.Count)];

                int targetRow = chosenMove[0];
                int targetCol = chosenMove[1];



                // Perform the random move
                board[targetRow][targetCol].currentPiece = board[row][col].currentPiece;
                board[row][col].currentPiece = null;
                this.currentRow = targetRow;
                this.currentCol = targetCol;
                return new Point(targetRow, targetCol);
            }
            return new Point(row, col);
        }

    }
}
