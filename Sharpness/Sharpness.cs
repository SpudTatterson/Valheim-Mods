using BepInEx;
using HarmonyLib;
using Sharpness.Pieces;

namespace Sharpness
{
    public static class PluginInfo
    {
        public const string Guid = "Spuds.SpudsSharpness";
        public const string Name = "SpudsSharpness";
        public const string Ver = "1.0.2";
    }

    [BepInPlugin(PluginInfo.Guid, PluginInfo.Name, PluginInfo.Ver)]
    [BepInDependency(ValheimLib.ValheimLib.ModGuid)]

    public class Sharpness : BaseUnityPlugin
    {
        internal static Sharpness Instance { get; private set; }
        public void Awake()
        {
          
            GrindStonePiece.Init();

            var harmony = new Harmony(PluginInfo.Guid);
            harmony.PatchAll();


            Instance = this;

        }
    }
}
