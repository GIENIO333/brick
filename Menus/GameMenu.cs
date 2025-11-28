using BrickGame.Models;
using System.Text.Json;

namespace BrickGame.Menus;
public class GameMenu
{
    private int selected = 0;
    private List<string> options = new List<string> { "Start Game", "Instructions", "Best Score", "Ranking", "Difficulty", "Exit" };

    private const string GAME_TITLE =
@"   __________        .__        __   /\       ___.          .__  .__   
   \______   \_______|__| ____ |  | _)/____   \_ |__ _____  |  | |  |  
    |    |  _/\_  __ \  |/ ___\|  |/ //    \   | __ \\__  \ |  | |  |  
    |    |   \ |  | \/  \  \___|    <|   |  \  | \_\ \/ __ \|  |_|  |__
    |______  / |__|  |__|\___  >__|_ \___|  /  |___  (____  /____/____/
           \/                \/     \/    \/       \/     \/            ";

    private void DrawTitleAnimated()
    {
        ConsoleColor[] colors = { ConsoleColor.Magenta, ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.Yellow };
        int colorIndex = DateTime.Now.Millisecond / 250 % colors.Length;
        Console.ForegroundColor = colors[colorIndex];

        var lines = GAME_TITLE.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        int consoleWidth = 80;
        foreach (var line in lines)
        {
            int padding = (consoleWidth - line.Length) / 2;
            if (padding > 0) Console.Write(new string(' ', padding));
            Console.WriteLine(line);
        }
        Console.ResetColor();
        Console.WriteLine();
    }

    private List<PlayerScore> LoadRanking()
    {
        try
        {
            if (!File.Exists("scores.json"))
                return new List<PlayerScore>();

            var json = File.ReadAllText("scores.json") ?? "";
            return JsonSerializer.Deserialize<List<PlayerScore>>(json) ?? new List<PlayerScore>();
        }
        catch
        {
            return new List<PlayerScore>();
        }
    }

    public void ShowDifficulty(GameModel model)
    {
        int choice = 0;
        string[] diffs = { "Easy", "Medium", "Hard" };

        for (int i = 0; i < diffs.Length; i++)
        {
            if (diffs[i] == model.Difficulty)
            {
                choice = i;
                break;
            }
        }

        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("========== SELECT DIFFICULTY ==========\n");
            Console.ResetColor();
            for (int i = 0; i < diffs.Length; i++)
            {
                if (i == choice)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($" -> {diffs[i]} <-");
                    Console.ResetColor();
                }
                else Console.WriteLine($"   {diffs[i]}");
            }

            Console.WriteLine($"\nCurrent: {diffs[choice]} - Lives: {GetLivesForDifficulty(diffs[choice])}");
            Console.WriteLine("\nUse UP/DOWN or W/S to move, ENTER to confirm. ESC: Back");

            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Escape) return;
            if (key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.W)
                choice = (choice - 1 + diffs.Length) % diffs.Length;
            else if (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.S)
                choice = (choice + 1) % diffs.Length;
            else if (key.Key == ConsoleKey.Enter)
            {
                string oldDifficulty = model.Difficulty;
                model.Difficulty = diffs[choice];

                if (oldDifficulty != model.Difficulty)
                {
                    model.Reset();
                }

                try { File.WriteAllText("difficulty.txt", model.Difficulty); } catch { }
                Console.Clear();
                Console.WriteLine($"Difficulty set to {model.Difficulty}!");
                Console.WriteLine($"Lives: {GetLivesForDifficulty(model.Difficulty)}");
                Console.WriteLine("\nPress any key to return...");
                Console.ReadKey(true);
                return;
            }
        }
    }

    private int GetLivesForDifficulty(string difficulty)
    {
        return difficulty switch
        {
            "Easy" => 5,
            "Hard" => 2,
            _ => 3,
        };
    }

    public bool Show(GameModel model)
    {
        while (true)
        {
            Console.Clear();
            DrawTitleAnimated();
            Console.WriteLine("                     by Patryk Klewinowski & Bartosz Janczewski  |  version 1.0\n");

            for (int i = 0; i < options.Count; i++)
            {
                if (i == selected)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($" -> {options[i]} <-");
                    Console.ResetColor();
                }
                else Console.WriteLine($"   {options[i]}");
            }

            Console.WriteLine("\nUse W/S or arrows, ENTER to select. ESC: Exit");
            var key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape) return false;
            if (key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.W)
                selected = (selected - 1 + options.Count) % options.Count;
            else if (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.S)
                selected = (selected + 1) % options.Count;
            else if (key.Key == ConsoleKey.Enter)
            {
                var opt = options[selected];
                if (opt == "Start Game") return true;
                else if (opt == "Instructions")
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("========== INSTRUCTIONS ==========\n");
                    Console.ResetColor();
                    Console.WriteLine("Move: A/D or LEFT/RIGHT\nLaunch: SPACE\nQuit: Q\nBack to Menu: ESC");
                    Console.WriteLine("Power-ups:\n L - Longer paddle\n S - Slower ball\n P - +100 points\n");
                    Console.WriteLine("Press any key to return...");
                    Console.ReadKey(true);
                }
                else if (opt == "Best Score")
                {
                    Console.Clear();
                    int hs = LoadHighScore();
                    Console.WriteLine("========== BEST SCORE ==========\n");
                    Console.WriteLine($"Highest score so far: {hs}\n\nPress any key...");
                    Console.ReadKey(true);
                }
                else if (opt == "Ranking")
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("========== TOP 10 RANKING ==========\n");
                    Console.ResetColor();

                    var ranking = LoadRanking()
                        .OrderByDescending(r => r.Score)
                        .Take(10)
                        .ToList();

                    if (ranking.Count == 0)
                    {
                        Console.WriteLine("No scores yet! Be the first to play!\n");
                    }
                    else
                    {
                        Console.WriteLine("Rank  Player           Score  Level  Difficulty  Date");
                        Console.WriteLine("--------------------------------------------------------");

                        for (int i = 0; i < ranking.Count; i++)
                        {
                            var score = ranking[i];
                            Console.WriteLine($"{i + 1,-4} {score.Name,-15} {score.Score,-6} {score.Level,-6} {score.Difficulty,-11} {score.Date:yyyy-MM-dd}");
                        }
                        Console.WriteLine();
                    }

                    Console.WriteLine("Press any key to return...");
                    Console.ReadKey(true);
                }
                else if (opt == "Difficulty")
                {
                    int currentLevel = model.Level;
                    int currentScore = model.Score;

                    ShowDifficulty(model);

                    model.Level = currentLevel;
                    model.Score = currentScore;
                }
                else if (opt == "Exit") return false;
            }
        }
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