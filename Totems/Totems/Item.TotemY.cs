using System.Collections.Generic;
using Totem.Util;
using UnityEngine;
using ValheimLib;
using ValheimLib.ODB;

namespace Totem.Items.TotemE
{
    public static class ItemDataY
    {
        public static CustomItem CustomItem;
        public static CustomRecipe CustomRecipe;

        public const string TokenName = "$custom_item_Ytotem";
        public const string TokenValue = "Yagluth Totem";

        public const string TokenDescriptionName = "$custom_item_ytotem_description";
        public const string TokenDescriptionValue = "Yagluth Totem gives you Yagluth's abilty as a passive";

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

            recipe.m_item = AssetHelper.TotemYPrefab.GetComponent<ItemDrop>();

            recipe.m_minStationLevel = 3;

            var neededResources1 = new List<Piece.Requirement>
            {
                MockRequirement.Create("GoblinTotem", 5),
                MockRequirement.Create("TrophyGoblinKing", 2),
                MockRequirement.Create("FineWood", 10),
                
            };
            var neededResources2 = new List<Piece.Requirement>
            {
                MockRequirement.Create("GoblinTotem", 11),
                MockRequirement.Create("TrophyGoblinKing", 3),
                MockRequirement.Create("FineWood", 10),
               // MockRequirement.Create("GoblinTotem", 1),
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
            CustomItem = new CustomItem(AssetHelper.TotemYPrefab, true);                      

            ObjectDBHelper.Add(CustomItem);
            
        }
    }
}


