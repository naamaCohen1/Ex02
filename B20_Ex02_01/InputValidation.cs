using System;
using System.Threading;

namespace B20_Ex02_01
{
    public class InputValidation<T>
    {

        public bool CheckBoardSize(string i_BoardHight, string i_BoardLength)
        {
            int length, hight;
            bool isValidInput = true;
            int.TryParse(i_BoardLength, out length);
            int.TryParse(i_BoardHight, out hight);

            if((hight > 6 || hight < 4) || (length > 6 || length < 4))
            {
                isValidInput = false;
            }
            else if((hight * length) % 2 != 0)
            {
                isValidInput = false;
            }

            if(!isValidInput)
            {
                Console.WriteLine("Invalid Board Size. Please try again.");
            }

            return isValidInput;
        }

        public bool CheckSlotValidation(string i_StringSlot, Board<T> i_Board)
        {
            bool isValidInput = false;

            shouldExit(i_StringSlot);
            if(i_StringSlot.Length == 2)
            {
                isValidInput = checkSlot(i_StringSlot, i_Board);
            }
            else
            {
                Console.WriteLine("Slot should contain row and column. Please try again.");
            }

            return isValidInput;
        }

        private bool checkSlot(string i_StringSlot, Board<T> i_Board)
        {
            int row;
            bool isValidInput = true;
            int column = i_StringSlot[0] - 'A';
            int.TryParse(char.ToString(i_StringSlot[1]), out row);

            if(row > i_Board.Hight || row < 1)
            {
                isValidInput = false;
                Console.WriteLine("Invalid Board row. Please try again.");
            }
            else if(column > i_Board.Length - 1 || column < 0)
            {
                isValidInput = false;
                Console.WriteLine("Invalid Board column. Please try again.");
            }
            else if(!i_Board.MatrixToShow[row - 1, column].Equals(default(T)))
            {
                isValidInput = false;
                Console.WriteLine("This slot is already selected. Please try again.");
            }

            return isValidInput;
        }

        private void shouldExit(string i_StringSlot)
        {
            if(i_StringSlot.Equals("Q"))
            {
                Console.WriteLine("You Chose to ended the game. Bye Bye");
                Thread.Sleep(1000);
                Environment.Exit(0);
            }
        }

        public bool ChekAnswer(string i_Answer, ref bool io_NewGame)
        {
            bool isValidInput = false;

            if(i_Answer.ToLower().Equals("yes"))
            {
                io_NewGame = true;
                isValidInput = true;
            }
            else if(i_Answer.ToLower().Equals("no"))
            {
                io_NewGame = false;
                isValidInput = true;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter Yes or No.");
            }

            return isValidInput;
        }

        public bool CheckNumOfPlayers(string i_NumOfPlayers)
        {
            bool isValidInput = false;

            if(i_NumOfPlayers.Equals("1") || i_NumOfPlayers.Equals("2"))
            {
                isValidInput = true;
            }
            else
            {
                Console.WriteLine("Invalid input. Please try again.");
            }

            return isValidInput;
        }
    }
}