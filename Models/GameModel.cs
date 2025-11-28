
using System.Collections.Generic;
using System.Linq;
using System;
using System.Runtime.Versioning;
[assembly: SupportedOSPlatform("windows")]
namespace BrickGame.Models;
public class GameModel
{
    public int Width { get; set; }
    public int Height { get; set; }
    public int PaddleX { get; set; }
    public int PaddleY { get; set; }
    public int PaddleW { get; set; }
    public int BallX { get; set; }
    public int BallY { get; set; }
    public int BallDX { get; set; }
    public int BallDY { get; set; }
    public int Lives { get; set; }
    public int Score { get; set; }
    public int Level { get; set; }
    public bool BallAttached { get; set; }
    public double Speed { get; set; }
    public bool GameOver { get; set; } = false;
    public bool LevelWon { get; set; } = false;
    public string Difficulty { get; set; } = "Medium";
    public bool ViewNeedsRefresh { get; set; } = false;
    public string Message { get; set; } = "";
    public bool RequestingPlayerName { get; set; } = false;
    
    public int MarginLeft { get; set; } = 5;
    public int MarginTop { get; set; } = 2;

    public Dictionary<(int, int), int> Bricks { get; set; } = new Dictionary<(int, int), int>();
    public List<PowerUp> PowerUps { get; set; } = new List<PowerUp>();

    private double stuckTimer = 0.0;
    private int lastBallDX = 0;
    private int lastBallDY = 0;
    private Random rnd = new Random();

    public event Action<string> OnPlayerNameRequested = delegate { };
    public event Action<int> OnHighScoreSaved = delegate { };
    public event Action<PlayerScore> OnScoreSaved = delegate { };

    public GameModel(int w = 50, int h = 30)
    {
        Width = w;
        Height = h;
        PaddleW = 9;
        Reset();
    }

    public void Reset()
    {
        Level = 1;
        Score = 0;
        ApplyDifficultySettings(); 
        InitBoard();
        Message = "Press SPACE to start | ESC: Menu";
        RequestingPlayerName = false;
    }

    public void ApplyDifficultySettings()
    {
        if (Difficulty == "Easy")
        {
            Lives = 5;
            Speed = 0.12 - (Level - 1) * 0.003;
        }
        else if (Difficulty == "Hard")
        {
            Lives = 2;
            Speed = 0.06 - (Level - 1) * 0.003;
        }
        else
        {
            Lives = 3;
            Speed = 0.09 - (Level - 1) * 0.003;
        }

        if (Speed < 0.02) Speed = 0.02;
    }

    public void InitBoard()
    {
        Bricks.Clear();
        PowerUps.Clear();
        
        //rózne levele
        CreateLevelLayout(Level);
        
        PaddleX = Width / 2 - PaddleW / 2;
        PaddleY = Height - 2;
        BallX = PaddleX + PaddleW / 2;
        BallY = PaddleY - 1;
        BallDX = 1;
        BallDY = -1;
        BallAttached = true;
        GameOver = false;
        LevelWon = false;
        RequestingPlayerName = false;
    }

    private void CreateLevelLayout(int level)
    {
        switch (level)
        {
            case 1: // Poziom 1 prostokąt
                CreateRectangle(3, 2, 1);
                break;
            case 2: // 2 
                CreateRectangle(4, 2, 2);
                break;
            case 3: // 3  piramida
                CreatePyramid(2);
                break;
            case 4: // 4  romb
                CreateDiamond(2);
                break;
            case 5: // 5  krzyż
                CreateCross(2);
                break;
            case 6: // 6 schodki
                CreateStairs(2);
                break;
            case 7: // 7- ramka
                CreateFrame(2);
                break;
            case 8: //8 dwa prostokąty
                CreateDoubleRectangle(2);
                break;
            default: //9+  losowo
                CreateRandomLayout(level);
                break;
        }
    }

    private void CreateRectangle(int rows, int startY, int baseDurability)
    {
        for (int y = startY; y < startY + rows; y++)
        {
            for (int x = 4; x < Width - 4; x++)
            {
                int durability = baseDurability + (y - startY) % 3;
                Bricks[(x, y)] = durability;
            }
        }
    }

    private void CreatePyramid(int baseDurability)
    {
        int center = Width / 2;
        int rows = 5;
        int startY = 2;

        for (int row = 0; row < rows; row++)
        {
            int bricksInRow = row + 3;
            int startX = center - bricksInRow / 2;

            for (int i = 0; i < bricksInRow; i++)
            {
                int x = startX + i;
                int y = startY + row;
                int durability = baseDurability + (row % 3);
                Bricks[(x, y)] = durability;
            }
        }
    }

