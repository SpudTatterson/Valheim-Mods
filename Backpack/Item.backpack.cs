using System.Collections.Generic;
using Backpack.Util;
using UnityEngine;
using ValheimLib;
using ValheimLib.ODB;

namespace Backpack.Items.Backpack
{
    public static class ItemData
    {
        public static CustomItem CustomItem;
        public static CustomRecipe CustomRecipe;

        public const string TokenName = "$custom_item_Backpack";
        public const string TokenValue = "Backpack";

        public const string TokenDescriptionName = "$custom_item_Backpack_description";
        public const string TokenDescriptionValue = "Backpacks are used to increase inventory space";

        public const string CraftingStationPrefabName = "piece_workbench";

        internal static void Init()
        {
            AddCustomItem();
            AddCustomRecipe();          

            Language.AddToken(TokenName, TokenValue);
            Language.AddToken(TokenDescriptionName, TokenDescriptionValue);
        }

        private static void AddCustomRecipe()
        {
            var recipe = ScriptableObject.CreateInstance<Recipe>();

            recipe.m_item = AssetHelper.BackpackPrefab.GetComponent<ItemDrop>();

            var neededResources = new List<Piece.Requirement>
            {
                MockRequirement.Create("Ooze", 4),
                MockRequirement.Create("DeerHide", 5),
                MockRequirement.Create("FineWood", 5),
            };

            recipe.m_resources = neededResources.ToArray();

            CustomRecipe = new CustomRecipe(recipe, false, true);
            ObjectDBHelper.Add(CustomRecipe);
        }

        private static void AddCustomItem()
        {
            CustomItem = new CustomItem(AssetHelper.BackpackPrefab, true);
            
            ObjectDBHelper.Add(CustomItem);
        }
    }
}


