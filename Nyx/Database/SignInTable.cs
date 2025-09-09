using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nyx.Server.Database
{
   public static class SignInTable
    {
       public class Prize
       {
           public uint PrizeItemID;
           public byte Days2Claim;
       }
       public static Dictionary<byte, Prize> Prizes;
       public static void Load()
       {
           Prizes = new Dictionary<byte, Prize>();
           var r = new IniFile("database\\signin.ini");
           byte count = r.ReadByte("Prize", "amount", 0);
           for (byte i = 1; i <= count; i++)
           {
               Prize prize = new Prize();
               prize.Days2Claim = r.ReadByte(i.ToString(), "needDays", 0);
               prize.PrizeItemID = r.ReadUInt32(i.ToString(), "prizeItemType");
               Prizes.Add(prize.Days2Claim, prize);
           }
       }
    }
}
