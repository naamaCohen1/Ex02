using System;
using System.Collections.Generic;

namespace B20_Ex02_01
{
    public class CharValues
    {
        private List<char?> m_CharList;
        private Random m_Random;

        public List<char?> CharList
        {
            get
            {
                return m_CharList;
            }

            set
            {
                m_CharList = value;
            }
        }

        public CharValues()
        {
            m_Random = new Random();
            initList();
        }

        private void initList()
        {
            CharList = new List<char?>();
            for(int i = 0; i < 26; i++)
            {
                CharList.Add((char)('A' + i));
            }
        }
    }
}