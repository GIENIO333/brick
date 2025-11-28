namespace BrickGame.Services;
//Implementacja obsługi konsoli
public class ConsoleInputService : IInputService
{
    public bool KeyAvailable => Console.KeyAvailable;
    public ConsoleKeyInfo ReadKey(bool intercept = true) => Console.ReadKey(intercept);
    public string ReadLine() => Console.ReadLine() ?? "";
    public void SetCursorVisibility(bool visible) => Console.CursorVisible = visible;
}