
using BrickGame.Services;
using BrickGame.Models;
using BrickGame.Views;
using BrickGame.Controllers;
using BrickGame.Menus;
using System.Diagnostics;
using System.Runtime.Versioning;
[assembly: SupportedOSPlatform("windows")]
class Program
{
    const string GAME_TITLE =
@"   __________        .__        __   /\       ___.          .__  .__   
   \______   \_______|__| ____ |  | _)/____   \_ |__ _____  |  | |  |  
    |    |  _/\_  __ \  |/ ___\|  |/ //    \   | __ \\__  \ |  | |  |  
    |    |   \ |  | \/  \  \___|    <|   |  \  | \_\ \/ __ \|  |_|  |__
    |______  / |__|  |__|\___  >__|_ \___|  /  |___  (____  /____/____/
           \/                \/     \/    \/       \/     \/            ";

    static void Main()
    {
        try
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
        }
        catch { }

        Console.CursorVisible = false;
        Console.Title = "Brick'n'Ball";

        var inputService = new ConsoleInputService();
        bool exitGame = false;

        while (!exitGame)
        {
            var model = new GameModel();
            var menu = new GameMenu();

            try
            {
                if (File.Exists("difficulty.txt"))
                {
                    var d = File.ReadAllText("difficulty.txt").Trim();
                    if (!string.IsNullOrEmpty(d)) model.Difficulty = d;
                }
            }
            catch { }

            if (!menu.Show(model))
            {
                exitGame = true;
                continue;
            }

            var view = new GameView();
            var controller = new GameController(model, inputService);

            model.OnHighScoreSaved += controller.SaveHighScore;
            model.OnScoreSaved += controller.SaveToRanking;

            bool returnToMenu = RunGameLoop(model, view, controller);

            if (!returnToMenu || !controller.IsGameRunning)
            {
                exitGame = true;
            }
        }

        Console.Clear();
        Console.WriteLine("\nThanks for playing Brick'n'Ball!");
        Console.CursorVisible = true;
    }

    private static bool RunGameLoop(GameModel model, GameView view, GameController controller)
    {
        var sw = Stopwatch.StartNew();
        double lastUpdate = sw.Elapsed.TotalSeconds;
        double lastRender = sw.Elapsed.TotalSeconds;

        try
        {
            Console.SetWindowSize(85, 35);
            Console.SetBufferSize(85, 35);
        }
        catch { }

        while (!controller.ShouldReturnToMenu && controller.IsGameRunning)
        {
            double currentTime = sw.Elapsed.TotalSeconds;

            controller.ProcessInput();

            if (currentTime - lastUpdate >= model.Speed)
            {
                model.Update();
                lastUpdate = currentTime;
            }
              //render ograniczony do 60FPS
            if (currentTime - lastRender >= 0.016)
            {
                view.Draw(model);
                model.ViewNeedsRefresh = false;
                lastRender = currentTime;
            }

            Thread.Sleep(1);
        }

        return controller.ShouldReturnToMenu;
    }
}