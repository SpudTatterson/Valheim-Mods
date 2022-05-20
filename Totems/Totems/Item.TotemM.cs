using System.Collections.Generic;
using Totem.Util;
using UnityEngine;
using ValheimLib;
using ValheimLib.ODB;

namespace Totem.Items.TotemE
{
    public static class ItemDataM
    {
        public static CustomItem CustomItem;
        public static CustomRecipe CustomRecipe;

        public const string TokenName = "$custom_item_Mtotem";
        public const string TokenValue = "Moder Totem";

        public const string TokenDescriptionName = "$custom_item_mtotem_description";
        public const string TokenDescriptionValue = "Moder Totem gives you Moder's abilty as a passive";

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

            recipe.m_item = AssetHelper.TotemMPrefab.GetComponent<ItemDrop>();

            recipe.m_minStationLevel = 3;

            var neededResources1 = new List<Piece.Requirement>
            {
                MockRequirement.Create("DragonEgg", 3),
                MockRequirement.Create("TrophyDragonQueen", 2),
                MockRequirement.Create("FineWood", 10),
                
            };
            var neededResources2 = new List<Piece.Requirement>
            {
                MockRequirement.Create("DragonEgg", 6),
                MockRequirement.Create("TrophyDragonQueen", 3),
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
            CustomItem = new CustomItem(AssetHelper.TotemMPrefab, true);                      

            ObjectDBHelper.Add(CustomItem);
            
        }
    }
}


