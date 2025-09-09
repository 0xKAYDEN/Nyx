using System;
using Nyx.Server.Network;
using Nyx.Server.Database;
using Nyx.Server;
using Nyx.Server.Interfaces;
using Nyx.Server.Network.GamePackets;
using Message = Nyx.Server.Network.GamePackets.Message;

namespace Nyx.Server.Game.Attacking
{
    public class DurabilityItems
    {
        public static void LessDura(Entity Target, byte DuraLess = 0)
        {
            if (Target.EntityFlag == EntityFlag.Player && Target.Owner.Equipment != null)
            {
                foreach (var item in Target.Owner.Equipment.Objects)
                {
                    if (item == null) continue;
                   ushort pos = (ushort)PacketHandler.GetPositionFromID(item.ID);
                   if (item != null && item.ID != 0 && item.Durability > 0 && !Target.Owner.Equipment.Free(pos) && !Network.PacketHandler.IsArrow(item.ID) && pos != ConquerItem.Bottle && pos != ConquerItem.Bottle && pos != ConquerItem.Garment && pos != ConquerItem.Steed && pos != ConquerItem.SteedArmor && pos != ConquerItem.AlternateGarment && pos != ConquerItem.LeftWeaponAccessory && pos != ConquerItem.RightWeaponAccessory)
                    {
                        if (DuraLess == 0)
                        {
                            item.Durability = (ushort)(item.Durability - (10 * 100));//10
                        }
                        else
                        {
                            item.Durability = (ushort)(item.Durability - (DuraLess * 100));
                        }
                        while (item.Durability < 0 || item.Durability > 7700)
                        {
                            item.Durability = 0;
                            ConquerItemTable.UpdateDurabilityItem(item);
                        }
                        item.Mode = Enums.ItemMode.Update;
                        item.Send(Target.Owner);
                        if (item.Durability <= 300)
                        {
                            Target.Owner.Send(new Message("Warning: Your item dura is too low, Go and repair it now! ", System.Drawing.Color.Red, 2011));

                        }
                    }
                   else if (item.Durability == 0 && item.ID != 0 && !Network.PacketHandler.IsArrow(item.ID) && pos != ConquerItem.Bottle && pos != ConquerItem.Bottle && pos != ConquerItem.Garment && pos != ConquerItem.Steed && pos != ConquerItem.SteedArmor && pos != ConquerItem.AlternateGarment && pos != ConquerItem.LeftWeaponAccessory && pos != ConquerItem.RightWeaponAccessory && !Target.Owner.Equipment.Free(pos))
                    {
                        item.Durability = 0;
                        item.Mode = Enums.ItemMode.Update;
                        Target.Owner.CalculateStatBonus();
                        Target.Owner.CalculateHPBonus();
                        Target.Owner.GemAlgorithm();
                        Target.Owner.LoadItemStats();
                        WindowsStats WS = new WindowsStats(Target.Owner);
                        WS.Send(Target.Owner);
                        ConquerItemTable.UpdateDurabilityItem(item);
                        Target.Owner.Send(new Message("Warning: Your item dura is bad, go and repair it now.", System.Drawing.Color.Red, 2011));
                        _String str = new _String(true)
                        {
                            UID = Target.UID,
                            Type = 10,
                            TextsCount = 1
                        };
                        str.Texts.Add("expression_error");
                        Target.Owner.Send(str.Encode());
                    }
                }
            }
        }
    }
}