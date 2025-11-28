using BrickGame.Models;
using BrickGame.Services;
using System.Text.Json;
//interakcja miedzy model a view
namespace BrickGame.Controllers;
public class GameController
{
    private GameModel model;
    private IInputService inputService;
    private bool returnToMenu = false;
    private bool gameRunning = true;

    public GameController(GameModel m, IInputService inputService)
    {
        model = m;
        this.inputService = inputService;
        model.OnPlayerNameRequested += HandlePlayerNameRequest;
    }

    public bool ProcessInput()
    {
        if (!inputService.KeyAvailable)
            return false;

        var key = inputService.ReadKey(true);

        if (key.Key == ConsoleKey.Escape)
        {
            returnToMenu = true;
            return true;
        }

        if (key.Key == ConsoleKey.Q)
        {
            gameRunning = false;
            return true;
        }

        if (key.Key == ConsoleKey.C)
        {
            model.CheatDestroyAllBricks();
            return true;
        }

        if (model.RequestingPlayerName)
            return true;

        if (key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.A)
            model.MovePaddleLeft();
        else if (key.Key == ConsoleKey.RightArrow || key.Key == ConsoleKey.D)
            model.MovePaddleRight();
        else if (key.Key == ConsoleKey.Spacebar)
            HandleSpacebar();

        return true;
    }
    //uzywanie spacji
    private void HandleSpacebar()
    {
        if (model.GameOver)
        {
            model.RequestPlayerName();
        }
        else if (model.LevelWon)
        {
            model.NextLevel();
        }
        else
        {
            model.LaunchBall();
        }
    }
    //obsluga imienia
    private void HandlePlayerNameRequest(string prompt)
    {
        int clearY = model.Height + model.MarginTop + 3;
        Console.SetCursorPosition(model.MarginLeft, clearY);
        Console.Write(new string(' ', 50));
        Console.SetCursorPosition(model.MarginLeft, clearY);
        Console.Write(prompt);

        inputService.SetCursorVisibility(true);
        string name = (inputService.ReadLine() ?? "").Trim();
        if (string.IsNullOrWhiteSpace(name))
            name = "Player";
        inputService.SetCursorVisibility(false);

        int bestScore = LoadHighScore();
        model.SavePlayerName(name, bestScore);
        model.Reset();
    }

    public bool ShouldReturnToMenu => returnToMenu;
    public bool IsGameRunning => gameRunning;

    private int LoadHighScore()
    {
        if (!File.Exists("highscore.txt")) return 0;

        var s = File.ReadAllText("highscore.txt")?.Trim() ?? "0";
        if (int.TryParse(s, out int val))
            return val;
        return 0;
    }

    public void SaveHighScore(int score)
    {
        try { File.WriteAllText("highscore.txt", score.ToString()); } catch { }
    }

    public void SaveToRanking(PlayerScore playerScore)
    {
        try
        {
            var ranking = new List<PlayerScore>();
            if (File.Exists("scores.json"))
            {
                var json = File.ReadAllText("scores.json");
                ranking = JsonSerializer.Deserialize<List<PlayerScore>>(json) ?? new List<PlayerScore>();
            }

            ranking.Add(playerScore);
            ranking = ranking.OrderByDescending(r => r.Score).Take(20).ToList();

            var newJson = JsonSerializer.Serialize(ranking, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("scores.json", newJson);
        }
        catch { }
    }
}