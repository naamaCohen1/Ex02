using System;
using System.Collections.Generic;
using System.Text;


namespace B20_Ex02_01
{
    public class UserActivety<T>
    {
        private InputValidation<T> m_InputValidation = new InputValidation<T>();

        public void GetBoardSize(ref int io_Length, ref int io_Hight)
        {
            bool isValidInput = false;
            string stringHight = null, stringLength = null;

            while(!isValidInput)
            {
                Console.WriteLine("{0}Please enter Board size,", Environment.NewLine);
                Console.WriteLine("Board should be between 4X4 to 6X6 and should contain even slots' number.");
                Console.Write("Please enter Board hight: ");
                stringHight = Console.ReadLine();
                Console.Write("Please enter Board length: ");
                stringLength = Console.ReadLine();
                isValidInput = m_InputValidation.CheckBoardSize(stringHight, stringLength);
            }

            int.TryParse(stringLength, out io_Length);
            int.TryParse(stringHight, out io_Hight);
        }

        public void Print(string i_StringBuilder)
        {
            Console.WriteLine(i_StringBuilder);
        }

        public string GetSlotFromUser(Board<T> i_Board, ref Player io_Player)
        {
            string stringSlot = null;
            bool isValidInput = false;

            while(!isValidInput)
            {
                Console.Write("{0}, please choose a slot: ", io_Player.Name);
                stringSlot = Console.ReadLine();
                isValidInput = m_InputValidation.CheckSlotValidation(stringSlot, i_Board);
            }

            return stringSlot;
        }

        public void GetMainPlayerInput(ref Player io_FirstPlayer, ref Player io_SecondPlayer)
        {
            getUserName(ref io_FirstPlayer);
            isVsComputer(ref io_SecondPlayer);
        }

        private void getUserName(ref Player io_Player)
        {
            Console.Write("Please enter Player name: ");
            io_Player.Name = Console.ReadLine();
        }

        private void isVsComputer(ref Player io_SecondPlayer)
        {
            string numOfPlayers = null;
            bool isValidInput = false;

            while(!isValidInput)
            {
                Console.Write(
                    "Do you want to play with another player or computer [1-vs computer , 2-vs another player]: ");
                numOfPlayers = Console.ReadLine();
                isValidInput = m_InputValidation.CheckNumOfPlayers(numOfPlayers);
            }

            if(numOfPlayers.Equals("1"))
            {
                io_SecondPlayer.Name = "Computer";
                io_SecondPlayer.UsedSpots = new Dictionary<string, List<string>>();
            }
            else
            {
                getUserName(ref io_SecondPlayer);
            }
        }

        public bool StartNewGame()
        {
            bool isValidInput = false, newGame = false;
            string answer = null;

            while(!isValidInput)
            {
                Console.Write("{0}Do you want to start a new game [Yes / No]? ", Environment.NewLine);
                answer = Console.ReadLine();
                isValidInput = m_InputValidation.ChekAnswer(answer, ref newGame);
            }

            if(newGame)
            {
                System.Console.Clear();
            }

            return newGame;
        }

        public void PrintBoard(Board<T> i_Board)
        {
            int hight = i_Board.Hight;
            int length = i_Board.Length;
            StringBuilder stringBuilder = new StringBuilder(Environment.NewLine);
            StringBuilder seperatorStringBuilder;

            System.Console.Clear();
            seperatorStringBuilder = initBoardLineAnSeperator(stringBuilder, i_Board);
            for(int i = 0; i < hight; i++)
            {
                stringBuilder.AppendFormat("{0} |", i + 1);
                for(int j = 0; j < length; j++)
                {
                    if(i_Board.MatrixToShow[i, j].Equals(default(T)))
                    {
                        stringBuilder.AppendFormat("   |");
                    }
                    else
                    {
                        stringBuilder.AppendFormat(" {0} |", i_Board.MatrixToShow[i, j]);
                    }
                }

                stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append(seperatorStringBuilder);
            }

            Console.WriteLine(stringBuilder);
        }

        private StringBuilder initBoardLineAnSeperator(StringBuilder i_StringBuilder, Board<T> i_Board)
        {
            int length = i_Board.Length;
            StringBuilder seperatorStringBuilder = new StringBuilder("  ");
            char letterToPrint = 'A';
            string separator = "====";

            i_StringBuilder.Append("   ");
            for(int i = 0; i < length; i++)
            {
                i_StringBuilder.AppendFormat(" {0}  ", letterToPrint);
                seperatorStringBuilder.Append(separator);
                letterToPrint++;
            }

            i_StringBuilder.Append(Environment.NewLine);
            seperatorStringBuilder.Append(Environment.NewLine);
            i_StringBuilder.Append(seperatorStringBuilder);

            return seperatorStringBuilder;
        }
    }
}