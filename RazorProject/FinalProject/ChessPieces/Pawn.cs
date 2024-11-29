using System.Drawing;

namespace FinalProject.ChessPieces
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

        public List<int[]> GetValidPawnMoves(squareChess[][] board, int row, int col)
        {

            int[][] BlackPawnMoves = new int[][]
            {
                new int[] { 1,0 },
                new int[] {2,0 },
                new int[] {0,1 },
                new int[] { 0,-1},
                new int[] {1,1 },
                new int[] { 1,-1 }

            };

            int[][] WhitePawnMoves = new int[][]
            {
                new int[] { -1,0 },
                new int[] {-2,0 },
                new int[] { 0,1 },
                new int[] { 0,-1 },
                new int[] { -1,1},
                new int[] { -1,-1}
            };

            int[][] WhitePawnMovessecound = new int[][]
            {
                new int[] { -1,0 },
                new int[] { 0,1 },
                new int[] { 0,-1 },
                new int[] { -1,1},
                new int[] { -1,-1}
            };

            int[][] BlackPawnMovessecound = new int[][]
            {
                new int[] { 1,0 },
                new int[] { 0,-1 },
                new int[] { 0,1 },
                new int[] { 1,-1},
                new int[] { 1,1}
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
                    if (Math.Abs(move[1]) == 1 && Math.Abs(move[0]) == 1 && board[newRow][newCol].currentPiece != null)
                    {
                        if (board[newRow][newCol].currentPiece.Color != this.Color)
                        {
                            validMoves.Add(new int[] { newRow, newCol });
                        }
                    }
                    else if (Math.Abs(move[1]) == 1 && Math.Abs(move[0]) == 1 && board[newRow][newCol].currentPiece == null)
                    {

                    }
                    else if (Math.Abs(move[0]) == 2)
                    {
                        if (this.Color == Color.Black && board[newRow - 1][newCol].currentPiece == null)
                        {
                            validMoves.Add(new int[] { newRow, newCol });
                        }
                        if (this.Color == Color.White && board[newRow + 1][newCol].currentPiece == null)
                        {
                            validMoves.Add(new int[] { newRow, newCol });
                        }


                    }
                    else if (board[newRow][newCol].currentPiece == null)
                    {
                        validMoves.Add(new int[] { newRow, newCol });
                    }
                }
            }


            return validMoves;
        }

        public void UpdateIsPotential(squareChess[][] board, int row, int col)
        {
            List<int[]> validMoves = GetValidPawnMoves(board, row, col);

            foreach (var Updatemove in validMoves)
            {
                int UpdatNewRow = Updatemove[0];
                int UpdatNewCol = Updatemove[1];
                if (this.Color == Color.White)
                {
                    board[UpdatNewRow][UpdatNewCol].isWhitePotentialMove = true;
                }
                else
                {
                    board[UpdatNewRow][UpdatNewCol].isBlackPotentialMove = true;
                }
            }
        }

        public Point PawnRandomMove(squareChess[][] board, int row, int col)
        {
            List<int[]> validMoves = GetValidPawnMoves(board, row, col);

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

                isFirstmove = false;

                return new Point(targetRow, targetCol);
            }

            return new Point(row, col);
        }

    }
}