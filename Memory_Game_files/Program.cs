using System;
namespace B20_Ex02_01
{
    public class Program
    {
        public static void Main()
        {
            runApp();
        }

        private static void runApp()
        {
            CharValues values = new CharValues();
            UserActivety<char?> userActivity = new UserActivety<char?>();
            Player firstPlayer = new Player();
            Player secondPlayer = new Player();
            int length = 0, hight = 0;
            bool newGame = true;

            userActivity.GetMainPlayerInput(ref firstPlayer, ref secondPlayer);
            while(newGame)
            {
                userActivity.GetBoardSize(ref length, ref hight);
                Board<char?> board = new Board<char?>(length, hight, values.CharList);
                Game<char?> game = new Game<char?>(board, ref firstPlayer, ref secondPlayer);
                userActivity.PrintBoard(board);
                game.PlayGame();
                newGame = userActivity.StartNewGame();
            }

            userActivity.Print("****** Game Over! Bye Bye ******");
        }
    }
}
