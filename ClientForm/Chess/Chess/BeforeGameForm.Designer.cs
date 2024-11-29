namespace Chess
{
    partial class BeforeGameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.UserName = new System.Windows.Forms.Label();
            this.Timer_Label = new System.Windows.Forms.Label();
            this.Timer_ComboBox = new System.Windows.Forms.ComboBox();
            this.Start_Game = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // UserName
            // 
            this.UserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.UserName.Location = new System.Drawing.Point(78, 39);
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(329, 69);
            this.UserName.TabIndex = 0;
            this.UserName.Text = "Welcome UserName";
            this.UserName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.UserName.Click += new System.EventHandler(this.UserName_Click);
            // 
            // Timer_Label
            // 
            this.Timer_Label.AutoSize = true;
            this.Timer_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.Timer_Label.Location = new System.Drawing.Point(128, 171);
            this.Timer_Label.Name = "Timer_Label";
            this.Timer_Label.Size = new System.Drawing.Size(52, 20);
            this.Timer_Label.TabIndex = 1;
            this.Timer_Label.Text = "Timer";
            this.Timer_Label.Click += new System.EventHandler(this.Timer_Label_Click);
            // 
            // Timer_ComboBox
            // 
            this.Timer_ComboBox.FormattingEnabled = true;
            this.Timer_ComboBox.Location = new System.Drawing.Point(83, 214);
            this.Timer_ComboBox.Name = "Timer_ComboBox";
            this.Timer_ComboBox.Size = new System.Drawing.Size(157, 24);
            this.Timer_ComboBox.TabIndex = 2;
            this.Timer_ComboBox.SelectedIndexChanged += new System.EventHandler(this.Timer_ComboBox_SelectedIndexChanged);
            // 
            // Start_Game
            // 
            this.Start_Game.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.Start_Game.Location = new System.Drawing.Point(479, 309);
            this.Start_Game.Name = "Start_Game";
            this.Start_Game.Size = new System.Drawing.Size(250, 98);
            this.Start_Game.TabIndex = 3;
            this.Start_Game.Text = "Start Game";
            this.Start_Game.UseVisualStyleBackColor = true;
            this.Start_Game.Click += new System.EventHandler(this.Start_Game_Click);
            // 
            // BeforeGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 472);
            this.Controls.Add(this.Start_Game);
            this.Controls.Add(this.Timer_ComboBox);
            this.Controls.Add(this.Timer_Label);
            this.Controls.Add(this.UserName);
            this.Name = "BeforeGameForm";
            this.Text = "BeforeGameForm";
            this.Load += new System.EventHandler(this.BeforeGameForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label UserName;
        private System.Windows.Forms.Label Timer_Label;
        private System.Windows.Forms.ComboBox Timer_ComboBox;
        private System.Windows.Forms.Button Start_Game;
    }
}