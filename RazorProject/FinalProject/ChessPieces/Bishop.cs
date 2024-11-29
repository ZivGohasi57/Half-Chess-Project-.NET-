using System.Drawing;

namespace FinalProject.ChessPieces
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

        public List<int[]> GetValidBishopMoves(squareChess[][] board, int row, int col)
        {
            int[][] bishopMoves = new int[][]
            {
                new int[] { 1, 1 },
                new int[] { 1, -1 },
                new int[] { -1, 1 },
                new int[] { -1, -1 },
            };




            List<int[]> validMoves = new List<int[]>();



            foreach (var move in bishopMoves)
            {
                int newRow = row + move[0];
                int newCol = col + move[1];
                ;

                while (newRow >= minRow && newRow <= maxRow && newCol >= minCol && newCol <= maxCol)
                {
                    if (board[newRow][newCol].currentPiece == null)
                    {
                        validMoves.Add(new int[] { newRow, newCol });
                        newRow += move[0];
                        newCol += move[1];

                    }
                    else
                    {
                        if (board[newRow][newCol].currentPiece.Color != this.Color)
                        {
                            validMoves.Add(new int[] { newRow, newCol });
                            break;

                        }
                        break;
                    }

                }
            }



            return validMoves;
        }

        public void UpdateIsPotential(squareChess[][] board, int row, int col)
        {
            List<int[]> validMoves = GetValidBishopMoves(board, row, col);

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

        public Point BishopRandomMove(squareChess[][] board, int row, int col)
        {
            List<int[]> validMoves = GetValidBishopMoves(board, row, col);

            if (validMoves.Count > 0)
            {
                Random random = new Random();
                int[] chosenMove = validMoves[random.Next(validMoves.Count)];

                int targetRow = chosenMove[0];
                int targetCol = chosenMove[1];


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