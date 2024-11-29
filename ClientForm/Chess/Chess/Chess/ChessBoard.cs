using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    public partial class ChessBoard : Form
    {
        public ChessBoard()
        {
            InitializeComponent();
            
        }

        private void ChessBoard_Load(object sender, EventArgs e)
        {
            CreateChessBoard();
        }


        private void CreateChessBoard()
        {
            tableLayoutPanel1.Controls.Clear();

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    Button button = new Button
                    {
                        Dock = DockStyle.Fill,
                        BackColor = (row + col) % 2 == 0 ? Color.Beige : Color.Sienna,
                        FlatStyle = FlatStyle.Flat,
                        Tag = new Point(row, col) // שמירת מיקום התא
                    };
                    button.FlatAppearance.BorderSize = 0;
                    //button.Click += Tile_Click; // אירוע לחיצה
                    tableLayoutPanel1.Controls.Add(button, col, row);
                }
            }
        }
    }
}
