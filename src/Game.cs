using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace B20_Ex02_01
{
    public class Game<T>
    {
        private UserActivety<T> m_UserActivety;
        private Board<T> m_Board;
        private Player m_FirstPlayer;
        private Player m_SecondPlayer;
        private Random m_Random;

        public Game(Board<T> i_Board, ref Player io_FirstPlayer, ref Player io_SecondPlayer)
        {
            m_Board = i_Board;
            m_FirstPlayer = io_FirstPlayer;
            m_SecondPlayer = io_SecondPlayer;
            m_UserActivety = new UserActivety<T>();
            m_Random = new Random();
        }

        public void PlayGame()
        {
            int matrixSize = m_Board.Hight * m_Board.Length;

            while(m_Board.NumOfExposedCards < matrixSize)
            {
                playTurn(ref m_FirstPlayer);
                if(m_Board.NumOfExposedCards < matrixSize)
                {
                    playTurn(ref m_SecondPlayer);
                }
            }

            checkWinner();
        }

        private void checkWinner()
        {
            StringBuilder stringBuilder = new StringBuilder("***** RESULTS *****" + Environment.NewLine);

            if(m_FirstPlayer.Points < m_SecondPlayer.Points)
            {
                stringBuilder.AppendFormat("{0} is the WINNER!!!{1}", m_SecondPlayer.Name, Environment.NewLine);
            }
            else if(m_FirstPlayer.Points > m_SecondPlayer.Points)
            {
                stringBuilder.AppendFormat("{0} is the WINNER!!!{1}", m_FirstPlayer.Name, Environment.NewLine);
            }
            else
            {
                stringBuilder.AppendFormat("There is no winner, It's a Tie!!!{0}", Environment.NewLine);
            }

            stringBuilder.AppendFormat("{0}'s points: {1}{2}", m_FirstPlayer.Name, m_FirstPlayer.Points, Environment.NewLine);
            stringBuilder.AppendFormat("{0}'s points: {1}", m_SecondPlayer.Name, m_SecondPlayer.Points);
            m_UserActivety.Print(stringBuilder.ToString());
        }

        private void playTurn(ref Player io_Player)
        {
            string firstSlot = null;
            string secondSlot = null;

            if(!io_Player.Name.Equals("Computer"))
            {
                firstSlot = m_UserActivety.GetSlotFromUser(m_Board, ref io_Player);
                m_Board.ShowBoardSlot(firstSlot);
                m_UserActivety.PrintBoard(m_Board);
                secondSlot = m_UserActivety.GetSlotFromUser(m_Board, ref io_Player);
                m_Board.ShowBoardSlot(secondSlot);
                m_UserActivety.PrintBoard(m_Board);
            }
            else
            {
                computerTurn(ref firstSlot, ref secondSlot);
                m_UserActivety.Print("Computer Turn... Please wait for your turn");
            }

            Thread.Sleep(2000);
            setTurnResult(ref io_Player, firstSlot, secondSlot);
            m_UserActivety.PrintBoard(m_Board);
        }

        private void setTurnResult(ref Player io_Player, string i_FirstSlot, string i_SeconSlot)
        {
            int firstRow, secondRow;
            int firstColumn = i_FirstSlot[0] - 'A';
            int.TryParse(char.ToString(i_FirstSlot[1]), out firstRow);
            int secondColumn = i_SeconSlot[0] - 'A';
            int.TryParse(char.ToString(i_SeconSlot[1]), out secondRow);

            if(m_Board.MatrixToShow[firstRow - 1, firstColumn].Equals
                (m_Board.MatrixToShow[secondRow - 1, secondColumn]))
            {
                io_Player.Points++;
                m_Board.NumOfExposedCards += 2;
            }
            else
            {
                m_Board.MatrixToShow[firstRow - 1, firstColumn] = default(T);
                m_Board.MatrixToShow[secondRow - 1, secondColumn] = default(T);
            }
        }

        private void computerTurn(ref string io_FirstSlot, ref string io_SeconSlot)
        {
            io_FirstSlot = generateFirstSlot();
            m_Board.ShowBoardSlot(io_FirstSlot);
            m_UserActivety.PrintBoard(m_Board);
            io_SeconSlot = generateSecondSlot(io_FirstSlot);
            m_Board.ShowBoardSlot(io_SeconSlot);
            m_UserActivety.PrintBoard(m_Board);
        }

        private string generateFirstSlot()
        {
            int row = 0, column = 0;
            char? columnChar = null;
            string spot = null, finalSpot = null;

            while(finalSpot == null)
            {
                generate(ref column, ref row, ref columnChar);
                if(m_Board.MatrixToShow[row - 1, column].Equals(default(T)))
                {
                    spot = string.Format("{0}{1}", columnChar, row);
                    if(!m_SecondPlayer.UsedSpots.ContainsKey(spot))
                    {
                        m_SecondPlayer.UsedSpots.Add(spot, new List<string>());
                    }

                    finalSpot = spot;
                }
            }

            return finalSpot;
        }

        private string generateSecondSlot(string i_FirstSlot)
        {
            int row = 0, column = 0;
            char? columnChar = null;
            string spot = null, finalSpot = null;
            List<string> userSpots;

            while(finalSpot == null)
            {
                generate(ref column, ref row, ref columnChar);
                if(m_Board.MatrixToShow[row - 1, column].Equals(default(T)))
                {
                    spot = string.Format("{0}{1}", columnChar, row);
                    m_SecondPlayer.UsedSpots.TryGetValue(i_FirstSlot, out userSpots);
                    if(!userSpots.Contains(spot))
                    {
                        finalSpot = spot;
                        userSpots.Add(spot);
                    }
                }
            }

            return finalSpot;
        }

        private void generate(ref int io_Column, ref int io_Row, ref char? io_ColumnChar)
        {
            io_Row = m_Random.Next(1, m_Board.Hight + 1);
            io_Column = m_Random.Next(0, m_Board.Length);
            io_ColumnChar = (char)('A' + io_Column);
        }
    }
}