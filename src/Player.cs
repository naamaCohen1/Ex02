using System;
using System.Collections.Generic;

namespace B20_Ex02_01
{
    public struct Player
    {
        private string m_Name;
        private int m_Points;
        private Dictionary<string, List<string>> m_UsedSpots;

        public string Name
        {
            get
            {
                return m_Name;
            }

            set
            {
                m_Name = value;
            }
        }

        public int Points
        {
            get
            {
                return m_Points;
            }

            set
            {
                m_Points = value;
            }
        }

        public Dictionary<string, List<string>> UsedSpots
        {
            get
            {
                return m_UsedSpots;
            }

            set
            {
                m_UsedSpots = value;
            }
        }
    }
}
