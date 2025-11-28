namespace BrickGame.Models;
//wyniki i dane gracza
public class PlayerScore
{
    public string Name { get; set; } = "";
    public int Score { get; set; }
    public int Level { get; set; }
    public string Difficulty { get; set; } = "Medium";
    public DateTime Date { get; set; } = DateTime.Now;
}