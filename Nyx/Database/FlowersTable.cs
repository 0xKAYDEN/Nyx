using System;
using System.Linq;

namespace Nyx.Server.Database
{
    public class Flowers
    {
        public static void SaveFlowers()
        {
            using (Write write = new Write("database\\flowers.txt"))
            {
                var Flower = Game.Features.Flowers.Flowers_Poll.Values.ToArray();
                uint cout = (uint)Game.Features.Flowers.Flowers_Poll.Count;
                string[] items = new string[cout];
                for (uint x = 0; x < cout; x++)
                {
                    items[x] = Flower[x].ToString();
                }
                write.Add(items, items.Length).Execute(Mode.Open);
            }
            using (Write write = new Write("database\\Kisses.txt"))
            {
                var Kisse = Game.Features.Flowers.BoyFlowers.Values.ToArray();
                uint cout = (uint)Game.Features.Flowers.BoyFlowers.Count;
                string[] items = new string[cout];
                for (uint x = 0; x < cout; x++)
                {
                    items[x] = Kisse[x].ToString();
                }
                write.Add(items, items.Length).Execute(Mode.Open);
            }
        }
        public static void LoadFlowers()
        {
            using (Read r = new Read("database\\Flowers.txt"))
            {
                if (r.Reader())
                {
                    int count = r.Count;
                    for (uint x = 0; x < count; x++)
                    {
                        string Data = r.ReadString("");
                        if (Data != null)
                        {
                            Game.Features.Flowers Flower = new Game.Features.Flowers();
                            Flower.Read(Data);
                            if (!Game.Features.Flowers.Flowers_Poll.ContainsKey(Flower.UID))
                            {
                                Game.Features.Flowers.Flowers_Poll.TryAdd(Flower.UID, Flower);
                            }
                            else
                                Game.Features.Flowers.Flowers_Poll[Flower.UID] = Flower;
                            Game.Features.Flowers.CulculateRankRouse(Flower);
                            Game.Features.Flowers.CulculateRankLilies(Flower);
                            Game.Features.Flowers.CulculateRankOrchids(Flower);
                            Game.Features.Flowers.CulculateRankTulips(Flower);
                        }
                    }
                }
            }
            using (Read r = new Read("database\\Kisses.txt"))
            {
                if (r.Reader())
                {
                    int count = r.Count;
                    for (uint x = 0; x < count; x++)
                    {
                        string Data = r.ReadString("");
                        if (Data != null)
                        {
                            Game.Features.Flowers Kisse = new Game.Features.Flowers();
                            Kisse.Read(Data);
                            if (!Game.Features.Flowers.BoyFlowers.ContainsKey(Kisse.UID))
                            {
                                Game.Features.Flowers.BoyFlowers.TryAdd(Kisse.UID, Kisse);
                            }
                            else
                                Game.Features.Flowers.BoyFlowers[Kisse.UID] = Kisse;
                            Game.Features.Flowers.CulculateRankKiss(Kisse);
                            Game.Features.Flowers.CulculateRankLove(Kisse);
                            Game.Features.Flowers.CulculateRankTine(Kisse);
                            Game.Features.Flowers.CulculateRankJade(Kisse);
                        }
                    }
                }
            }
            GC.Collect();
        }
    }
}