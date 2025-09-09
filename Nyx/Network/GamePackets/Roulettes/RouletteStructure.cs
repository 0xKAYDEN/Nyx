using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nyx.Server.Game
{
    public struct BetAtt
    {
        public byte Type;
        public byte Profitability;
        public List<byte> Values;
    }

    public class RouletteStructure
    {
        public BetAtt BetAttribute;
        public uint BetAmount;

        public void GetValues()
        {
            byte[] Red = {1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36};
            byte[] Black = {2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35};
            byte[] Odd = {1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25, 27, 29, 31, 33, 35};
            byte[] Even = {2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34, 36};
            byte[] Small = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18};
            byte[] Big = {19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36};
            byte[] Front = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12};
            byte[] Middle = {13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24};
            byte[] Back = {25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36};
            byte[] Line1 = {1, 4, 7, 10, 13, 16, 19, 22, 25, 28, 31, 34};
            byte[] Line2 = {2, 5, 8, 11, 14, 17, 20, 23, 26, 29, 32, 35};
            byte[] Line3 = {3, 6, 9, 12, 15, 18, 21, 24, 27, 30, 33, 36};
            BetAttribute.Values = new List<byte>();
            if (BetAttribute.Type == 152)
            {
                BetAttribute.Profitability = 2;
                BetAttribute.Values.AddRange(Black);
            
            }
            else if (BetAttribute.Type == 151)
            {
                BetAttribute.Profitability = 2;
                BetAttribute.Values.AddRange(Red);
            }
            else if (BetAttribute.Type == 112)
            {
                BetAttribute.Profitability = 2;
                BetAttribute.Values.AddRange(Odd);
            }
            else if (BetAttribute.Type == 111)
            {
                BetAttribute.Profitability = 2;
                BetAttribute.Values.AddRange(Even);
            }
            else if (BetAttribute.Type == 141)
            {
                BetAttribute.Profitability = 2;
                BetAttribute.Values.AddRange(Small);
            }
            else if (BetAttribute.Type == 142)
            {
                BetAttribute.Profitability = 2;
                BetAttribute.Values.AddRange(Big);
            }
            else if (BetAttribute.Type == 121)
            {
                BetAttribute.Profitability = 3;
                BetAttribute.Values.AddRange(Front);
            }
            else if (BetAttribute.Type == 122)
            {
                BetAttribute.Profitability = 3;
                BetAttribute.Values.AddRange(Middle);
            }
            else if (BetAttribute.Type == 123)
            {
                BetAttribute.Profitability = 3;
                BetAttribute.Values.AddRange(Back);
            }
            else if (BetAttribute.Type == 131)
            {
                BetAttribute.Profitability = 3;
                BetAttribute.Values.AddRange(Line1);
            }
            else if (BetAttribute.Type == 132)
            {
                BetAttribute.Profitability = 3;
                BetAttribute.Values.AddRange(Line2);
            }
            else if (BetAttribute.Type == 133)
            {
                BetAttribute.Profitability = 3;
                BetAttribute.Values.AddRange(Line3);
            }
            else
            {
                BetAttribute.Profitability = 36;
                BetAttribute.Values.Add(BetAttribute.Type);
            }
        }
    }

    public class RoulettePlayerStructure
    {
        public string Name;
        public uint Mesh;
        public uint ID;
        public uint RoulletBetAmount;
        public uint RoulletWinnigAmount { get; set; }
        public uint RouletteID { get; set; }
        public uint RouletteWatchID { get; set; }
        public byte RoulettePlace { get; set; }
    }
}