    private void CreateDiamond(int baseDurability)
    {
        int center = Width / 2;
        int size = 5;
        int startY = 3;

        for (int row = 0; row < size * 2 - 1; row++)
        {
            int bricksInRow = row < size ? row + 1 : size * 2 - 1 - row;
            int startX = center - bricksInRow / 2;

            for (int i = 0; i < bricksInRow; i++)
            {
                int x = startX + i;
                int y = startY + row;
                int durability = baseDurability + (row % 3);
                if (x >= 4 && x < Width - 4 && y < Height - 5)
                {
                    Bricks[(x, y)] = durability;
                }
            }
        }
    }

    private void CreateCross(int baseDurability)
    {
        int centerX = Width / 2;
        int centerY = 4;
        int size = 4;

        for (int x = centerX - size; x <= centerX + size; x++)
        {
            if (x >= 4 && x < Width - 4)
            {
                Bricks[(x, centerY)] = baseDurability + 1;
            }
        }
        for (int y = centerY - size; y <= centerY + size; y++)
        {
            if (y >= 2 && y < Height - 5)
            {
                Bricks[(centerX, y)] = baseDurability + 1;
            }
        }
    }

    private void CreateStairs(int baseDurability)
    {
        int startY = 2;
        int steps = 6;

        for (int step = 0; step < steps; step++)
        {
            int startX = 4 + step * 3;
            int endX = Width - 4 - step;

            for (int x = startX; x < endX; x++)
            {
                int y = startY + step;
                int durability = baseDurability + (step % 3);
                if (x >= 4 && x < Width - 4 && y < Height - 5)
                {
                    Bricks[(x, y)] = durability;
                }
            }
        }
    }

    private void CreateFrame(int baseDurability)
    {
        int startY = 2;
        int height = 5;
        int startX = 4;
        int endX = Width - 4;

        // Górna i dolna ramka
        for (int x = startX; x < endX; x++)
        {
            Bricks[(x, startY)] = baseDurability + 2; // Górna
            Bricks[(x, startY + height - 1)] = baseDurability + 2; // Dolna
        }

        // Boczne ramki
        for (int y = startY + 1; y < startY + height - 1; y++)
        {
            Bricks[(startX, y)] = baseDurability + 1; // Lewa
            Bricks[(endX - 1, y)] = baseDurability + 1; // Prawa
        }
    }

    private void CreateDoubleRectangle(int baseDurability)
    {
        // górny prostokąt
        for (int y = 2; y < 4; y++)
        {
            for (int x = 6; x < Width - 6; x++)
            {
                Bricks[(x, y)] = baseDurability + (y % 2);
            }
        }

        // Dolny prostokąt
        for (int y = 5; y < 7; y++)
        {
            for (int x = 8; x < Width - 8; x++)
            {
                Bricks[(x, y)] = baseDurability + (y % 2) + 1;
            }
        }
    }

    private void CreateRandomLayout(int level)
    {
        int rows = Math.Min(6 + level / 2, 10);
        int startY = 2;

        for (int y = startY; y < startY + rows; y++)
        {
            // Losowe przerwy między klockami
            for (int x = 4; x < Width - 4; x++)
            {
                if (rnd.Next(100) < 70) // 70% szans na klocek
                {
                    int durability = Math.Min(level / 2 + 1 + (y - startY) % 3, 5);
                    Bricks[(x, y)] = durability;
                }
            }
        }
    }

    public void MovePaddleLeft()
    {
        if (PaddleX > 1)
        {
            PaddleX -= 3;
            if (BallAttached) BallX -= 3;
        }
    }

    public void MovePaddleRight()
    {
        if (PaddleX + PaddleW < Width - 1)
        {
            PaddleX += 3;
            if (BallAttached) BallX += 3;
        }
    }

    public void LaunchBall()
    {
        if (BallAttached) BallAttached = false;
    }

    private void SpawnPowerUp(int x, int y)
    {
        char[] types = { 'L', 'S', 'P' };
        PowerUps.Add(new PowerUp { X = x, Y = y, Type = types[rnd.Next(types.Length)] });
    }

    private void CollectPowerUp(PowerUp p)
    {
        if (p.Type == 'L') PaddleW = Math.Min(PaddleW + 3, 15);
        else if (p.Type == 'S') Speed = Math.Max(Speed - 0.01, 0.015);
        else if (p.Type == 'P') Score += 100;
        Message = "Power-up! | ESC: Menu";
    }

