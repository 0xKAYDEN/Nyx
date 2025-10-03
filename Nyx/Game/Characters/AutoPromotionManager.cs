using Nyx.Server.Client;
using Nyx.Server.Database.Entities;
using Nyx.Server.Interfaces;
using Nyx.Server.Network.GamePackets;
using System.Drawing;

namespace Nyx.Server.Game.Characters
{
    public class AutoPromotionManager
    {

        private GameClient _owner;
        public AutoPromotionManager(GameClient owner)
        {
            _owner = owner;
        }

        // This method is called by the server whenever a significant event happens
        // that could trigger a promotion: Level Up, Metempsychosis, Completing a Quest.
        public void CheckForAvailablePromotions()
        {
            // Use the PromotionManager to get available promotions
            var availablePromotions = Database.Repositories.PromotionsRepository.Instance.GetAvailablePromotions(
                _owner.Entity.Class,
                _owner.Entity.Level,
                _owner.Entity.Reborn
            );

            // Filter out already completed promotions
            var newPromotions = availablePromotions
                .Where(p => !_owner.Entity.CompletedPromotions.Contains(p.PromotionId))
                .ToList();

            foreach (var promotion in newPromotions)
            {
                if (HasRequiredItems(_owner, promotion) && _owner.Entity.Money >= promotion.RequiredMoney)
                {
                    ExecutePromotion(promotion);
                }
            }
        }

        private bool HasRequiredItems(GameClient client, PromotionData promotion)
        {
            foreach (var itemReq in promotion.RequiredItems)
            {
                if (client.Inventory.Contains((uint)itemReq.ItemId, itemReq.Amount))
                    return false;
            }
            return true;
        }

        private void ExecutePromotion(PromotionData promotion)
        {
            // 1. Consume the required items and money
            foreach (var itemReq in promotion.RequiredItems)
            {
                //_owner.Inventory.Remove(itemReq.ItemId, itemReq.Amount);
            }
            _owner.Entity.Money -= (uint)promotion.RequiredMoney;

            // 2. Change the player's class
            _owner.Entity.Class = (byte)promotion.ResultClassId;

            // 3. Mark this promotion as completed
            _owner.Entity.CompletedPromotions.Add(promotion.PromotionId);

            // 4. Grant Rewards
            _owner.Entity.Atributes += (ushort)promotion.StatPointsRewarded;
            foreach (var itemReward in promotion.ItemsRewarded)
            {
                _owner.Inventory.Add((uint)itemReward.ItemID,0, (ushort)itemReward.Amount);
            }

            // 5. MOST IMPORTANT: Auto-Learn/Upgrade Skills (See next section)
            foreach (var skillReward in promotion.SkillsUnlocked)
            {
                // This method handles both new skills and upgrades
                _owner.AddSpell(LearnableMagic(skillReward.SkillID));
            }

            // 6. Notify the player and everyone nearby
            _owner.Send(new Network.GamePackets.Message ($"Congratulations! You have been promoted to {promotion.Name}!",Color.Green, Network.GamePackets.Message.SystemWhisper));

            if (Game.ConquerStructures.Broadcast.Broadcasts.Count > 0)
            {
                Game.ConquerStructures.Broadcast.CurrentBroadcast = Game.ConquerStructures.Broadcast.Broadcasts[0];
                Game.ConquerStructures.Broadcast.Broadcasts.Remove(Game.ConquerStructures.Broadcast.CurrentBroadcast);
                Game.ConquerStructures.Broadcast.LastBroadcast = DateTime.Now;
                _owner.Send(new Network.GamePackets.Message("" + _owner.Entity.Name +" has been promoted!", "ALLUSERS", "System", System.Drawing.Color.Red, Network.GamePackets.Message.BroadcastMessage));
            }
            else
                Game.ConquerStructures.Broadcast.CurrentBroadcast.EntityID = 1;

           

            // 7. Save the character's state to the database
            //_owner.Character.SaveToDatabase();
        }

        public static ISkill LearnableMagic(ushort MagicID)
        {
            ISkill Magic = new Spell(true);
            Magic.ID = MagicID;
            return Magic;
        }


    }
}
