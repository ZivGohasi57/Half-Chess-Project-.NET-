using Chess.ChessPieces;
using Chess.Model;
using Chess.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Chess
{
    public partial class Board : Form
    {
        static HttpClient client = new HttpClient(); // Creates a static HttpClient instance to send HTTP requests efficiently and reuse it throughout the application's lifecycle

        private const string PATH = "https://localhost:7243/"; // Defines the base URL for the HTTP requests

        private const int ROWS = 8;
        private const int COLUMNS = 4;
        private const int SQUARE_SIZE = 60;
        public PictureBox[,] chessSquares = new PictureBox[ROWS, COLUMNS];
        private ChessBoard chessBoard;//represents all the chess game
        private PictureBox selectedSquare = null;
        private Label serverTimerLabel;
        private Label userTimerLabel;
        private Timer gameTimer;
        private int serverTime;
        private int userTime;
        private DataGridView movesTable;//the table that represent the moves that has been done and shows them.
        private int userTurnElapsedTime = 0; // The time counted during the user's turn.


        // The varieables that we send to the api TblGames controller
        private int moveCount = 0;
        public string result = "DNF";
        public DateTime endDate;
        private DateTime startDate = DateTime.Now;
        private TblUsers user = null;
        private List<TblMoves> moves = new List<TblMoves>();







        /// <summary>
        /// Constructor that initializes and sets up the game board, timers, and user details.
        /// </summary>
        /// <param name="initialTime">The initial time (in seconds) for the game timers.</param>
        /// <param name="userLogIn">The logged-in user's information.</param>
        public Board(int initialTime, TblUsers userLogIn)
        {


            InitializeComponent();

            serverTime = initialTime; // Initialize the server's starting time
            userTime = initialTime; // Initialize the user's starting time
            this.Resize += Board_Resize; // Subscribe to the Resize event to handle window resizing


            user = userLogIn;// Store the logged-in user details

            InitializeBoard();// Set up the board elements

            InitializeTimers();// Set up and start timers for the game



            InitializeGameTimer();  // Set up the main game timer

            InitializeMovesTable(); // Set up the table for tracking game moves

            InitializeGame(); // Start the game initialization process


        }

        /// <summary>
        /// Handles the form's closing event and sends updated game data to the API if the user closes the form.
        /// </summary>
        /// <param name="e">The event data related to the form-closing action.</param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            // Check if the user closed the form by clicking on the X button

            if (e.CloseReason == CloseReason.UserClosing)
            {
                //send the game updeted data to api
                GetGameDataAndSend();


            }
        }



        /// <summary>
        /// Initializes the server and user timers, displaying their countdowns in the top-right and bottom-right corners of the form.
        /// </summary>
        private void InitializeTimers()
        {
            // Server timer label - displayed at the top-right corner
            serverTimerLabel = new Label
            {
                Text = $"Server: {serverTime / 60:D2}:{serverTime % 60:D2}", // Set initial time from the previous form
                AutoSize = true,
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(this.ClientSize.Width - 120, 10), // Position at the top-right corner
                Anchor = AnchorStyles.Top | AnchorStyles.Right // Maintain position at the top-right corner when resizing
            };
            Controls.Add(serverTimerLabel);

            // User timer label - displayed at the bottom-right corner
            userTimerLabel = new Label
            {
                Text = $"User: {userTime / 60:D2}:{userTime % 60:D2}", 
                AutoSize = true,
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(this.ClientSize.Width - 120, this.ClientSize.Height - 40), // ימין למטה
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right 
            };
            Controls.Add(userTimerLabel);
        }



        /// <summary>
        /// Initializes the game timer to update every second and starts it.
        /// </summary>
        private void InitializeGameTimer()
        {
            gameTimer = new Timer
            {
                Interval = 1000  // Set the timer interval to 1 second (1000 milliseconds)
            };
            gameTimer.Tick += UpdateTimers;// Subscribe to the Tick event to update timers on each interval
            gameTimer.Start();
        }


        /// <summary>
        /// Updates the server and user timers on each tick, managing countdowns and displaying them on the UI.
        /// </summary>
        /// <param name="sender">The source of the event (the Timer).</param>
        /// <param name="e">Event arguments.</param>
        private void UpdateTimers(object sender, EventArgs e)
        {
            if (serverTime > 0 && chessBoard.serverTurn == true)//check if its server turn => count down the timer  
            {
                serverTime--;
            }

            if (userTime > 0 && chessBoard.userTurn == true)//check if its user turn => count down the timer  
            {
                userTime--;
                userTurnElapsedTime++;  // Track time elapsed during the user's turn
            }

            serverTimerLabel.Text = $"Server: {serverTime / 60:D2}:{serverTime % 60:D2}";
            userTimerLabel.Text = $"User: {userTime / 60:D2}:{userTime % 60:D2}";

            if (serverTime == 0 || userTime == 0)
            {
                gameTimer.Stop();
                if (userTime == 0)
                {
                    MessageBox.Show("User Time's up!, Server is The Winner");
                    chessBoard.Winner = "Server";
                    GetGameDataAndSend();
                    
                }
                else if(serverTime == 0)
                {
                    MessageBox.Show("Server Time's up!, Server is The Winner");
                    chessBoard.Winner = "User";
                    GetGameDataAndSend();
                    
                }
                this.Close();
                
            }
        }





        /// <summary>
        /// Initializes the chessboard by calculating the size, positioning, and creating the squares for the board.
        /// </summary>
        private void InitializeBoard()
        {
            // Calculate the width and the hight of the squares of the board 
            int boardWidth = COLUMNS * SQUARE_SIZE;
            int boardHeight = ROWS * SQUARE_SIZE;

            // Calculate the offset to center the board on the form
            int offsetX = (this.ClientSize.Width - boardWidth) / 2;
            int offsetY = (this.ClientSize.Height - boardHeight) / 2;

            for (int row = 0; row < ROWS; row++)
            {
                for (int col = 0; col < COLUMNS; col++)
                {
                    var square = new PictureBox
                    {
                        Size = new Size(SQUARE_SIZE, SQUARE_SIZE),
                        // Set the size of each square
                        // Add the offset to the location of each square to center it on the form
                        Location = new Point(offsetX + col * SQUARE_SIZE, offsetY + row * SQUARE_SIZE),
                        BorderStyle = BorderStyle.FixedSingle,
                        // Add a border to each square
                        // Alternate the square color between white and gray based on the row and column
                        BackColor = (row + col) % 2 == 0 ? Color.White : Color.Gray,
                        Tag = new Point(row, col)// Store the position (row, column) of each square
                    };

                    square.Click += Square_Click; // Attach the event handler for the square click
                    Controls.Add(square); 
                    chessSquares[row, col] = square; 
                }
            }
        }




        /// <summary>
        /// Initializes the moves table (DataGridView) to display the chess moves, including the piece, start and end positions, and time taken for each move.
        /// </summary>
        private void InitializeMovesTable()
        {
            // Create DataGridView
            movesTable = new DataGridView
            {
                Width = 250,
                Height = this.ClientSize.Height - 20, 
                Location = new Point(10, 10), // Position the table on the left side of the form
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,// Automatically resize columns to fill available space
                AllowUserToAddRows = false,// Prevent the user from adding new rows
                ReadOnly = true,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,// Adjust the header height automatically
                RowHeadersVisible = false, // Hide the row headers (leftmost column)
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,// Disable selecting multiple rows at once
                BackgroundColor = Color.LightGray,
                BorderStyle = BorderStyle.Fixed3D
            };

            // Add columns to the table
            movesTable.Columns.Add("Piece", "Piece");
            movesTable.Columns.Add("From", "From"); 
            movesTable.Columns.Add("To", "To"); 
            movesTable.Columns.Add("Time", "Time"); 

            // Style the table cells
            movesTable.DefaultCellStyle.Font = new Font("Arial", 10);
            movesTable.DefaultCellStyle.ForeColor = Color.Black;
            movesTable.DefaultCellStyle.BackColor = Color.White;
            movesTable.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            movesTable.DefaultCellStyle.SelectionForeColor = Color.Black;
            movesTable.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            movesTable.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            movesTable.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            movesTable.EnableHeadersVisualStyles = false;

            // Set the width of each column
            movesTable.Columns["Piece"].Width = 140; 
            movesTable.Columns["From"].Width = 60;  
            movesTable.Columns["To"].Width = 60;    
            movesTable.Columns["Time"].Width = 60;  

            Controls.Add(movesTable);
        }








        /// <summary>
        /// Initializes the game by setting up the chessboard and updating the board's UI.
        /// </summary>
        private void InitializeGame()
        {

            // Create a new instance of the ChessBoard class and pass the current form (this) as a parameter
            chessBoard = new ChessBoard(this);

            // Update the UI to reflect the current state of the board
            UpdateBoardUI();


        }


        /// <summary>
        /// Updates the UI of the chessboard, including displaying the pieces and coloring the squares.
        /// </summary>
        private void UpdateBoardUI()
        {
            for (int row = 0; row < ROWS; row++)
            {

                for (int col = 0; col < COLUMNS; col++)
                {
                    var square = chessSquares[row, col];

                    var piece = chessBoard.board[row][col].currentPiece; // Get the piece at the current position


                    if (piece != null)
                    {
                        // If there is a piece on the square, display its image
                        square.Image = GetPieceImage(piece);
                        square.SizeMode = PictureBoxSizeMode.StretchImage;


                    }
                    else
                    {
                        // If there is no piece, clear the image from the square
                        square.Image = null;
                    }
                    // Alternate the square's background color between white and gray based on position
                    square.BackColor = (row + col) % 2 == 0 ? Color.White : Color.Gray;


                }


            }
            // Highlight the king if it is in check
            chessBoard.HighlightKingInCheck();

        }


        /// <summary>
        /// Checks if either king is in check.
        /// </summary>
        public void CheckIfChecked()
        {
            List<int[]> allAliveBlackValidePoints = chessBoard.AllTargetPoints(Color.Black, chessBoard.board);
            List<int[]> allAliveWhiteValidePoints = chessBoard.AllTargetPoints(Color.White, chessBoard.board);
            int[] blackKingPosition = chessBoard.GetKingCurrentPosition(Color.Black, chessBoard.board);
            int[] whiteKingPosition = chessBoard.GetKingCurrentPosition(Color.White, chessBoard.board);

            if (allAliveBlackValidePoints.Contains(whiteKingPosition))
            {

                chessSquares[blackKingPosition[0], blackKingPosition[1]].BackColor = Color.Red;
                ChessBoard.isCheck = true;
            }

            if (allAliveWhiteValidePoints.Contains(blackKingPosition))
            {

                chessSquares[whiteKingPosition[0], whiteKingPosition[1]].BackColor = Color.Red;
                ChessBoard.isCheck = true;
            }



        }

        /// <summary>
        /// Retrieves the image corresponding to the given chess piece based on its type and color.
        /// </summary>
        /// <param name="piece">The chess piece for which the image is being fetched.</param>
        /// <returns>An image representing the chess piece, or null if the piece is null.</returns>
        private Image GetPieceImage(ChessPiece piece)
        {
            if (piece == null) return null;

            string resourceName = $"{piece.GetType().Name}_{(piece.Color == Color.White ? "White" : "Black")}";
            return (Image)Properties.Resources.ResourceManager.GetObject(resourceName);
        }












        /// <summary>
        /// Handles the click event for a square on the chessboard. It selects a piece if none is selected, 
        /// highlights valid moves for the selected piece, and processes a move when a piece is selected.
        /// </summary>
        /// <param name="sender">The object that triggered the event, which is the clicked square (PictureBox).</param>
        /// <param name="e">Event arguments.</param>
        private async void Square_Click(object sender, EventArgs e)
        {
            // Cast the sender to a PictureBox and get the position of the clicked square from its Tag
            var clickedSquare = sender as PictureBox;
            var position = (Point)clickedSquare.Tag;

            // If no square is currently selected, check if the clicked square contains a piece and if it's the user's turn
            if (selectedSquare == null)
            {
                var piece = chessBoard.board[position.X][position.Y].currentPiece;
                // If a piece is found and it's the user's turn with a white piece, select the square and highlight valid moves
                if (piece != null && chessBoard.userTurn && piece.Color == Color.White)
                {
                    selectedSquare = clickedSquare;
                    HighlightValidMoves(position);

                    

                }
            }
            else
            {
                // If a square is already selected, determine the start and target positions
                var startPosition = (Point)selectedSquare.Tag;
                var targetPosition = position;

                // Check if the target position is a valid move for the selected piece
                if (chessBoard.GetValidMoves(startPosition).Contains(targetPosition))
                {


                    var movingPiece = chessBoard.board[startPosition.X][startPosition.Y].currentPiece;
                    // If the moving piece is a pawn, mark it as no longer being its first move
                    if (movingPiece is Pawn pawn)
                    {
                        pawn.isFirstmove = false;
                    }

                    // If there's a piece on the target square, mark it as "not alive"
                    if (chessBoard.board[targetPosition.X][targetPosition.Y].currentPiece != null)
                    {
                        chessBoard.board[targetPosition.X][targetPosition.Y].currentPiece.isAlive = false;
                    }

                    // Move the piece to the new position
                    movingPiece.currentRow = targetPosition.X;
                    movingPiece.currentCol = targetPosition.Y;


                    // Update the chessboard with the moved piece
                    chessBoard.board[targetPosition.X][targetPosition.Y].currentPiece =
                        chessBoard.board[startPosition.X][startPosition.Y].currentPiece;

                    // Clear the old position on the chessboard
                    chessBoard.board[startPosition.X][startPosition.Y].currentPiece = null;



                    // Switch turns: the user is done, and it's now the server's turn
                    chessBoard.userTurn = false;
                    chessBoard.serverTurn = true;
                    selectedSquare = null;

                    // Update potential moves for the next turn
                    chessBoard.UpdatePotentialMoves(chessBoard.board);

                    // Update the UI to reflect the move
                    UpdateBoardUI();

                    // Check if either king is in check after the move
                    CheckIfChecked();


                    // Add the move to the moves table and reset the timer
                    AddMoveToTable(movingPiece, startPosition, targetPosition, TimeSpan.FromSeconds(userTurnElapsedTime));
                    userTurnElapsedTime = 0;


                    // If it's the server's turn, simulate a server move with a delay to show a reel respond(for it to feel real to the user)
                    if (chessBoard.serverTurn)
                    {
                        Random random = new Random();
                        int delay = random.Next(1000, 5001);
                        await Task.Delay(delay);

                        // Perform the server's random move
                        chessBoard.ServerRandomPlay(delay);
                        UpdateBoardUI();
                    }
                }
                else
                {
                    // If the move is invalid, reset the board colors and check if either king is in check
                    ResetChessboardColors();

                    CheckIfChecked();
                    selectedSquare = null;
                }
            }
        }



        /// <summary>
        /// Highlights the valid moves for the selected piece on the chessboard.
        /// It resets the chessboard colors and then highlights the valid squares and the selected square. 
        /// </summary>
        /// <param name="positions">The position of the selected piece on the board.</param>
        private void HighlightValidMoves(Point positions)
        {
            ResetChessboardColors();// Reset the colors of all squares on the chessboard to default

            var validMoves = chessBoard.GetValidMoves(positions);// based on its position
            // Highlight the valid moves by changing the background color of each valid square to light green
            foreach (var move in validMoves)
            {
                var square = chessSquares[move.X, move.Y];
                square.BackColor = Color.LightGreen;
            }

            selectedSquare.BackColor = Color.Yellow;// Highlight the selected square by changing its background color to yellow

        }


        /// <summary>
        /// /// Resets the background colors of all squares on the chessboard to their default alternating pattern (white and gray).
        /// </summary>
        private void ResetChessboardColors()
        {
            for (int row = 0; row < ROWS; row++)
            {
                for (int col = 0; col < COLUMNS; col++)
                {
                    chessSquares[row, col].BackColor = (row + col) % 2 == 0 ? Color.White : Color.Gray;
                }
            }

        }

        //not in use
        private void Board_Load(object sender, EventArgs e)
        {




        }


        /// <summary>
        /// Resizes and repositions the chessboard and the moves table when the window is resized.
        /// </summary>
        /// <param name="sender">The object that triggered the resize event.</param>
        /// <param name="e">The event arguments for the resize event.</param>
        private void Board_Resize(object sender, EventArgs e)
        {
            // Adjust the width of the moves table to one third of the form's width
            movesTable.Width = this.ClientSize.Width / 3; 
            movesTable.Height = this.ClientSize.Height;

            // Calculate the dimensions for the chessboard
            int boardWidth = COLUMNS * SQUARE_SIZE;
            int boardHeight = ROWS * SQUARE_SIZE;

            // Calculate the horizontal and vertical offsets to center the chessboard on the form
            int offsetX = (this.ClientSize.Width - boardWidth - movesTable.Width) / 2;
            int offsetY = (this.ClientSize.Height - boardHeight) / 2; 

            // Resize and reposition each chess square
            for (int row = 0; row < ROWS; row++)
            {
                for (int col = 0; col < COLUMNS; col++)
                {
                    chessSquares[row, col].Size = new Size(SQUARE_SIZE, SQUARE_SIZE);
                    chessSquares[row, col].Location = new Point(offsetX + col * SQUARE_SIZE, offsetY + row * SQUARE_SIZE);
                }
            }

            // Reposition the timer labels
            serverTimerLabel.Location = new Point(this.ClientSize.Width - 120, 10);
            userTimerLabel.Location = new Point(this.ClientSize.Width - 120, this.ClientSize.Height - 40); 
        }


        /// <summary>
        /// Collects the game data, creates a TblGames object, and sends the game data to the server.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if sending the game data fails.</exception>
        public async void GetGameDataAndSend()
        {
            // Create a new TblGames object to store the game data
            var game = new TblGames
            {
                MovesCount = moves.Count,
                Result = result,
                StartDate = startDate,
                EndDate = DateTime.Now
            };

            // Send the game data to the server
            bool isSuccessful = await SendGameToApi(game);

            // Show an error message if the game data could not be sent
            if (!isSuccessful)
            {
                MessageBox.Show("Failed to send the Game to the server.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        ///  Sends the game data to the API to save it in the database.
        /// </summary>
        /// <param name="game">The game data to be sent to the server.</param>
        /// <returns>Returns true if the game was successfully sent; otherwise, false.</returns>
        public async Task<bool> SendGameToApi(TblGames game)
        {
            try
            {
                string apiUrl = "https://localhost:7243/Api/TblGames";// Define the API URL for sending game data

                using (HttpClient client = new HttpClient())
                {
                    string json = JsonConvert.SerializeObject(game);  // Serialize the game object to JSON format
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    // Send the POST request to the API
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);


                    // If the request was successful, show a success message
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Game added successfully!");
                        return true;
                    }
                    else
                    {
                        // If there was an error, show the error content and close the form
                        string errorContent = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Error: {response.StatusCode}\n{errorContent}");
                        this.Close();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                // If an exception occurred, show the error message and close the form
                MessageBox.Show($"Error: {ex.Message}");
                this.Close();
                return false;
            }
        }



        /// <summary>
        ///  Sends the move data to the API to save it in the database.
        /// </summary>
        /// <param name="move">The move data to be sent to the server.</param>
        /// <returns>Returns true if the move was successfully sent; otherwise, false.</returns>
        private async Task<bool> SendMoveToApi(TblMoves move)
        {
            using (HttpClient client = new HttpClient())
            {
                // Define the base address of the API
                client.BaseAddress = new Uri("https://localhost:7243/");
                HttpResponseMessage response;

                try
                {
                    string json = JsonConvert.SerializeObject(move); // Serialize the move object to JSON format
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    response = await client.PostAsync("Api/TblMoves", content);

                    // Send the POST request to the API
                    if (response.IsSuccessStatusCode)
                    {
                        return true; // Successfully sent the move
                    }
                    else
                    {
                        return false; // Failed to send the move
                    }
                }
                catch (Exception ex)
                {
                    // If an exception occurred, show the error message
                    MessageBox.Show($"Error sending move to the server: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }




        /// <summary>
        /// Adds the move details to the local moves table and sends the move data to the API.
        /// </summary>
        /// <param name="piece">The chess piece that made the move.</param>
        /// <param name="start">The starting position of the piece.</param>
        /// <param name="end">The ending position of the piece.</param>
        /// <param name="duration">The time duration the move took to complete.</param>
        public async void AddMoveToTable(ChessPiece piece, Point start, Point end, TimeSpan duration)
        {
            // Calculate move details
            string from = $"{(char)('A' + start.Y)}{8 - start.X}";
            string to = $"{(char)('A' + end.Y)}{8 - end.X}";
            string pieceType = piece.GetType().Name; // Example: "Pawn", "Knight", etc.
            int timeTaken = (int)Math.Ceiling(duration.TotalSeconds);

            // Add move to the local table in WinForms
            movesTable.Rows.Add(pieceType, from, to, timeTaken);

            if (movesTable.Rows.Count > 10)
            {
                movesTable.FirstDisplayedScrollingRowIndex = movesTable.Rows.Count - 1;
            }

            // Prepare the move to send to the Razor API
            var move = new TblMoves
            {
                GameId = 0, // Replace with actual GameId if available
                PieceType = pieceType,
                FromPosition = from,
                ToPosition = to,
                TimeTaken = timeTaken
            };
            



            
            
            bool isSuccessful = await SendMoveToApi(move);
            if (!isSuccessful)
            {
                MessageBox.Show("Failed to send the move to the server.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            moveCount++;// Add one move to the overall count of moves of a specific game
        }



        
        




    }
}


            
