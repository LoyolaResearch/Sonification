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
            ((System.ComponentModel.ISupportInitialize)player).BeginInit();
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
            // lblMessages
            // 
            lblMessages.AutoSize = true;
            lblMessages.Location = new Point(115, 16);
            lblMessages.Name = "lblMessages";
            lblMessages.Size = new Size(49, 15);
            lblMessages.TabIndex = 14;
            lblMessages.Text = "              ";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(555, 276);
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
    }
}
