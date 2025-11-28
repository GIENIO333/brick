using System.Drawing;
using System.Windows.Forms;

namespace BrickGame2.Gui
{
    partial class MenuForm
    {
        private System.ComponentModel.IContainer components = null;

        private PictureBox logoBox;
        private System.Windows.Forms.Timer logoAnimationTimer;  // UŻYJEMY KONKRETNIE Forms.Timer
        private Button btnStart;
        private Button btnInstructions;
        private Button btnRanking;
        private Button btnExit;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.logoBox = new System.Windows.Forms.PictureBox();
            this.logoAnimationTimer = new System.Windows.Forms.Timer(this.components);
            this.btnStart = new System.Windows.Forms.Button();
            this.btnInstructions = new System.Windows.Forms.Button();
            this.btnRanking = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).BeginInit();
            this.SuspendLayout();
            // 
            // logoBox
            // 
            this.logoBox.BackColor = System.Drawing.Color.Black;
            this.logoBox.Location = new System.Drawing.Point(40, 20);
            this.logoBox.Name = "logoBox";
            this.logoBox.Size = new System.Drawing.Size(1000, 250);
            this.logoBox.TabIndex = 0;
            this.logoBox.TabStop = false;
            this.logoBox.Paint += new System.Windows.Forms.PaintEventHandler(this.logoBox_Paint);
            // 
            // logoAnimationTimer
            // 
            this.logoAnimationTimer.Enabled = true;
            this.logoAnimationTimer.Interval = 16;
            this.logoAnimationTimer.Tick += new System.EventHandler(this.logoAnimationTimer_Tick);
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(40, 160, 90);
            this.btnStart.FlatAppearance.BorderSize = 0;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("Consolas", 14F);
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.Location = new System.Drawing.Point(420, 300);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(250, 50);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "START GAME";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnInstructions
            // 
            this.btnInstructions.BackColor = System.Drawing.Color.FromArgb(60, 60, 90);
            this.btnInstructions.FlatAppearance.BorderSize = 0;
            this.btnInstructions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInstructions.Font = new System.Drawing.Font("Consolas", 14F);
            this.btnInstructions.ForeColor = System.Drawing.Color.White;
            this.btnInstructions.Location = new System.Drawing.Point(420, 360);
            this.btnInstructions.Name = "btnInstructions";
            this.btnInstructions.Size = new System.Drawing.Size(250, 50);
            this.btnInstructions.TabIndex = 2;
            this.btnInstructions.Text = "INSTRUCTIONS";
            this.btnInstructions.UseVisualStyleBackColor = false;
            this.btnInstructions.Click += new System.EventHandler(this.btnInstructions_Click);
            // 
            // btnRanking
            // 
            this.btnRanking.BackColor = System.Drawing.Color.FromArgb(90, 60, 60);
            this.btnRanking.FlatAppearance.BorderSize = 0;
            this.btnRanking.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRanking.Font = new System.Drawing.Font("Consolas", 14F);
            this.btnRanking.ForeColor = System.Drawing.Color.White;
            this.btnRanking.Location = new System.Drawing.Point(420, 420);
            this.btnRanking.Name = "btnRanking";
            this.btnRanking.Size = new System.Drawing.Size(250, 50);
            this.btnRanking.TabIndex = 3;
            this.btnRanking.Text = "RANKING";
            this.btnRanking.UseVisualStyleBackColor = false;
            this.btnRanking.Click += new System.EventHandler(this.btnRanking_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(120, 40, 40);
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Consolas", 14F);
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(420, 480);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(250, 50);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "EXIT";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1080, 600);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnRanking);
            this.Controls.Add(this.btnInstructions);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.logoBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BrickGame - Menu";
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion
    }
}
