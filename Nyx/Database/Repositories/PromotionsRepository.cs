using Nyx.Server.Database.Entities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Nyx.Server.Database.Entities.PromotionData;

namespace Nyx.Server.Database.Repositories
{
    public class PromotionsRepository
    {
        public static PromotionsRepository _instance;
        public static List<PromotionData> _allPromotions = new List<PromotionData>();

        public static PromotionsRepository Instance
        {
            get
            {
                _instance ??= new PromotionsRepository();
                return _instance;
            }
        }

        private PromotionsRepository() { }

        // Load promotions from JSON file
        public static void LoadPromotions(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"Promotions file not found: {filePath}");
                }

                string jsonContent = File.ReadAllText(filePath);
                var container = JsonSerializer.Deserialize<PromotionDataContainer>(jsonContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                _allPromotions = container?.Promotions ?? new List<PromotionData>();
                Log.Information($"[Promotions] Loaded {_allPromotions.Count} promotions from {filePath}");
            }
            catch (Exception ex)
            {
                Log.Error($"Error loading promotions: {ex.Message}");
                _allPromotions = new List<PromotionData>();
            }
        }

        // Get all promotions for a specific character class
        public List<PromotionData> GetPromotionsForClass(int characterClassId)
        {
            return _allPromotions
                .Where(p => p.RequiredClass == characterClassId)
                .OrderBy(p => p.RequiredLevel) // Order by level for progression
                .ThenBy(p => p.RequiredMetempsychosis) // Then by rebirth level
                .ToList();
        }

        // Get available promotions for a character (filtered by class and level requirements)
        public List<PromotionData> GetAvailablePromotions(int characterClassId, int characterLevel, int characterMetempsychosis)
        {
            return _allPromotions
                .Where(p => p.RequiredClass == characterClassId)
                .Where(p => characterLevel >= p.RequiredLevel)
                .Where(p => characterMetempsychosis >= p.RequiredMetempsychosis)
                .OrderBy(p => p.RequiredLevel)
                .ThenBy(p => p.RequiredMetempsychosis)
                .ToList();
        }

        // Get a specific promotion by ID
        public PromotionData GetPromotionById(int promotionId)
        {
            return _allPromotions.FirstOrDefault(p => p.PromotionId == promotionId);
        }

        // Get next available promotion for a character
        public PromotionData GetNextPromotion(int characterClassId, int characterLevel, int characterMetempsychosis)
        {
            var availablePromotions = GetAvailablePromotions(characterClassId, characterLevel, characterMetempsychosis);

            // Return the first available promotion (lowest level/rebirth requirement)
            return availablePromotions.FirstOrDefault();
        }

        // Get all promotions that lead to a specific result class
        public List<PromotionData> GetPromotionsLeadingToClass(int resultClassId)
        {
            return _allPromotions
                .Where(p => p.ResultClassId == resultClassId)
                .ToList();
        }

        // Check if a character meets requirements for a specific promotion
        public bool MeetsPromotionRequirements(int promotionId, int characterLevel, int characterMetempsychosis)
        {
            var promotion = GetPromotionById(promotionId);
            if (promotion == null)
                return false;

            return characterLevel >= promotion.RequiredLevel &&
                   characterMetempsychosis >= promotion.RequiredMetempsychosis;
        }

        // Reload promotions (useful for hot-reloading during development)
        public void ReloadPromotions(string filePath)
        {
            LoadPromotions(filePath);
        }



    }
}
