using System.Drawing;
using System.Windows.Forms;

namespace BrickGame2.Gui
{
    partial class GameForm
    {
        private System.ComponentModel.IContainer components = null;

        private Panel gamePanel;
        private Button startButton;
        private Button pauseButton;
        private Label scoreLabel;
        private Label messageLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.scoreLabel = new System.Windows.Forms.Label();
            this.messageLabel = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.pauseButton = new System.Windows.Forms.Button();
            this.gamePanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // scoreLabel
            // 
            this.scoreLabel.BackColor = System.Drawing.Color.DimGray;
            this.scoreLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.scoreLabel.ForeColor = System.Drawing.Color.White;
            this.scoreLabel.Location = new System.Drawing.Point(0, 0);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.scoreLabel.Size = new System.Drawing.Size(1100, 30);
            this.scoreLabel.TabIndex = 0;
            this.scoreLabel.Text = "Punkty: 0   Życia: 3   Poziom: 1";
            this.scoreLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // messageLabel
            // 
            this.messageLabel.BackColor = System.Drawing.Color.DimGray;
            this.messageLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.messageLabel.ForeColor = System.Drawing.Color.White;
            this.messageLabel.Location = new System.Drawing.Point(0, 720);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.messageLabel.Size = new System.Drawing.Size(1100, 30);
            this.messageLabel.TabIndex = 1;
            this.messageLabel.Text = "SPACE - start | ESC - wyjście";
            this.messageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.Color.FromArgb(40, 160, 90);
            this.startButton.FlatAppearance.BorderSize = 0;
            this.startButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startButton.Font = new System.Drawing.Font("Consolas", 10F);
            this.startButton.ForeColor = System.Drawing.Color.White;
            this.startButton.Location = new System.Drawing.Point(12, 36);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(90, 28);
            this.startButton.TabIndex = 2;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // pauseButton
            // 
            this.pauseButton.BackColor = System.Drawing.Color.FromArgb(50, 60, 110);
            this.pauseButton.FlatAppearance.BorderSize = 0;
            this.pauseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pauseButton.Font = new System.Drawing.Font("Consolas", 10F);
            this.pauseButton.ForeColor = System.Drawing.Color.White;
            this.pauseButton.Location = new System.Drawing.Point(108, 36);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(90, 28);
            this.pauseButton.TabIndex = 3;
            this.pauseButton.Text = "Pause";
            this.pauseButton.UseVisualStyleBackColor = false;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // gamePanel
            // 
            this.gamePanel.BackColor = System.Drawing.Color.Black;
            this.gamePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gamePanel.Location = new System.Drawing.Point(0, 30);
            this.gamePanel.Name = "gamePanel";
            this.gamePanel.Size = new System.Drawing.Size(1100, 690);
            this.gamePanel.TabIndex = 4;
            this.gamePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.gamePanel_Paint);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1100, 750);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.gamePanel);
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.scoreLabel);
            this.KeyPreview = true;
            this.Name = "GameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BrickGame - GUI";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