    public void Update()
    {
        if (GameOver || LevelWon || BallAttached || RequestingPlayerName) return;

        int nextX = BallX + BallDX;
        int nextY = BallY + BallDY;

        if (BallDX == lastBallDX && BallDY == lastBallDY)
        {
            stuckTimer += Speed;
            if (stuckTimer > 10)
            {
                BallDX = (BallDX == 0) ? 1 : -BallDX;
                BallDY = (BallDY == 0) ? -1 : BallDY;
                stuckTimer = 0;
            }
        }
        else
        {
            stuckTimer = 0;
            lastBallDX = BallDX;
            lastBallDY = BallDY;
        }

        if (nextX <= 1 || nextX >= Width - 2)
        {
            BallDX *= -1;
            if (BallDX == 0) BallDX = (rnd.Next(2) == 0 ? -1 : 1);
            ThreadPool.QueueUserWorkItem(_ => { try { Console.Beep(800, 10); } catch { } });
        }
        if (nextY <= 0)
        {
            BallDY *= -1;
            BallY = 1;
            if (BallDX == 0) BallDX = (rnd.Next(2) == 0 ? -1 : 1);
            ThreadPool.QueueUserWorkItem(_ => { try { Console.Beep(900, 20); } catch { } });
        }

        if (nextY == PaddleY && nextX >= PaddleX && nextX < PaddleX + PaddleW)
        {
            BallDY = -1;
            double relativeHit = (double)(nextX - PaddleX) / (PaddleW - 1);
            double angle = (relativeHit - 0.5) * 1.2;
            BallDX = angle > 0 ? 1 : -1;

            if (Math.Abs(angle) < 0.2)
                BallDX = (rnd.Next(2) == 0) ? -1 : 1;

            ThreadPool.QueueUserWorkItem(_ => { try { Console.Beep(600, 30); } catch { } });
        }

        bool collision = false;
        (int, int) hitBrick = (-1, -1);
        
        foreach (var pos in new (int x, int y)[] { (nextX, nextY), (nextX, BallY), (BallX, nextY) })
        {
            if (Bricks.ContainsKey((pos.x, pos.y)))
            {
                hitBrick = (pos.x, pos.y);
                collision = true;
                break;
            }
        }

        if (collision)
        {
            if (Bricks.ContainsKey(hitBrick))
            {
                Bricks[hitBrick]--;
                if (Bricks[hitBrick] <= 0)
                {
                    Bricks.Remove(hitBrick);
                    if (rnd.Next(10) == 0)
                        SpawnPowerUp(hitBrick.Item1, hitBrick.Item2);
                }
                Score += 10;
            }

            ThreadPool.QueueUserWorkItem(_ => { try { Console.Beep(800, 10); } catch { } });
            
            if (Math.Abs(nextX - hitBrick.Item1) > Math.Abs(nextY - hitBrick.Item2))
                BallDX *= -1;
            else
                BallDY *= -1;

            if (BallDX == 0) BallDX = (rnd.Next(2) == 0 ? -1 : 1);
            ViewNeedsRefresh = true;
        }

        BallX += BallDX;
        BallY += BallDY;

        if (BallY >= Height - 1)
        {
            Lives--;
            if (Lives <= 0)
            {
                GameOver = true;
                Message = "GAME OVER! Press SPACE to restart | ESC: Menu";
            }
            else
            {
                BallAttached = true;
                BallX = PaddleX + PaddleW / 2;
                BallY = PaddleY - 1;
                Message = "Life lost! Press SPACE to continue | ESC: Menu";
            }
        }

        foreach (var p in PowerUps) p.Y++;
        PowerUps.RemoveAll(p =>
        {
            if (p.Y == PaddleY && p.X >= PaddleX && p.X < PaddleX + PaddleW)
            {
                CollectPowerUp(p);
                return true;
            }
            return (p.Y >= Height);
        });

        if (Bricks.Count == 0)
        {
            LevelWon = true;
            Message = "LEVEL CLEARED! Press SPACE for next level | ESC: Menu";
        }
    }

    public void NextLevel()
    {
        Level++;
        InitBoard();
        Message = $"Level {Level}! Press SPACE to launch | ESC: Menu";
    }

    public void RequestPlayerName()
    {
        RequestingPlayerName = true;
        OnPlayerNameRequested?.Invoke("Enter your name: ");
    }

    public void SavePlayerName(string name, int bestScore)
    {
        RequestingPlayerName = false;

        if (Score > bestScore)
        {
            OnHighScoreSaved?.Invoke(Score);
            Message = "New High Score! Press SPACE to restart | ESC: Menu";
        }

        var playerScore = new PlayerScore
        {
            Name = string.IsNullOrWhiteSpace(name) ? "Player" : name,
            Score = Score,
            Level = Level,
            Difficulty = Difficulty,
            Date = DateTime.Now
        };

        OnScoreSaved?.Invoke(playerScore);
        Message = $"Thanks, {playerScore.Name}! Press SPACE to restart | ESC: Menu";
    }

    public void CheatDestroyAllBricks()
    {
        if (Bricks.Count > 0)
        {
            Bricks.Clear();
            ViewNeedsRefresh = true;
        }
    }}