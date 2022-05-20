using System.Collections.Generic;
using Totem.Util;
using UnityEngine;
using ValheimLib;
using ValheimLib.ODB;

namespace Totem.Items.TotemE
{
    public static class ItemDataE
    {
        public static CustomItem CustomItem;
        public static CustomRecipe CustomRecipe;

        public const string TokenName = "$custom_item_Etotem";
        public const string TokenValue = "Eikthyr Totem";

        public const string TokenDescriptionName = "$custom_item_etotem_description";
        public const string TokenDescriptionValue = "Eikthyr Totem gives you Eikthyr's abilty as a passive";

        public const string CraftingStationPrefabName = "piece_workbench";

        //public static ItemDrop TotemData = AssetHelper.TotemPrefab.GetComponent<ItemDrop>();

        internal static void Init(int recipediffculty)
        {


            AddCustomItem();
            AddCustomRecipe(recipediffculty);          

            Language.AddToken(TokenName, TokenValue, false);
            Language.AddToken(TokenDescriptionName, TokenDescriptionValue, false);
        }

        private static void AddCustomRecipe(int recipediffculty)
        {
            var recipe = ScriptableObject.CreateInstance<Recipe>();

            recipe.m_item = AssetHelper.TotemEPrefab.GetComponent<ItemDrop>();

            recipe.m_minStationLevel = 2;
            

            var neededResources1 = new List<Piece.Requirement>
            {
                MockRequirement.Create("TrophyDeer", 3),
                MockRequirement.Create("TrophyEikthyr", 2),
                MockRequirement.Create("FineWood", 10),
                
            };
            var neededResources2 = new List<Piece.Requirement>
            {
                MockRequirement.Create("TrophyDeer", 6),
                MockRequirement.Create("TrophyEikthyr", 4),
                MockRequirement.Create("FineWood", 10),
                MockRequirement.Create("GoblinTotem", 1),
            };

            if (recipediffculty == 1)
            {
                recipe.m_resources = neededResources1.ToArray();
            }
            if (recipediffculty == 2)
            {
                recipe.m_resources = neededResources2.ToArray();
            }
            recipe.m_craftingStation = Mock<CraftingStation>.Create("piece_workbench");

           

            CustomRecipe = new CustomRecipe(recipe, true, true);
            ObjectDBHelper.Add(CustomRecipe);
        }

        private static void AddCustomItem()
        {
            CustomItem = new CustomItem(AssetHelper.TotemEPrefab, true);                      
            
            ObjectDBHelper.Add(CustomItem);
            
        }
    }
}


