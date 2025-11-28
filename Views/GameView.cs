using BrickGame.Models;
using System.Text.Json;
// odpowiada za wyświetlanie stanu gry
namespace BrickGame.Views;
public class GameView
{// poprzednia klatka potrzebna do odswiezania czesci obrazu
    private List<string> lastFrame = new List<string>();
    private List<ConsoleColor[]> lastColors = new List<ConsoleColor[]>();
    // pomiar czasu renderowania
    private ConsoleColor BrickColor(int dur)//kolory klockow w zaleznosci od wytrzymalosci
    {
        return dur switch
        {
            3 => ConsoleColor.Red,
            2 => ConsoleColor.Yellow,
            1 => ConsoleColor.Green,
            _ => ConsoleColor.DarkGray,
        };
    }
    //ustawienie kursora na 
    private void Gotoxy(int x, int y)
    {
        try
        {
            if (x >= 0 && y >= 0 && x < Console.WindowWidth && y < Console.WindowHeight)
                Console.SetCursorPosition(x, y);
        }
        catch { }
    }

    private void DrawBorder(GameModel m)
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;

        for (int x = 0; x <= m.Width + 1; x++)
        {
            Gotoxy(m.MarginLeft + x - 1, m.MarginTop - 1);
            Console.Write('═');
            Gotoxy(m.MarginLeft + x - 1, m.MarginTop + m.Height);
            Console.Write('═');
        }
        //sciany boczne
        for (int y = 0; y <= m.Height; y++)
        {
            Gotoxy(m.MarginLeft - 1, m.MarginTop + y);
            Console.Write('║');
            Gotoxy(m.MarginLeft + m.Width, m.MarginTop + y);
            Console.Write('║');
        }

        Gotoxy(m.MarginLeft - 1, m.MarginTop - 1); Console.Write('╔');
        Gotoxy(m.MarginLeft + m.Width, m.MarginTop - 1); Console.Write('╗');
        Gotoxy(m.MarginLeft - 1, m.MarginTop + m.Height); Console.Write('╚');
        Gotoxy(m.MarginLeft + m.Width, m.MarginTop + m.Height); Console.Write('╝');

        Console.ResetColor();
    }
    //rendering
    public void Draw(GameModel m)
    {
        var frame = new List<char[]>();
        var currentColors = new List<ConsoleColor[]>();

        for (int y = 0; y < m.Height; y++)
        {
            frame.Add(Enumerable.Repeat(' ', m.Width).ToArray());
            currentColors.Add(Enumerable.Repeat(ConsoleColor.White, m.Width).ToArray());
        }//wypelnianie spacjami ekranu(pusty ekran)

        char brickChar = '■';
        char paddleChar = '═';
        char ballChar = '●';

        foreach (var b in m.Bricks)
        {
            int x = b.Key.Item1, y = b.Key.Item2;
            if (y < m.Height && x < m.Width)
            {
                frame[y][x] = brickChar;
                currentColors[y][x] = BrickColor(b.Value);
            }
        }

        foreach (var p in m.PowerUps)
        {
            if (p.Y < m.Height && p.X < m.Width)
            {
                frame[p.Y][p.X] = p.Type;
                currentColors[p.Y][p.X] = ConsoleColor.White;
            }
        }

        int paddleEnd = Math.Min(m.PaddleX + m.PaddleW, m.Width);
        int paddleStart = Math.Max(m.PaddleX, 0);
        for (int x = paddleStart; x < paddleEnd; x++)
        {
            if (m.PaddleY < m.Height)
            {
                frame[m.PaddleY][x] = paddleChar;
                currentColors[m.PaddleY][x] = ConsoleColor.Cyan;
            }
        }

        if (m.BallY < m.Height && m.BallX < m.Width && m.BallY >= 0 && m.BallX >= 0)
        {
            frame[m.BallY][m.BallX] = ballChar;
            currentColors[m.BallY][m.BallX] = ConsoleColor.Blue;
        }

        var frameStrs = frame.Select(arr => new string(arr)).ToList();
        //pierwsze rysowanie ekranu
        if (lastFrame.Count == 0)
        {
            Console.Clear();
            for (int y = 0; y < m.Height; y++)
            {
                for (int x = 0; x < m.Width; x++)
                {
                    int drawX = x + m.MarginLeft;
                    int drawY = y + m.MarginTop;

                    Gotoxy(drawX, drawY);
                    Console.ForegroundColor = currentColors[y][x];
                    Console.Write(frame[y][x]);
                }
            }
            Console.ResetColor();
        }
        else
        {// rysowanie tylko zmian
            for (int y = 0; y < m.Height; y++)
            {
                for (int x = 0; x < m.Width; x++)
                {
                    bool charChanged = frameStrs[y][x] != lastFrame[y][x];
                    bool colorChanged = lastColors.Count == 0 ||
                                      (lastColors[y][x] != currentColors[y][x]);

                    if (charChanged || colorChanged)
                    {
                        int drawX = x + m.MarginLeft;
                        int drawY = y + m.MarginTop;

                        Gotoxy(drawX, drawY);
                        Console.ForegroundColor = currentColors[y][x];
                        Console.Write(frame[y][x]);
                    }
                }
            }
            Console.ResetColor();
        }

        lastFrame = frameStrs;
        lastColors = currentColors.Select(arr =>
        {
            var newArr = new ConsoleColor[arr.Length];
            Array.Copy(arr, newArr, arr.Length);
            return newArr;
        }).ToList();

        DrawStatus(m);
        DrawBorder(m);
    }

    private void DrawStatus(GameModel m)
    {
        int best = LoadHighScore();

        Gotoxy(m.MarginLeft, m.Height + m.MarginTop + 1);
        Console.Write($"Score: {m.Score}   Lives: {m.Lives}   Level: {m.Level}   Best: {best}   Diff: {m.Difficulty}   ".PadRight(70));

        Gotoxy(m.MarginLeft, m.Height + m.MarginTop + 2);
        Console.Write(m.Message.PadRight(70));

        Gotoxy(m.MarginLeft, m.Height + m.MarginTop + 3);
        Console.Write(new string(' ', 70));
    }

    private int LoadHighScore()
    {
        try
        {
            if (File.Exists("highscore.txt"))
            {
                var s = File.ReadAllText("highscore.txt").Trim();
                if (int.TryParse(s, out int val)) return val;
            }
        }
        catch { }
        return 0;
    }
}