using Chess.ChessPieces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Chess
{
    public class ChessBoard
    {
        private char[] chars { get; } = { 'A', 'B', 'C', 'D' };
        public string Winner { get; set; }
        private Board boardInstance;
        public squareChess[][] board { get; set; }
        public static bool isCheck { get; set; } = false;
        public bool userTurn { get; set; } = true;
        public bool serverTurn { get; set; } = false;
        private int random { get; set; }

        // Black pieces
        King BlackKing;
        Bishop BlackBishop;
        Knight BlackKnight;
        Rook BlackRook;
        Pawn BlackPawn1, BlackPawn2, BlackPawn3, BlackPawn4;

        // White pieces
        King WhiteKing;
        Bishop WhiteBishop;
        Knight WhiteKnight;
        Rook WhiteRook;
        Pawn WhitePawn1, WhitePawn2, WhitePawn3, WhitePawn4;

        // Constructor to initialize the chessboard
        public ChessBoard(Board boardInstance)
        {
            // Initialize pieces
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

            this.boardInstance = boardInstance;

            // Initialize and set up the board
            board = initBoard();
            StartPlacePieces(board);
        }

        // Initialize the board with squares
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

        // Place the chess pieces on their starting positions
        public void StartPlacePieces(squareChess[][] board)
        {
            // Black pieces
            this.board[0][0].currentPiece = BlackKing;
            BlackKing.currentRow = 0; BlackKing.currentCol = 0;
            BlackKing.UpdateIsPotential(board, BlackKing.currentRow, BlackKing.currentCol);

            this.board[0][1].currentPiece = BlackBishop;
            BlackBishop.currentRow = 0; BlackBishop.currentCol = 1;
            BlackBishop.UpdateIsPotential(board, BlackBishop.currentRow, BlackBishop.currentCol);

            this.board[0][2].currentPiece = BlackKnight;
            BlackKnight.currentRow = 0; BlackKnight.currentCol = 2;
            BlackKnight.UpdateIsPotential(board, BlackKnight.currentRow, BlackKnight.currentCol);

            this.board[0][3].currentPiece = BlackRook;
            BlackRook.currentRow = 0; BlackRook.currentCol = 3;
            BlackRook.UpdateIsPotential(board, BlackRook.currentRow, BlackRook.currentCol);

            // Black pawns
            this.board[1][0].currentPiece = BlackPawn1;
            BlackPawn1.currentRow = 1; BlackPawn1.currentCol = 0;
            BlackPawn1.UpdateIsPotential(board, BlackPawn1.currentRow, BlackPawn1.currentCol);

            this.board[1][1].currentPiece = BlackPawn2;
            BlackPawn2.currentRow = 1; BlackPawn2.currentCol = 1;
            BlackPawn2.UpdateIsPotential(board, BlackPawn2.currentRow, BlackPawn2.currentCol);

            this.board[1][2].currentPiece = BlackPawn3;
            BlackPawn3.currentRow = 1; BlackPawn3.currentCol = 2;
            BlackPawn3.UpdateIsPotential(board, BlackPawn3.currentRow, BlackPawn3.currentCol);

            this.board[1][3].currentPiece = BlackPawn4;
            BlackPawn4.currentRow = 1; BlackPawn4.currentCol = 3;
            BlackPawn4.UpdateIsPotential(board, BlackPawn4.currentRow, BlackPawn4.currentCol);

            // White pieces
            this.board[6][0].currentPiece = WhitePawn1;
            WhitePawn1.currentRow = 6; WhitePawn1.currentCol = 0;
            WhitePawn1.UpdateIsPotential(board, WhitePawn1.currentRow, WhitePawn1.currentCol);

            this.board[6][1].currentPiece = WhitePawn2;
            WhitePawn2.currentRow = 6; WhitePawn2.currentCol = 1;
            WhitePawn2.UpdateIsPotential(board, WhitePawn2.currentRow, WhitePawn2.currentCol);

            this.board[6][2].currentPiece = WhitePawn3;
            WhitePawn3.currentRow = 6; WhitePawn3.currentCol = 2;
            WhitePawn3.UpdateIsPotential(board, WhitePawn3.currentRow, WhitePawn3.currentCol);

            this.board[6][3].currentPiece = WhitePawn4;
            WhitePawn4.currentRow = 6; WhitePawn4.currentCol = 3;
            WhitePawn4.UpdateIsPotential(board, WhitePawn4.currentRow, WhitePawn4.currentCol);

            this.board[7][0].currentPiece = WhiteKing;
            WhiteKing.currentRow = 7; WhiteKing.currentCol = 0;
            WhiteKing.UpdateIsPotential(board, WhiteKing.currentRow, WhiteKing.currentCol);

            this.board[7][1].currentPiece = WhiteBishop;
            WhiteBishop.currentRow = 7; WhiteBishop.currentCol = 1;
            WhiteBishop.UpdateIsPotential(board, WhiteBishop.currentRow, WhiteBishop.currentCol);

            this.board[7][2].currentPiece = WhiteKnight;
            WhiteKnight.currentRow = 7; WhiteKnight.currentCol = 2;
            WhiteKnight.UpdateIsPotential(board, WhiteKnight.currentRow, WhiteKnight.currentCol);

            this.board[7][3].currentPiece = WhiteRook;
            WhiteRook.currentRow = 7; WhiteRook.currentCol = 3;
            WhiteRook.UpdateIsPotential(board, WhiteRook.currentRow, WhiteRook.currentCol);
        }

        // Handle the server's random play with a delay
        public void ServerRandomPlay(int delay)
        {
            TimeSpan duration = TimeSpan.FromMilliseconds(delay);

            if (isCheck)
            {
                // Logic to avoid check if the king is in check
                Point kingStartPosition = new Point(BlackKing.currentRow, BlackKing.currentCol);
                List<int[]> kingMoves = BlackKing.GetValidKingMoves(board, kingStartPosition.X, kingStartPosition.Y);
                List<int[]> kingValidCheckMoves = kingMoves
                    .Where(move => !board[move[0]][move[1]].isWhitePotentialMove)
                    .ToList();

                if (kingValidCheckMoves.Any())
                {
                    Random random = new Random();
                    int[] avoidCheckMove = kingValidCheckMoves[random.Next(kingValidCheckMoves.Count)];
                    Point kingEndPosition = new Point(avoidCheckMove[0], avoidCheckMove[1]);

                    if (board[avoidCheckMove[0]][avoidCheckMove[1]].currentPiece != null)
                    {
                        board[avoidCheckMove[0]][avoidCheckMove[1]].currentPiece.isAlive = false;
                    }

                    board[avoidCheckMove[0]][avoidCheckMove[1]].currentPiece = board[kingStartPosition.X][kingStartPosition.Y].currentPiece;
                    board[kingStartPosition.X][kingStartPosition.Y].currentPiece = null;

                    BlackKing.currentRow = avoidCheckMove[0];
                    BlackKing.currentCol = avoidCheckMove[1];
                    isCheck = false;

                    boardInstance.AddMoveToTable(BlackKing, kingStartPosition, kingEndPosition, duration);
                }
                else
                {
                    CheckMate();
                }
            }
            else
            {
                // Randomly choose a piece from the black team and make a valid move
                List<ChessPiece> aliveBlackTeam = BlackTeam();
                Random random = new Random();
                ChessPiece chosenPiece = aliveBlackTeam[random.Next(aliveBlackTeam.Count)];

                while (!chosenPiece.FindValidPointsByPiece(chosenPiece, chosenPiece.currentRow, chosenPiece.currentCol, board).Any())
                {
                    chosenPiece = aliveBlackTeam[random.Next(aliveBlackTeam.Count)];
                }

                int pieceCurrentRow = chosenPiece.currentRow;
                int pieceCurrentCol = chosenPiece.currentCol;
                Point startPosition = new Point(pieceCurrentRow, pieceCurrentCol);
                Point endPosition = chosenPiece.RandomMove(chosenPiece, pieceCurrentRow, pieceCurrentCol, board);

                boardInstance.AddMoveToTable(chosenPiece, startPosition, endPosition, duration);

                chosenPiece.currentRow = endPosition.X;
                chosenPiece.currentCol = endPosition.Y;
            }

            serverTurn = false;
            userTurn = true;
        }

        // Handle checkmate scenario
        public void CheckMate()
        {
            if (userTurn)
            {
                serverTurn = false;
                boardInstance.result = "Server";
                boardInstance.endDate = DateTime.Now;
                boardInstance.GetGameDataAndSend();
                MessageBox.Show("Server win by check mate");
                
            }
            else
            {
                userTurn = false;
                boardInstance.result = "User";
                boardInstance.endDate = DateTime.Now;
                boardInstance.GetGameDataAndSend();
                MessageBox.Show("User win by check mate");
                
            }
            boardInstance.Close();
        }

        // Update potential moves for all pieces on the board
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

        // Get valid moves for a specific piece
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

        // Get the list of alive black pieces
        public List<ChessPiece> BlackTeam()
        {
            List<ChessPiece> BlackTeam = new List<ChessPiece>
            {
                BlackKing, BlackKnight, BlackBishop, BlackRook, BlackPawn1, BlackPawn2, BlackPawn3, BlackPawn4
            };
            return BlackTeam.Where(a => a.isAlive).ToList();
        }

        // Get the list of alive white pieces
        public List<ChessPiece> WhiteTeam()
        {
            List<ChessPiece> WhiteTeam = new List<ChessPiece>
            {
                WhiteKing, WhiteBishop, WhiteKnight, WhiteRook, WhitePawn1, WhitePawn2, WhitePawn3, WhitePawn4
            };
            return WhiteTeam.Where(a => a.isAlive).ToList();
        }

        // Get all valid attack points for the specified color
        public List<int[]> AllTargetPoints(Color color, squareChess[][] board)
        {
            List<int[]> points = new List<int[]>();
            var whichPoints = color == Color.Black ? BlackTeam() : WhiteTeam();

            foreach (ChessPiece piece in whichPoints)
            {
                List<int[]> piecePoints = piece.FindValidPointsByPiece(piece, piece.currentRow, piece.currentCol, board);
                points.AddRange(piecePoints);
            }
            return points;
        }

        // Get the current position of the king for the specified color
        public int[] GetKingCurrentPosition(Color color, squareChess[][] board)
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

        // Highlight the king in check
        public void HighlightKingInCheck()
        {
            int[] blackKingPosition = GetKingCurrentPosition(Color.Black, board);
            int[] whiteKingPosition = GetKingCurrentPosition(Color.White, board);

            List<int[]> whiteTargets = AllTargetPoints(Color.White, board);
            List<int[]> blackTargets = AllTargetPoints(Color.Black, board);

            bool blackKingInCheck = whiteTargets.Any(target => target[0] == blackKingPosition[0] && target[1] == blackKingPosition[1]);
            bool whiteKingInCheck = blackTargets.Any(target => target[0] == whiteKingPosition[0] && target[1] == whiteKingPosition[1]);

            if (blackKingInCheck)
            {
                isCheck = true;
                board[blackKingPosition[0]][blackKingPosition[1]].isBlackPotentialMove = true;
                boardInstance.chessSquares[blackKingPosition[0], blackKingPosition[1]].BackColor = Color.Red;
            }
            else
            {
                boardInstance.chessSquares[blackKingPosition[0], blackKingPosition[1]].BackColor = (blackKingPosition[0] + blackKingPosition[1]) % 2 == 0 ? Color.White : Color.Gray;
            }

            if (whiteKingInCheck)
            {
                isCheck = true;
                board[whiteKingPosition[0]][whiteKingPosition[1]].isWhitePotentialMove = true;
                boardInstance.chessSquares[whiteKingPosition[0], whiteKingPosition[1]].BackColor = Color.Red;
            }
            else
            {
                boardInstance.chessSquares[whiteKingPosition[0], whiteKingPosition[1]].BackColor = (whiteKingPosition[0] + whiteKingPosition[1]) % 2 == 0 ? Color.White : Color.Gray;
            }
        }
    }
}
