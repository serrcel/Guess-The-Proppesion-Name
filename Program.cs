namespace GuessGame
{
    public class Program
    {
        static void Main(string[] args)
        {
            var game = new GameProcess();
            game.ShowAllTree();
            game.StartGame();
        }
    }
}