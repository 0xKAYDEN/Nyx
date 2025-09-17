using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Nyx.Server.Database.Entities
{
    public class PromotionData
    {
        [JsonPropertyName("promotionId")]
        public int PromotionId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } // e.g., "Second Promotion"

        [JsonPropertyName("requiredLevel")]
        public int RequiredLevel { get; set; } // e.g., 70

        [JsonPropertyName("requiredClass")]
        public int RequiredClass { get; set; } // ID of the class that can promote (e.g., Warrior)

        [JsonPropertyName("requiredMetempsychosis")]
        public int RequiredMetempsychosis { get; set; } // e.g., 0 for 1st promo, 1 for 2nd, etc.

        [JsonPropertyName("resultClassId")]
        public int ResultClassId { get; set; } // The new class ID the player becomes

        // You can add items, money, or quest requirements here

        [JsonPropertyName("requiredMoney")]
        public int RequiredMoney { get; set; }

        [JsonPropertyName("requiredItems")]
        public List<ItemRequirement> RequiredItems { get; set; } = new List<ItemRequirement>(); // e.g., Dragon Gem

        // Rewards upon promotion
        [JsonPropertyName("skillsUnlocked")]
        public List<SkillReward> SkillsUnlocked { get; set; } = new List<SkillReward>();

        [JsonPropertyName("itemsRewarded")]
        public List<ItemReward> ItemsRewarded { get; set; } = new List<ItemReward>();

        [JsonPropertyName("statPointsRewarded")]
        public int StatPointsRewarded { get; set; }

        public class ItemRequirement
        {
            [JsonPropertyName("itemId")]
            public int ItemId { get; set; }

            [JsonPropertyName("amount")]
            public ushort Amount { get; set; }
        }
        public class SkillReward
        {
            [JsonPropertyName("skillId")]
            public ushort SkillID { get; set; }
        }
        public class ItemReward
        {
            [JsonPropertyName("itemId")]
            public ushort ItemID { get; set; }

            [JsonPropertyName("amount")]
            public int Amount { get; set; }
        }

        public class PromotionDataContainer
        {
            [JsonPropertyName("promotions")]
            public List<PromotionData> Promotions { get; set; } = new List<PromotionData>();
        }
    }
}
