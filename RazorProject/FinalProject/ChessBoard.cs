using Newtonsoft.Json.Bson;
using System;
using FinalProject.ChessPieces;
using System.Drawing;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject
{
    public class ChessBoard
    {

        


        private char[] chars { get; } = { 'A', 'B', 'C', 'D' };


        public squareChess[][] board { get; set; }

        public static bool isCheck { get; set; } = false;

        public bool userTurn { get; set; } = true;

        public bool severTurn { get; set; } = false;



        King BlackKing;
        Bishop BlackBishop;
        Knight BlackKnight;
        Rook BlackRook;
        Pawn BlackPawn1;
        Pawn BlackPawn2;
        Pawn BlackPawn3;
        Pawn BlackPawn4;


        King WhiteKing;
        Bishop WhiteBishop;
        Knight WhiteKnight;
        Rook WhiteRook;
        Pawn WhitePawn1;
        Pawn WhitePawn2;
        Pawn WhitePawn3;
        Pawn WhitePawn4;




        public ChessBoard()
        {
            BlackKing = new King(Color.Black);
            BlackBishop = new Bishop(Color.Black);
            BlackKnight = new Knight(Color.Black);
            BlackRook = new Rook(Color.Black);
            BlackPawn1 = new Pawn(Color.Black);
            BlackPawn2 = new Pawn(Color.Black);
            BlackPawn3 = new Pawn(Color.Black);
            BlackPawn4 = new Pawn(Color.Black);


            WhiteKing = new King(Color.White);
            WhiteBishop = new Bishop(Color.White);
            WhiteKnight = new Knight(Color.White);
            WhiteRook = new Rook(Color.White);
            WhitePawn1 = new Pawn(Color.White);
            WhitePawn2 = new Pawn(Color.White);
            WhitePawn3 = new Pawn(Color.White);
            WhitePawn4 = new Pawn(Color.White);
            board = initBoard();


            StartPlacePieces(board);
        }

        public squareChess[][] initBoard()
        {
            board = new squareChess[8][];

            for (int i = 0; i < 8; i++)
            {
                board[i] = new squareChess[4];

                for (int j = 0; j < 4; j++)
                {
                    string squareName = $"{chars[j]}{i + 1}";
                    board[i][j] = new squareChess(squareName);
                }
            }
            return board;
        }

        public void StartPlacePieces(squareChess[][] board)
        {

            board[0][0].currentPiece = BlackKing;
            BlackKing.currentRow = 0;
            BlackKing.currentCol = 0;
            BlackKing.UpdateIsPotential(board, BlackKing.currentRow, BlackKing.currentCol);

            board[0][1].currentPiece = BlackBishop;
            BlackBishop.currentRow = 0;
            BlackBishop.currentCol = 1;
            BlackBishop.UpdateIsPotential(board, BlackBishop.currentRow, BlackBishop.currentCol);

            board[0][2].currentPiece = BlackKnight;
            BlackKnight.currentRow = 0;
            BlackKnight.currentCol = 2;
            BlackKnight.UpdateIsPotential(board, BlackKnight.currentRow, BlackKnight.currentCol);

            board[0][3].currentPiece = BlackRook;
            BlackRook.currentRow = 0;
            BlackRook.currentCol = 3;
            BlackRook.UpdateIsPotential(board, BlackRook.currentRow, BlackRook.currentCol);

            board[1][0].currentPiece = BlackPawn1;
            BlackPawn1.currentRow = 1;
            BlackPawn1.currentCol = 0;
            BlackPawn1.UpdateIsPotential(board, BlackPawn1.currentRow, BlackPawn1.currentCol);

            board[1][1].currentPiece = BlackPawn2;
            BlackPawn2.currentRow = 1;
            BlackPawn2.currentCol = 1;
            BlackPawn2.UpdateIsPotential(board, BlackPawn2.currentRow, BlackPawn2.currentCol);

            board[1][2].currentPiece = BlackPawn3;
            BlackPawn3.currentRow = 1;
            BlackPawn3.currentCol = 2;
            BlackPawn3.UpdateIsPotential(board, BlackPawn3.currentRow, BlackPawn3.currentCol);

            board[1][3].currentPiece = BlackPawn4;
            BlackPawn4.currentRow = 1;
            BlackPawn4.currentCol = 3;
            BlackPawn4.UpdateIsPotential(board, BlackPawn4.currentRow, BlackPawn4.currentCol);




            board[6][0].currentPiece = WhitePawn1;
            WhitePawn1.currentRow = 6;
            WhitePawn1.currentCol = 0;
            WhitePawn1.UpdateIsPotential(board, WhitePawn1.currentRow, WhitePawn1.currentCol);

            board[6][1].currentPiece = WhitePawn2;
            WhitePawn2.currentRow = 6;
            WhitePawn2.currentCol = 1;
            WhitePawn2.UpdateIsPotential(board, WhitePawn2.currentRow, WhitePawn2.currentCol);

            board[6][2].currentPiece = WhitePawn3;
            WhitePawn3.currentRow = 6;
            WhitePawn3.currentCol = 2;
            WhitePawn3.UpdateIsPotential(board, WhitePawn3.currentRow, WhitePawn3.currentCol);

            board[6][3].currentPiece = WhitePawn4;
            WhitePawn4.currentRow = 6;
            WhitePawn4.currentCol = 3;
            WhitePawn4.UpdateIsPotential(board, WhitePawn4.currentRow, WhitePawn4.currentCol);

            board[7][0].currentPiece = WhiteKing;
            WhiteKing.currentRow = 7;
            WhiteKing.currentCol = 0;
            WhiteKing.UpdateIsPotential(board, WhiteKing.currentRow, WhiteKing.currentCol);

            board[7][1].currentPiece = WhiteBishop;
            WhiteBishop.currentRow = 7;
            WhiteBishop.currentCol = 1;
            WhiteBishop.UpdateIsPotential(board, WhiteBishop.currentRow, WhiteBishop.currentCol);

            board[7][2].currentPiece = WhiteKnight;
            WhiteKnight.currentRow = 7;
            WhiteKnight.currentCol = 2;
            WhiteKnight.UpdateIsPotential(board, WhiteKnight.currentRow, WhiteKnight.currentCol);

            board[7][3].currentPiece = WhiteRook;
            WhiteRook.currentRow = 7;
            WhiteRook.currentCol = 3;
            WhiteRook.UpdateIsPotential(board, WhiteRook.currentRow, WhiteRook.currentCol);




        }

        public void ServerRandomPlay(int delay)
        {
            TimeSpan duration = TimeSpan.FromMilliseconds(delay);
            if (isCheck)
            {

                int KingRow = BlackKing.currentRow;
                int KingCol = BlackKing.currentCol;
                Point kingStartPosition = new Point(BlackKing.currentRow, BlackKing.currentCol);


                if (BlackKing.GetValidKingMoves(board, KingRow, KingCol).Count > 0)
                {
                    List<int[]> kingMoves = BlackKing.GetValidKingMoves(board, KingRow, KingCol);
                    List<int[]> kingValidCheckMoves = new List<int[]>();
                    foreach (var move in kingMoves)
                    {
                        int targetRow = move[0];
                        int targetCol = move[1];
                        if (board[targetRow][targetCol].isWhitePotentialMove == false)
                        {
                            kingValidCheckMoves.Add(new int[] { targetRow, targetCol });
                        }
                    }
                    Random random = new Random();
                    int[] avoidCheckMove = kingValidCheckMoves[random.Next(kingValidCheckMoves.Count())];
                    Point kingEndPosition = new Point(avoidCheckMove[0], avoidCheckMove[1]);

                    if (board[avoidCheckMove[0]][avoidCheckMove[1]].currentPiece != null)
                    {
                        board[avoidCheckMove[0]][avoidCheckMove[1]].currentPiece.isAlive = false;
                    }
                    board[avoidCheckMove[0]][avoidCheckMove[1]].currentPiece = board[KingRow][KingCol].currentPiece;
                    board[KingRow][KingCol].currentPiece = null;

                    BlackKing.currentRow = avoidCheckMove[0];
                    BlackKing.currentCol = avoidCheckMove[1];
                    isCheck = false;
                }
                else
                {
                    CheckMate();
                }
            }
            else
            {
                List<ChessPiece> aliveBlackTeam = BlackTeam();
                Random random = new Random();
                ChessPiece chosenPiece = aliveBlackTeam[random.Next(aliveBlackTeam.Count)];
                while (chosenPiece.FindValidPointsByPiece(chosenPiece, chosenPiece.currentRow, chosenPiece.currentCol, board).Any() == false)
                {
                    chosenPiece = aliveBlackTeam[random.Next(aliveBlackTeam.Count)];
                }

                int pieceCurrentRow = chosenPiece.currentRow;
                int pieceCurrentCol = chosenPiece.currentCol;


                Point startPosition = new Point(pieceCurrentRow, pieceCurrentCol);
                Point endPotision = chosenPiece.RandomMove(chosenPiece, pieceCurrentRow, pieceCurrentCol, board);
                chosenPiece.currentRow = endPotision.X;
                chosenPiece.currentCol = endPotision.Y;

            }
            severTurn = false;
            userTurn = true;


        }

        public void CheckMate()
        {

        }

        public void UpdatePotentialMoves(squareChess[][] board)
        {

            foreach (var row in board)
            {
                foreach (var square in row)
                {
                    square.isWhitePotentialMove = false;
                    square.isBlackPotentialMove = false;
                }
            }


            foreach (var row in board)
            {
                foreach (var square in row)
                {
                    if (square.currentPiece != null)
                    {
                        square.currentPiece.UpdatePotential(square.currentPiece, square.currentPiece.currentRow, square.currentPiece.currentCol, board);
                    }
                }
            }
        }

        public List<Point> GetValidMoves(Point position)
        {
            int row = position.X;
            int col = position.Y;

            if (board[row][col].currentPiece == null)
            {
                return new List<Point>();
            }

            ChessPiece piece = board[row][col].currentPiece;

            List<int[]> validMoves;

            if (piece is King king)
            {
                validMoves = king.GetValidKingMoves(board, row, col);
            }
            else if (piece is Bishop bishop)
            {
                validMoves = bishop.GetValidBishopMoves(board, row, col);
            }
            else if (piece is Knight knight)
            {
                validMoves = knight.GetValidKnightMoves(board, row, col);
            }
            else if (piece is Rook rook)
            {
                validMoves = rook.GetValidRookMoves(board, row, col);
            }
            else if (piece is Pawn pawn)
            {
                validMoves = pawn.GetValidPawnMoves(board, row, col);
            }
            else
            {
                validMoves = new List<int[]>();
            }

            return validMoves.Select(move => new Point(move[0], move[1])).ToList();
        }

        public List<ChessPiece> BlackTeam()
        {
            List<ChessPiece> BlackTeam = new List<ChessPiece>
            {
                BlackKing,
                BlackKnight,
                BlackBishop,
                BlackRook,
                BlackPawn1,
                BlackPawn2,
                BlackPawn3,
                BlackPawn4
            };
            var aliveBlackTeam = BlackTeam.Where(a => a.isAlive).ToList();
            return aliveBlackTeam;

        }

        private List<ChessPiece> WhiteTeam()
        {
            List<ChessPiece> WhiteTeam = new List<ChessPiece>
        {
            WhiteKing,
            WhiteBishop,
            WhiteKnight,
            WhiteRook,
            WhitePawn1,
            WhitePawn2,
            WhitePawn3,
            WhitePawn4

        };
            var aliveWhiteTeam = WhiteTeam.Where(a => a.isAlive).ToList();
            return aliveWhiteTeam;
        }

        public List<int[]> AllTargetPoints(Color color, squareChess[][] board)
        {
            List<int[]> points = new List<int[]>();

            var whichPoints = color == Color.Black ? BlackTeam() : WhiteTeam();
            foreach (ChessPiece piece in whichPoints)
            {

                List<int[]> piecePoints = piece.FindValidPointsByPiece(piece, piece.currentRow, piece.currentCol, board);
                foreach (var move in piecePoints)
                {

                    points.Add(move);
                }

            }
            return points;
        }

        public void CheckIfChecked()
        {

            int[] blackKingPosition = GetKingCurrentPosition(Color.Black, board);
            int[] whiteKingPosition = GetKingCurrentPosition(Color.White, board);


            List<int[]> whiteTargets = AllTargetPoints(Color.White, board);
            List<int[]> blackTargets = AllTargetPoints(Color.Black, board);


            bool blackKingInCheck = whiteTargets.Any(target => target[0] == blackKingPosition[0] && target[1] == blackKingPosition[1]);
            bool whiteKingInCheck = blackTargets.Any(target => target[0] == whiteKingPosition[0] && target[1] == whiteKingPosition[1]);


            if (blackKingInCheck)
            {
                board[blackKingPosition[0]][blackKingPosition[1]].isBlackPotentialMove = true;
                isCheck = true;
            }
            else
            {
            }

            if (whiteKingInCheck)
            {
                isCheck = true;
                board[whiteKingPosition[0]][whiteKingPosition[1]].isWhitePotentialMove = true;
            }
            else
            {
            }
        }

        public int[] GetKingCurrentPosition(Color color, squareChess[][] bord)
        {
            int[] kingCurrentPosition = new int[2];


            if (color == Color.Black)
            {
                kingCurrentPosition[0] = BlackKing.currentRow;
                kingCurrentPosition[1] = BlackKing.currentCol;
            }
            else
            {
                kingCurrentPosition[0] = WhiteKing.currentRow;
                kingCurrentPosition[1] = WhiteKing.currentCol;
            }
            return kingCurrentPosition;

        }

    }
}

