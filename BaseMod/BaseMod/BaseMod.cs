using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace BaseMod
{
    public static class PluginInfo
    {
        public const string Guid = "Spuds.BaseMod";
        public const string Name = "BaseMod";
        public const string Ver = "1.0.0";
    }
    [BepInPlugin(PluginInfo.Guid, PluginInfo.Name, PluginInfo.Ver)]

    public class BaseMod : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony(PluginInfo.Guid);
        void Awake()
        {
            harmony.PatchAll();
        }
    }
}
