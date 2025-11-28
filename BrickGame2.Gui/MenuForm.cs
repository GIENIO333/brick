using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using BrickGame.Models;   // żeby widzieć PlayerScore

namespace BrickGame2.Gui
{
    public partial class MenuForm : Form
    {
        // animacja pikselowego logo
        private float logoAlpha = 0f;
        private float shakeOffset = 0f;
        private bool shakeDirection = true;

        public MenuForm()
        {
            InitializeComponent();
        }

        // ANIMACJA LOGO – CRT pixel shake + fade-in
        private void logoAnimationTimer_Tick(object sender, EventArgs e)
        {
            // fade-in
            logoAlpha += 0.02f;
            if (logoAlpha > 1f)
                logoAlpha = 1f;

            // CRT shake
            if (shakeDirection)
            {
                shakeOffset += 0.4f;
                if (shakeOffset > 2f) shakeDirection = false;
            }
            else
            {
                shakeOffset -= 0.4f;
                if (shakeOffset < -2f) shakeDirection = true;
            }

            logoBox.Invalidate();
        }


        // RYSOWANIE LOGO PIXEL ART
        private void logoBox_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.Black);

            string[] logo =
            {
                "██████╗ ██████╗ ██╗ ██████╗██╗  ██╗",
                "██╔══██╗██╔══██╗██║██╔════╝██║ ██╔╝",
                "██████╔╝██████╔╝██║██║     █████╔╝ ",
                "██╔══██╗██╔══██╗██║██║     ██╔═██╗ ",
                "██████╔╝██║  ██║██║╚██████╗██║  ██╗",
                "╚═════╝ ╚═╝  ╚═╝╚═╝ ╚═════╝╚═╝  ╚═╝ ",
                "      B R I C K   G A M E  2 0 2 5"
            };

            using var font = new Font("Consolas", 32, FontStyle.Bold);
            int alpha = (int)(logoAlpha * 255);
            if (alpha < 0) alpha = 0;
            if (alpha > 255) alpha = 255;

            using var brush = new SolidBrush(Color.FromArgb(alpha, 255, 60, 60));


            float y = 20 + shakeOffset;

            foreach (string line in logo)
            {
                var size = g.MeasureString(line, font);
                float x = (logoBox.Width - size.Width) / 2;
                g.DrawString(line, font, brush, x, y);
                y += size.Height - 5;
            }
        }

        // START
        private void btnStart_Click(object sender, EventArgs e)
        {
            this.Hide();
            var game = new GameForm();
            game.FormClosed += (s, _) => this.Show();
            game.Show();
        }

        // INSTRUKCJE
        private void btnInstructions_Click(object sender, EventArgs e)
        {
            using (var dlg = new InstructionsForm())
                dlg.ShowDialog(this);
        }

        // RANKING – czyta scores.json i pokazuje w MessageBox
        private void btnRanking_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists("scores.json"))
                {
                    MessageBox.Show(this,
                        "Brak zapisanych wyników.",
                        "Ranking",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                var json = File.ReadAllText("scores.json");
                var scores = JsonSerializer.Deserialize<List<PlayerScore>>(json) ?? new List<PlayerScore>();

                if (scores.Count == 0)
                {
                    MessageBox.Show(this,
                        "Brak zapisanych wyników.",
                        "Ranking",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                // sortowanie malejąco po wyniku
                scores.Sort((a, b) => b.Score.CompareTo(a.Score));

                int max = Math.Min(10, scores.Count);
                var text = "TOP SCORES:\r\n\r\n";
                for (int i = 0; i < max; i++)
                {
                    text += $"{i + 1}. {scores[i].Name} - {scores[i].Score}\r\n";
                }

                MessageBox.Show(this,
                    text,
                    "Ranking",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    "Błąd podczas odczytu rankingu:\r\n" + ex.Message,
                    "Ranking",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // WYJŚCIE
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
