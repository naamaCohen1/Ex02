using System;
using System.Collections.Generic;

namespace B20_Ex02_01
{
    public class Board<T>
    {
        private int m_Hight;
        private int m_Length;
        private T[,] m_Matrix;
        private T[,] m_MatrixToShow;
        private List<T> m_MatrixValues;
        private int m_NumOfExposedCards;
        private Random m_Random;
        private UserActivety<T> m_UserActivity;

        public int Hight
        {
            get
            {
                return m_Hight;
            }

            set
            {
                m_Hight = value;
            }
        }

        public int Length
        {
            get
            {
                return m_Length;
            }

            set
            {
                m_Length = value;
            }
        }

        public T[,] MatrixToShow
        {
            get
            {
                return m_MatrixToShow;
            }

            set
            {
                m_MatrixToShow = value;
            }
        }

        public int NumOfExposedCards
        {
            get
            {
                return m_NumOfExposedCards;
            }

            set
            {
                m_NumOfExposedCards = value;
            }
        }

        public Board(int i_Length, int i_Hight, List<T> i_ValueList)
        {
            Hight = i_Hight;
            Length = i_Length;
            NumOfExposedCards = 0;
            m_Random = new Random();
            m_UserActivity = new UserActivety<T>();
            m_MatrixToShow = new T[Hight, Length];
            m_Matrix = new T[Hight, Length];
            setMatrixValues(i_ValueList);
        }

        public void ShowBoardSlot(string i_Slot)
        {
            int row;
            int column = i_Slot[0] - 'A';
            int.TryParse(char.ToString(i_Slot[1]), out row);

            MatrixToShow[row - 1, column] = m_Matrix[row - 1, column];
        }

        private void setMatrixValues(List<T> i_ValueList)
        {
            int matrixSize = Hight * Length;
            int numOfValues = matrixSize / 2;
            m_MatrixValues = new List<T>(numOfValues);

            initMatrixValueList(numOfValues, i_ValueList);

            while(m_MatrixValues.Count != 0)
            {
                int index = m_Random.Next(0, m_MatrixValues.Count);
                setSlotValue(index);
                m_MatrixValues.RemoveAt(index);
            }
        }

        private void setSlotValue(int i_Index)
        {
            int row, column;
            int count = 0;

            while(count != 2)
            {
                row = m_Random.Next(0, Hight);
                column = m_Random.Next(0, Length);

                if(m_Matrix[row, column].Equals(default(T)))
                {
                    m_Matrix[row, column] = m_MatrixValues[i_Index];
                    count++;
                }
            }
        }

        private void initMatrixValueList(int i_NumOfValues, List<T> i_ValueList)
        {
            int num;
            T value;

            while(m_MatrixValues.Count != i_NumOfValues)
            {
                num = m_Random.Next(0, i_ValueList.Count);
                value = i_ValueList[num];
                if(!m_MatrixValues.Contains(value))
                {
                    m_MatrixValues.Add(value);
                }
            }
        }
    }
}