namespace ChessSonification
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            btnLoadPGN = new Button();
            openFileDialog1 = new OpenFileDialog();
            lstGames = new ListBox();
            btnGenerateMIDI = new Button();
            btnPlay = new Button();
            player = new AxWMPLib.AxWindowsMediaPlayer();
            lblBoardSnapshot = new Label();
            lstMoves = new ListBox();
            txtMoveNumber = new TextBox();
            progressBar1 = new ProgressBar();
            btnNextMove = new Button();
            btnPrevMove = new Button();
            chkIsMono = new CheckBox();
            chkIncludeWhite = new CheckBox();
            chkIncludeBlack = new CheckBox();
            btnPause = new Button();
            btnStop = new Button();
            imgBoard = new PictureBox();
            lblMessages = new Label();
            chkTwoNote = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)player).BeginInit();
            ((System.ComponentModel.ISupportInitialize)imgBoard).BeginInit();
            SuspendLayout();
            // 
            // btnLoadPGN
            // 
            btnLoadPGN.Location = new Point(12, 12);
            btnLoadPGN.Name = "btnLoadPGN";
            btnLoadPGN.Size = new Size(97, 23);
            btnLoadPGN.TabIndex = 0;
            btnLoadPGN.Text = "Load PGN...";
            btnLoadPGN.UseVisualStyleBackColor = true;
            btnLoadPGN.Click += btnLoadPGN_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // lstGames
            // 
            lstGames.FormattingEnabled = true;
            lstGames.ItemHeight = 15;
            lstGames.Location = new Point(12, 41);
            lstGames.Name = "lstGames";
            lstGames.Size = new Size(369, 139);
            lstGames.TabIndex = 1;
            lstGames.SelectedIndexChanged += lstGames_SelectedIndexChanged;
            // 
            // btnGenerateMIDI
            // 
            btnGenerateMIDI.Location = new Point(12, 201);
            btnGenerateMIDI.Name = "btnGenerateMIDI";
            btnGenerateMIDI.Size = new Size(124, 23);
            btnGenerateMIDI.TabIndex = 2;
            btnGenerateMIDI.Text = "Generate MIDI";
            btnGenerateMIDI.UseVisualStyleBackColor = true;
            btnGenerateMIDI.Click += btnGenerateMIDI_Click;
            // 
            // btnPlay
            // 
            btnPlay.Image = Properties.Resources.control_play;
            btnPlay.Location = new Point(209, 201);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(33, 23);
            btnPlay.TabIndex = 3;
            btnPlay.UseVisualStyleBackColor = true;
            btnPlay.Click += btnPlay_Click;
            // 
            // player
            // 
            player.Enabled = true;
            player.Location = new Point(396, 12);
            player.Name = "player";
            player.OcxState = (AxHost.State)resources.GetObject("player.OcxState");
            player.Size = new Size(75, 23);
            player.TabIndex = 4;
            player.Visible = false;
            // 
            // lblBoardSnapshot
            // 
            lblBoardSnapshot.Font = new Font("Courier New", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblBoardSnapshot.Location = new Point(21, 253);
            lblBoardSnapshot.Name = "lblBoardSnapshot";
            lblBoardSnapshot.Size = new Size(407, 197);
            lblBoardSnapshot.TabIndex = 5;
            // 
            // lstMoves
            // 
            lstMoves.BackColor = SystemColors.ButtonFace;
            lstMoves.ColumnWidth = 150;
            lstMoves.FormattingEnabled = true;
            lstMoves.ItemHeight = 15;
            lstMoves.Location = new Point(456, 41);
            lstMoves.MultiColumn = true;
            lstMoves.Name = "lstMoves";
            lstMoves.Size = new Size(300, 334);
            lstMoves.TabIndex = 6;
            lstMoves.SelectedIndexChanged += lstMoves_SelectedIndexChanged;
            // 
            // txtMoveNumber
            // 
            txtMoveNumber.Location = new Point(328, 202);
            txtMoveNumber.Name = "txtMoveNumber";
            txtMoveNumber.Size = new Size(100, 23);
            txtMoveNumber.TabIndex = 7;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(467, 401);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(100, 23);
            progressBar1.TabIndex = 8;
            // 
            // btnNextMove
            // 
            btnNextMove.Location = new Point(396, 231);
            btnNextMove.Name = "btnNextMove";
            btnNextMove.Size = new Size(32, 23);
            btnNextMove.TabIndex = 9;
            btnNextMove.Text = ">";
            btnNextMove.UseVisualStyleBackColor = true;
            btnNextMove.Click += btnNextMove_Click;
            // 
            // btnPrevMove
            // 
            btnPrevMove.Location = new Point(328, 231);
            btnPrevMove.Name = "btnPrevMove";
            btnPrevMove.Size = new Size(32, 23);
            btnPrevMove.TabIndex = 10;
            btnPrevMove.Text = "<";
            btnPrevMove.UseVisualStyleBackColor = true;
            btnPrevMove.Click += btnPrevMove_Click;
            // 
            // chkIsMono
            // 
            chkIsMono.AutoSize = true;
            chkIsMono.Location = new Point(142, 204);
            chkIsMono.Name = "chkIsMono";
            chkIsMono.Size = new Size(58, 19);
            chkIsMono.TabIndex = 11;
            chkIsMono.Text = "mono";
            chkIsMono.UseVisualStyleBackColor = true;
            // 
            // chkIncludeWhite
            // 
            chkIncludeWhite.AutoSize = true;
            chkIncludeWhite.Checked = true;
            chkIncludeWhite.CheckState = CheckState.Checked;
            chkIncludeWhite.Location = new Point(105, 231);
            chkIncludeWhite.Name = "chkIncludeWhite";
            chkIncludeWhite.Size = new Size(99, 19);
            chkIncludeWhite.TabIndex = 12;
            chkIncludeWhite.Text = "Include White";
            chkIncludeWhite.UseVisualStyleBackColor = true;
            // 
            // chkIncludeBlack
            // 
            chkIncludeBlack.AutoSize = true;
            chkIncludeBlack.Checked = true;
            chkIncludeBlack.CheckState = CheckState.Checked;
            chkIncludeBlack.Location = new Point(228, 231);
            chkIncludeBlack.Name = "chkIncludeBlack";
            chkIncludeBlack.Size = new Size(96, 19);
            chkIncludeBlack.TabIndex = 12;
            chkIncludeBlack.Text = "Include Black";
            chkIncludeBlack.UseVisualStyleBackColor = true;
            // 
            // btnPause
            // 
            btnPause.Image = Properties.Resources.control_pause;
            btnPause.Location = new Point(248, 200);
            btnPause.Name = "btnPause";
            btnPause.Size = new Size(33, 23);
            btnPause.TabIndex = 3;
            btnPause.UseVisualStyleBackColor = true;
            btnPause.Click += btnPause_Click;
            // 
            // btnStop
            // 
            btnStop.Image = Properties.Resources.control_stop;
            btnStop.Location = new Point(287, 202);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(33, 23);
            btnStop.TabIndex = 3;
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // imgBoard
            // 
            imgBoard.BackgroundImage = Properties.Resources.board;
            imgBoard.BackgroundImageLayout = ImageLayout.Stretch;
            imgBoard.InitialImage = null;
            imgBoard.Location = new Point(800, 40);
            imgBoard.Name = "imgBoard";
            imgBoard.Size = new Size(400, 400);
            imgBoard.SizeMode = PictureBoxSizeMode.StretchImage;
            imgBoard.TabIndex = 13;
            imgBoard.TabStop = false;
            // 
            // lblMessages
            // 
            lblMessages.AutoSize = true;
            lblMessages.Location = new Point(115, 16);
            lblMessages.Name = "lblMessages";
            lblMessages.Size = new Size(49, 15);
            lblMessages.TabIndex = 14;
            lblMessages.Text = "              ";
            // 
            // chkTwoNote
            // 
            chkTwoNote.AutoSize = true;
            chkTwoNote.Location = new Point(12, 231);
            chkTwoNote.Name = "chkTwoNote";
            chkTwoNote.Size = new Size(76, 19);
            chkTwoNote.TabIndex = 12;
            chkTwoNote.Text = "Two Note";
            chkTwoNote.UseVisualStyleBackColor = true;
            chkTwoNote.CheckedChanged += chkTwoNote_CheckedChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1237, 477);
            Controls.Add(lblMessages);
            Controls.Add(imgBoard);
            Controls.Add(chkIncludeBlack);
            Controls.Add(chkTwoNote);
            Controls.Add(chkIncludeWhite);
            Controls.Add(chkIsMono);
            Controls.Add(btnPrevMove);
            Controls.Add(btnNextMove);
            Controls.Add(progressBar1);
            Controls.Add(txtMoveNumber);
            Controls.Add(lstMoves);
            Controls.Add(lblBoardSnapshot);
            Controls.Add(player);
            Controls.Add(btnStop);
            Controls.Add(btnPause);
            Controls.Add(btnPlay);
            Controls.Add(btnGenerateMIDI);
            Controls.Add(lstGames);
            Controls.Add(btnLoadPGN);
            Name = "MainForm";
            Text = "Chess Sonification";
            ((System.ComponentModel.ISupportInitialize)player).EndInit();
            ((System.ComponentModel.ISupportInitialize)imgBoard).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnLoadPGN;
        private OpenFileDialog openFileDialog1;
        private ListBox lstGames;
        private Button btnGenerateMIDI;
        private Button btnPlay;
        private AxWMPLib.AxWindowsMediaPlayer player;
        private Label lblBoardSnapshot;
        private ListBox lstMoves;
        private TextBox txtMoveNumber;
        private ProgressBar progressBar1;
        private Button btnNextMove;
        private Button btnPrevMove;
        private CheckBox chkIsMono;
        private CheckBox chkIncludeWhite;
        private CheckBox chkIncludeBlack;
        private Button btnPause;
        private Button btnStop;
        private PictureBox imgBoard;
        private Label lblMessages;
        private CheckBox chkTwoNote;
    }
}
