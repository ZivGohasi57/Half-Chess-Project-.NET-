using FinalProject;
using System.Drawing;


namespace FinalProject.ChessPieces
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


        public List<int[]> GetValidKingMoves(squareChess[][] board, int row, int col)
        {
            int[][] kingMoves = new int[][]
            {
                new int[] { 1, 1 },
                new int[] { 0, -1 },
                new int[] { 1, 0 },
                new int[] { 1, -1 },
                new int[] { 0, 1 },
                new int[] { 0,0 },
                new int[] { -1,0 },
                new int[] { -1,1 },
            };

            List<int[]> validMoves = new List<int[]>();

            foreach (var move in kingMoves)
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
            List<int[]> validMoves = GetValidKingMoves(board, row, col);

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

        public Point KingRandomMove(squareChess[][] board, int row, int col)
        {
            List<int[]> validMoves = GetValidKingMoves(board, row, col);

            int targetCol = row;
            int targetRow = col;
            if (validMoves.Count > 0)
            {
                Random random = new Random();
                int[] chosenMove = validMoves[random.Next(validMoves.Count)];

                targetRow = chosenMove[0];
                targetCol = chosenMove[1];



                board[targetRow][targetCol].currentPiece = board[row][col].currentPiece;
                board[row][col].currentPiece = null;

                return new Point(targetRow, targetCol);
            }


            this.currentRow = targetRow;
            this.currentCol = targetCol;
            return new Point(row, col);
        }

    }

}    

