using System.Drawing;
using System.Windows.Forms;

namespace BrickGame2.Gui
{
    partial class InstructionsForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private Label lblSectionControls;
        private Label lblControlsText;
        private Label lblSectionPowerups;
        private Panel pnlPowerLife;
        private Panel pnlPowerSlow;
        private Panel pnlPowerStrong;
        private Label lblPowerLife;
        private Label lblPowerSlow;
        private Label lblPowerStrong;
        private Label lblGameplayHeader;
        private Label lblGameplayText;
        private Button btnClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSectionControls = new System.Windows.Forms.Label();
            this.lblControlsText = new System.Windows.Forms.Label();
            this.lblSectionPowerups = new System.Windows.Forms.Label();
            this.pnlPowerLife = new System.Windows.Forms.Panel();
            this.pnlPowerSlow = new System.Windows.Forms.Panel();
            this.pnlPowerStrong = new System.Windows.Forms.Panel();
            this.lblPowerLife = new System.Windows.Forms.Label();
            this.lblPowerSlow = new System.Windows.Forms.Label();
            this.lblPowerStrong = new System.Windows.Forms.Label();
            this.lblGameplayHeader = new System.Windows.Forms.Label();
            this.lblGameplayText = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.lblTitle.Size = new System.Drawing.Size(700, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "INSTRUCTIONS";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSectionControls
            // 
            this.lblSectionControls.AutoSize = true;
            this.lblSectionControls.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold);
            this.lblSectionControls.ForeColor = System.Drawing.Color.White;
            this.lblSectionControls.Location = new System.Drawing.Point(24, 70);
            this.lblSectionControls.Name = "lblSectionControls";
            this.lblSectionControls.Size = new System.Drawing.Size(90, 19);
            this.lblSectionControls.TabIndex = 1;
            this.lblSectionControls.Text = "CONTROLS";
            // 
            // lblControlsText
            // 
            this.lblControlsText.Font = new System.Drawing.Font("Consolas", 10.5F);
            this.lblControlsText.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblControlsText.Location = new System.Drawing.Point(40, 95);
            this.lblControlsText.Name = "lblControlsText";
            this.lblControlsText.Size = new System.Drawing.Size(630, 110);
            this.lblControlsText.TabIndex = 2;
            this.lblControlsText.Text =
"LEFT  / A   - move paddle left\r\nRIGHT / D  - move paddle right\r\nSPACE      - laun" +
"ch ball / next level / restart\r\nESC        - exit game\r\nX          - DEBUG: inst" +
"antly break all bricks (cheat)";
            // 
            // lblSectionPowerups
            // 
            this.lblSectionPowerups.AutoSize = true;
            this.lblSectionPowerups.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold);
            this.lblSectionPowerups.ForeColor = System.Drawing.Color.White;
            this.lblSectionPowerups.Location = new System.Drawing.Point(24, 220);
            this.lblSectionPowerups.Name = "lblSectionPowerups";
            this.lblSectionPowerups.Size = new System.Drawing.Size(99, 19);
            this.lblSectionPowerups.TabIndex = 3;
            this.lblSectionPowerups.Text = "POWER-UPS";
            // 
            // pnlPowerLife
            // 
            this.pnlPowerLife.BackColor = System.Drawing.Color.Lime;
            this.pnlPowerLife.Location = new System.Drawing.Point(40, 250);
            this.pnlPowerLife.Name = "pnlPowerLife";
            this.pnlPowerLife.Size = new System.Drawing.Size(26, 26);
            this.pnlPowerLife.TabIndex = 4;
            // 
            // pnlPowerSlow
            // 
            this.pnlPowerSlow.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.pnlPowerSlow.Location = new System.Drawing.Point(40, 285);
            this.pnlPowerSlow.Name = "pnlPowerSlow";
            this.pnlPowerSlow.Size = new System.Drawing.Size(26, 26);
            this.pnlPowerSlow.TabIndex = 5;
            // 
            // pnlPowerStrong
            // 
            this.pnlPowerStrong.BackColor = System.Drawing.Color.Gold;
            this.pnlPowerStrong.Location = new System.Drawing.Point(40, 320);
            this.pnlPowerStrong.Name = "pnlPowerStrong";
            this.pnlPowerStrong.Size = new System.Drawing.Size(26, 26);
            this.pnlPowerStrong.TabIndex = 6;
            // 
            // lblPowerLife
            // 
            this.lblPowerLife.Font = new System.Drawing.Font("Consolas", 10.5F);
            this.lblPowerLife.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblPowerLife.Location = new System.Drawing.Point(80, 250);
            this.lblPowerLife.Name = "lblPowerLife";
            this.lblPowerLife.Size = new System.Drawing.Size(590, 26);
            this.lblPowerLife.TabIndex = 7;
            this.lblPowerLife.Text = "L - Extra Life (green)   →  Gives +1 life.";
            this.lblPowerLife.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPowerSlow
            // 
            this.lblPowerSlow.Font = new System.Drawing.Font("Consolas", 10.5F);
            this.lblPowerSlow.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblPowerSlow.Location = new System.Drawing.Point(80, 285);
            this.lblPowerSlow.Name = "lblPowerSlow";
            this.lblPowerSlow.Size = new System.Drawing.Size(590, 26);
            this.lblPowerSlow.TabIndex = 8;
            this.lblPowerSlow.Text = "S - Slow Motion (blue)  →  Slows the ball for a short time.";
            this.lblPowerSlow.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPowerStrong
            // 
            this.lblPowerStrong.Font = new System.Drawing.Font("Consolas", 10.5F);
            this.lblPowerStrong.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblPowerStrong.Location = new System.Drawing.Point(80, 320);
            this.lblPowerStrong.Name = "lblPowerStrong";
            this.lblPowerStrong.Size = new System.Drawing.Size(590, 26);
            this.lblPowerStrong.TabIndex = 9;
            this.lblPowerStrong.Text = "P - Strong Ball (gold)  →  Ball ignores brick durability (1 hit = break).";
            this.lblPowerStrong.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblGameplayHeader
            // 
            this.lblGameplayHeader.AutoSize = true;
            this.lblGameplayHeader.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold);
            this.lblGameplayHeader.ForeColor = System.Drawing.Color.White;
            this.lblGameplayHeader.Location = new System.Drawing.Point(24, 365);
            this.lblGameplayHeader.Name = "lblGameplayHeader";
            this.lblGameplayHeader.Size = new System.Drawing.Size(90, 19);
            this.lblGameplayHeader.TabIndex = 10;
            this.lblGameplayHeader.Text = "GAMEPLAY";
            // 
            // lblGameplayText
            // 
            this.lblGameplayText.Font = new System.Drawing.Font("Consolas", 10.5F);
            this.lblGameplayText.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblGameplayText.Location = new System.Drawing.Point(40, 390);
            this.lblGameplayText.Name = "lblGameplayText";
            this.lblGameplayText.Size = new System.Drawing.Size(630, 90);
            this.lblGameplayText.TabIndex = 11;
            this.lblGameplayText.Text =
"• Destroy all bricks to finish a level.\r\n• Press SPACE after finishing a level to" +
" continue.\r\n• Don\'t let the ball fall below the paddle.\r\n• Collect power-ups by " +
"catching them with the paddle.\r\n• Difficulty affects starting lives.";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(80, 30, 40);
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Consolas", 10.5F);
            this.btnClose.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnClose.Location = new System.Drawing.Point(280, 495);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(140, 32);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "OK";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // InstructionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(700, 550);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblGameplayText);
            this.Controls.Add(this.lblGameplayHeader);
            this.Controls.Add(this.lblPowerStrong);
            this.Controls.Add(this.lblPowerSlow);
            this.Controls.Add(this.lblPowerLife);
            this.Controls.Add(this.pnlPowerStrong);
            this.Controls.Add(this.pnlPowerSlow);
            this.Controls.Add(this.pnlPowerLife);
            this.Controls.Add(this.lblSectionPowerups);
            this.Controls.Add(this.lblControlsText);
            this.Controls.Add(this.lblSectionControls);
            this.Controls.Add(this.lblTitle);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InstructionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Instructions";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
