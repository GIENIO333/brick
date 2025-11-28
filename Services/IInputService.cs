namespace BrickGame.Services;
//Interfejs obsługi wejścia
public interface IInputService
{
    bool KeyAvailable { get; }
    ConsoleKeyInfo ReadKey(bool intercept = true);
    string ReadLine();
    void SetCursorVisibility(bool visible);
}