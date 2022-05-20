
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;
using Backpack.Util;
using Item.Backpack.Logic;
using Unity.Collections;
using Backpack.Items.Backpack;




namespace Backpack
{
    public static class PluginInfo
    {
        public const string Guid = "Spuds.SpudsBackpacks";
        public const string Name = "SpudsBackpacks";
        public const string Ver = "1.0.0";
    }

    [BepInPlugin(PluginInfo.Guid, PluginInfo.Name, PluginInfo.Ver)]
    [BepInDependency(ValheimLib.ValheimLib.ModGuid)]

    public class Backpack : BaseUnityPlugin
    {

        internal static Backpack Instance { get; private set; }
        public void Awake()
        {
           
            AssetHelper.Init();

            ItemData.Init();

            Instance = this;
            Console.instance.Print("Backpacks loaded");
        }

        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.B))
            {
                //ItemLogic.Open();
            }
        }

    }



}

