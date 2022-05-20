using System.Collections.Generic;
using Totem.Util;
using UnityEngine;
using ValheimLib;
using ValheimLib.ODB;

namespace Totem.Items.TotemE
{
    public static class ItemDataT
    {
        public static CustomItem CustomItem;
        public static CustomRecipe CustomRecipe;

        public const string TokenName = "$custom_item_Ttotem";
        public const string TokenValue = "The Elder Totem";

        public const string TokenDescriptionName = "$custom_item_ttotem_description";
        public const string TokenDescriptionValue = "The Elder Totem gives you The Elder's abilty as a passive";

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

            recipe.m_item = AssetHelper.TotemTPrefab.GetComponent<ItemDrop>();

            recipe.m_minStationLevel = 3;

            var neededResources1 = new List<Piece.Requirement>
            {
                MockRequirement.Create("AncientSeed", 3),
                MockRequirement.Create("TrophyTheElder", 2),
                MockRequirement.Create("FineWood", 10),
                
            };
            var neededResources2 = new List<Piece.Requirement>
            {
                MockRequirement.Create("AncientSeed", 6),
                MockRequirement.Create("TrophyTheElder", 3),
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
            CustomItem = new CustomItem(AssetHelper.TotemTPrefab, true);                      

            ObjectDBHelper.Add(CustomItem);
            
        }
    }
}


