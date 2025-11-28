using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using BrickGame.Models;
using Timer = System.Windows.Forms.Timer;

namespace BrickGame2.Gui
{
    public partial class GameForm : Form
    {
        private GameModel _model;
        private Timer _timer;
        private readonly int _targetFps = 30;
        private bool _isPaused = false;
        private bool _scoreSaved = false;

        // ====== GAME OVER ANIMACJA ======
        private bool _showGameOverAnimation = false;
        private float _gameOverAlpha = 0f;
        private float _gameOverScale = 0.3f;
        private Timer _gameOverTimer;

        public GameForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode)
                return;

            _model = new GameModel();

            _timer = new Timer();
            _timer.Interval = 1000 / _targetFps;
            _timer.Tick += Timer_Tick;

            this.KeyPreview = true;
            gamePanel.TabStop = true;
            gamePanel.Focus();
            gamePanel.KeyDown += GameForm_KeyDown;

            _scoreSaved = false;
            pauseButton.Visible = true;

            UpdateHudLabels();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (_model == null || _isPaused) return;

            bool wasGameOver = _model.GameOver;

            _model.Update();
            UpdateHudLabels();
            gamePanel.Invalidate();

            if (_model.GameOver && !wasGameOver && !_scoreSaved)
            {
                _timer.Stop();           // zatrzymujemy logikę gry
                _isPaused = false;       // pauza NIE aktywna
                pauseButton.Visible = false;  // ukrywamy przycisk
                StartGameOverAnimation();
                HandleGameOverAndRanking();
            }
        }

        private void UpdateHudLabels()
        {
            scoreLabel.Text = $"Punkty: {_model.Score}   Życia: {_model.Lives}   Poziom: {_model.Level}";
            messageLabel.Text = _model.Message;
        }

        // ======== STEROWANIE ========
        private void GameForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (_model == null) return;

            if (e.KeyCode == Keys.P)
            {
                pauseButton_Click(sender!, EventArgs.Empty);
                return;
            }

            // cheat
            if (e.KeyCode == Keys.X)
            {
                _model.Bricks.Clear();
                _model.LevelWon = true;
                messageLabel.Text = "MAGIC KEY ACTIVATED! Press SPACE to continue!";
                gamePanel.Invalidate();
                return;
            }

            if (_isPaused)
                return;

            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                _model.MovePaddleLeft();
                gamePanel.Invalidate();
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                _model.MovePaddleRight();
                gamePanel.Invalidate();
            }
            else if (e.KeyCode == Keys.Space)
            {
                if (_model.GameOver)
                {
                    _model.Reset();
                    _scoreSaved = false;
                    _showGameOverAnimation = false;
                    pauseButton.Visible = true;
                    pauseButton.Text = "Pause";
                }
                else if (_model.LevelWon)
                {
                    _model.NextLevel();
                }
                else
                {
                    _model.LaunchBall();
                }

                UpdateHudLabels();
                gamePanel.Invalidate();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (_model != null && !_isPaused)
            {
                if (keyData == Keys.Left)
                {
                    _model.MovePaddleLeft();
                    gamePanel.Invalidate();
                    return true;
                }
                if (keyData == Keys.Right)
                {
                    _model.MovePaddleRight();
                    gamePanel.Invalidate();
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        // ===== START =====
        private void startButton_Click(object sender, EventArgs e)
        {
            if (!_timer.Enabled)
            {
                _isPaused = false;
                _timer.Start();
                pauseButton.Text = "Pause";
                messageLabel.Text = "A/D lub Strzałki – ruch | SPACE – piłka | P – pauza | X – cheat";
            }

            gamePanel.Focus();
        }

        // ===== PAUZA =====
        private void pauseButton_Click(object sender, EventArgs e)
        {
            if (!_timer.Enabled) return;
            if (_model.GameOver) return;

            if (_isPaused)
            {
                _isPaused = false;
                _timer.Start();
                pauseButton.Text = "Pause";
                messageLabel.Text = "Game Resumed";
            }
            else
            {
                _isPaused = true;
                _timer.Stop();
                pauseButton.Text = "Resume";
                messageLabel.Text = "GAME PAUSED - press P or Resume";
            }

            gamePanel.Invalidate();
        }

        // ======= RANKING =======
        private void HandleGameOverAndRanking()
        {
            if (_scoreSaved) return;

            string name = ShowNameDialog(_model.Score);

            if (!string.IsNullOrWhiteSpace(name))
            {
                SaveScoreToRanking(name.Trim(), _model.Score);
                SaveHighScore(_model.Score);
                messageLabel.Text = $"Game Over – wynik zapisany jako {name}";
            }
            else
            {
                messageLabel.Text = "Game Over – wynik nie zapisany.";
            }

            _scoreSaved = true;
        }

        private string ShowNameDialog(int score)
        {
            using var dlg = new Form();
            dlg.Text = "Game Over – wpisz nick";
            dlg.StartPosition = FormStartPosition.CenterParent;
            dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
            dlg.ClientSize = new Size(360, 150);

            var lbl = new Label
            {
                Text = $"Twój wynik: {score}\r\nPodaj nick:",
                Left = 10,
                Top = 10,
                Width = 340
            };

            var txt = new TextBox
            {
                Left = 10,
                Top = 55,
                Width = 340
            };

            var ok = new Button
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Left = 190,
                Top = 100,
                Width = 70
            };

            var cancel = new Button
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                Left = 270,
                Top = 100,
                Width = 70
            };

            dlg.Controls.Add(lbl);
            dlg.Controls.Add(txt);
            dlg.Controls.Add(ok);
            dlg.Controls.Add(cancel);

            dlg.AcceptButton = ok;
            dlg.CancelButton = cancel;

            return dlg.ShowDialog(this) == DialogResult.OK ? txt.Text : "";
        }

        private void SaveScoreToRanking(string name, int score)
        {
            try
            {
                List<PlayerScore> ranking;

                if (File.Exists("scores.json"))
                {
                    ranking = JsonSerializer.Deserialize<List<PlayerScore>>(File.ReadAllText("scores.json"))
                              ?? new List<PlayerScore>();
                }
                else ranking = new List<PlayerScore>();

                ranking.Add(new PlayerScore { Name = name, Score = score });

                File.WriteAllText("scores.json",
                    JsonSerializer.Serialize(ranking, new JsonSerializerOptions { WriteIndented = true }));
            }
            catch { }
        }

        private void SaveHighScore(int score)
        {
            try
            {
                int best = 0;

                if (File.Exists("highscore.txt"))
                    int.TryParse(File.ReadAllText("highscore.txt"), out best);

                if (score > best)
                    File.WriteAllText("highscore.txt", score.ToString());
            }
            catch { }
        }

        // ====== GAME OVER ANIMACJA ======
        private void StartGameOverAnimation()
        {
            _showGameOverAnimation = true;
            _gameOverAlpha = 0f;
            _gameOverScale = 0.3f;

            _gameOverTimer = new Timer();
            _gameOverTimer.Interval = 16;
            _gameOverTimer.Tick += (s, e) =>
            {
                _gameOverAlpha += 0.03f;
                _gameOverScale += 0.02f;

                if (_gameOverAlpha > 1f) _gameOverAlpha = 1f;
                if (_gameOverScale > 1f) _gameOverScale = 1f;

                gamePanel.Invalidate();

                if (_gameOverScale >= 1f)
                    _gameOverTimer.Stop();
            };

            _gameOverTimer.Start();
        }

        // ====== RYSOWANIE ======
        private void gamePanel_Paint(object? sender, PaintEventArgs e)
        {
            if (_model == null) return;

            var g = e.Graphics;
            g.Clear(Color.Black);

            int cellsX = _model.Width;
            int cellsY = _model.Height;

            float cellW = (float)gamePanel.Width / cellsX;
            float cellH = (float)gamePanel.Height / cellsY;
            float cellSize = Math.Min(cellW, cellH);

            float offX = (gamePanel.Width - cellsX * cellSize) / 2f;
            float offY = (gamePanel.Height - cellsY * cellSize) / 2f;

            // CEGLI
            foreach (var brick in _model.Bricks)
            {
                Brush b = brick.Value switch
                {
                    4 => Brushes.Red,
                    3 => Brushes.OrangeRed,
                    2 => Brushes.Gold,
                    1 => Brushes.LightGray,
                    _ => Brushes.DarkGray
                };

                g.FillRectangle(b,
                    offX + brick.Key.Item1 * cellSize,
                    offY + brick.Key.Item2 * cellSize,
                    cellSize - 1,
                    cellSize - 1);
            }

            // POWERUPY
            foreach (var p in _model.PowerUps)
            {
                Brush b = p.Type switch
                {
                    'L' => Brushes.Lime,
                    'S' => Brushes.DeepSkyBlue,
                    'P' => Brushes.Gold,
                    _ => Brushes.White
                };

                g.FillEllipse(
                    b,
                    offX + p.X * cellSize + cellSize * 0.15f,
                    offY + p.Y * cellSize + cellSize * 0.15f,
                    cellSize * 0.7f, cellSize * 0.7f);
            }

            // PALETKA
            g.FillRectangle(
                Brushes.Cyan,
                offX + _model.PaddleX * cellSize,
                offY + _model.PaddleY * cellSize,
                _model.PaddleW * cellSize,
                cellSize);

            // PIŁKA
            g.FillEllipse(
                Brushes.White,
                offX + _model.BallX * cellSize,
                offY + _model.BallY * cellSize,
                cellSize, cellSize);

            // RAMKA
            using var pen = new Pen(Color.DimGray);
            g.DrawRectangle(pen, offX, offY, cellsX * cellSize, cellsY * cellSize);

            // PAUSED OVERLAY
            if (_isPaused)
            {
                using var bg = new SolidBrush(Color.FromArgb(150, 0, 0, 0));
                g.FillRectangle(bg, gamePanel.ClientRectangle);

                using var font = new Font("Consolas", 28, FontStyle.Bold);
                using var brush = new SolidBrush(Color.DeepSkyBlue);
                var txt = "PAUSED";
                var size = g.MeasureString(txt, font);

                g.DrawString(txt, font, brush,
                    (gamePanel.Width - size.Width) / 2,
                    (gamePanel.Height - size.Height) / 2);
            }

            // GAME OVER ANIMACJA
            if (_showGameOverAnimation)
            {
                string txt = "GAME OVER";

                using var font = new Font("Consolas", 60 * _gameOverScale, FontStyle.Bold);
                var size = g.MeasureString(txt, font);

                using var brush = new SolidBrush(Color.FromArgb(
                    (int)(_gameOverAlpha * 255), 255, 40, 40));

                g.DrawString(txt, font, brush,
                    (gamePanel.Width - size.Width) / 2,
                    (gamePanel.Height - size.Height) / 2);
            }
        }
    }
}
