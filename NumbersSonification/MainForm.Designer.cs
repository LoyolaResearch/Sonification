namespace NumbersSonification
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
            btnLoadTxtFile = new Button();
            openFileDialog1 = new OpenFileDialog();
            btnGenerateMIDI = new Button();
            btnPlay = new Button();
            player = new AxWMPLib.AxWindowsMediaPlayer();
            chkIsMono = new CheckBox();
            btnPause = new Button();
            btnStop = new Button();
            lblMessages = new Label();
            lblFeedback = new Label();
            radioSimple = new RadioButton();
            grpAlgorithm = new GroupBox();
            chkUseDuration = new CheckBox();
            lblNumTracks = new Label();
            tbNumTracks = new TrackBar();
            radioOther = new RadioButton();
            chkNotePerLine = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)player).BeginInit();
            grpAlgorithm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tbNumTracks).BeginInit();
            SuspendLayout();
            // 
            // btnLoadTxtFile
            // 
            btnLoadTxtFile.Location = new Point(12, 12);
            btnLoadTxtFile.Name = "btnLoadTxtFile";
            btnLoadTxtFile.Size = new Size(97, 23);
            btnLoadTxtFile.TabIndex = 0;
            btnLoadTxtFile.Text = "Load File...";
            btnLoadTxtFile.UseVisualStyleBackColor = true;
            btnLoadTxtFile.Click += btnLoadTxtFile_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnGenerateMIDI
            // 
            btnGenerateMIDI.Location = new Point(12, 244);
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
            btnPlay.Location = new Point(209, 244);
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
            // chkIsMono
            // 
            chkIsMono.AutoSize = true;
            chkIsMono.Location = new Point(142, 247);
            chkIsMono.Name = "chkIsMono";
            chkIsMono.Size = new Size(58, 19);
            chkIsMono.TabIndex = 11;
            chkIsMono.Text = "mono";
            chkIsMono.UseVisualStyleBackColor = true;
            // 
            // btnPause
            // 
            btnPause.Image = Properties.Resources.control_pause;
            btnPause.Location = new Point(248, 243);
            btnPause.Name = "btnPause";
            btnPause.Size = new Size(33, 23);
            btnPause.TabIndex = 3;
            btnPause.UseVisualStyleBackColor = true;
            btnPause.Click += btnPause_Click;
            // 
            // btnStop
            // 
            btnStop.Image = Properties.Resources.control_stop;
            btnStop.Location = new Point(287, 245);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(33, 23);
            btnStop.TabIndex = 3;
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
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
            // lblFeedback
            // 
            lblFeedback.Location = new Point(443, 28);
            lblFeedback.Name = "lblFeedback";
            lblFeedback.Size = new Size(225, 326);
            lblFeedback.TabIndex = 15;
            // 
            // radioSimple
            // 
            radioSimple.AutoSize = true;
            radioSimple.Checked = true;
            radioSimple.Location = new Point(6, 22);
            radioSimple.Name = "radioSimple";
            radioSimple.Size = new Size(61, 19);
            radioSimple.TabIndex = 16;
            radioSimple.TabStop = true;
            radioSimple.Text = "Simple";
            radioSimple.UseVisualStyleBackColor = true;
            // 
            // grpAlgorithm
            // 
            grpAlgorithm.Controls.Add(chkNotePerLine);
            grpAlgorithm.Controls.Add(chkUseDuration);
            grpAlgorithm.Controls.Add(lblNumTracks);
            grpAlgorithm.Controls.Add(tbNumTracks);
            grpAlgorithm.Controls.Add(radioOther);
            grpAlgorithm.Controls.Add(radioSimple);
            grpAlgorithm.Location = new Point(29, 128);
            grpAlgorithm.Name = "grpAlgorithm";
            grpAlgorithm.Size = new Size(398, 100);
            grpAlgorithm.TabIndex = 17;
            grpAlgorithm.TabStop = false;
            grpAlgorithm.Text = "Algorithm";
            // 
            // chkUseDuration
            // 
            chkUseDuration.AutoSize = true;
            chkUseDuration.Location = new Point(27, 74);
            chkUseDuration.Name = "chkUseDuration";
            chkUseDuration.Size = new Size(94, 19);
            chkUseDuration.TabIndex = 19;
            chkUseDuration.Text = "Use Duration";
            chkUseDuration.UseVisualStyleBackColor = true;
            // 
            // lblNumTracks
            // 
            lblNumTracks.AutoSize = true;
            lblNumTracks.Location = new Point(113, 19);
            lblNumTracks.Name = "lblNumTracks";
            lblNumTracks.Size = new Size(120, 15);
            lblNumTracks.TabIndex = 18;
            lblNumTracks.Text = "Number of Tracks (1):";
            // 
            // tbNumTracks
            // 
            tbNumTracks.Location = new Point(113, 37);
            tbNumTracks.Minimum = 1;
            tbNumTracks.Name = "tbNumTracks";
            tbNumTracks.Size = new Size(275, 45);
            tbNumTracks.TabIndex = 17;
            tbNumTracks.Value = 1;
            tbNumTracks.Scroll += tbNumTracks_Scroll;
            // 
            // radioOther
            // 
            radioOther.AutoSize = true;
            radioOther.Location = new Point(6, 47);
            radioOther.Name = "radioOther";
            radioOther.Size = new Size(75, 19);
            radioOther.TabIndex = 16;
            radioOther.Text = "Two Note";
            radioOther.UseVisualStyleBackColor = true;
            // 
            // chkNotePerLine
            // 
            chkNotePerLine.AutoSize = true;
            chkNotePerLine.Location = new Point(158, 74);
            chkNotePerLine.Name = "chkNotePerLine";
            chkNotePerLine.Size = new Size(122, 19);
            chkNotePerLine.TabIndex = 20;
            chkNotePerLine.Text = "One Note per Line";
            chkNotePerLine.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(680, 399);
            Controls.Add(grpAlgorithm);
            Controls.Add(lblFeedback);
            Controls.Add(lblMessages);
            Controls.Add(chkIsMono);
            Controls.Add(player);
            Controls.Add(btnStop);
            Controls.Add(btnPause);
            Controls.Add(btnPlay);
            Controls.Add(btnGenerateMIDI);
            Controls.Add(btnLoadTxtFile);
            Name = "MainForm";
            Text = "Chess Sonification";
            ((System.ComponentModel.ISupportInitialize)player).EndInit();
            grpAlgorithm.ResumeLayout(false);
            grpAlgorithm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tbNumTracks).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnLoadTxtFile;
        private OpenFileDialog openFileDialog1;
        private Button btnGenerateMIDI;
        private Button btnPlay;
        private AxWMPLib.AxWindowsMediaPlayer player;
        private CheckBox chkIsMono;
        private Button btnPause;
        private Button btnStop;
        private Label lblMessages;
        private Label lblFeedback;
        private RadioButton radioSimple;
        private GroupBox grpAlgorithm;
        private RadioButton radioOther;
        private TrackBar tbNumTracks;
        private Label lblNumTracks;
        private CheckBox chkUseDuration;
        private CheckBox chkNotePerLine;
    }
}
