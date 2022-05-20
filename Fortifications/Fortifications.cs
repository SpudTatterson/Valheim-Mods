using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;





namespace Fortifications
{
    public static class PluginInfo
    {
        public const string Guid = "Spuds.SpudsFortifications";
        public const string Name = "SpudsFortifications";
        public const string Ver = "1.0.0";
    }

    [BepInPlugin(PluginInfo.Guid, PluginInfo.Name, PluginInfo.Ver)]
    [BepInDependency(ValheimLib.ValheimLib.ModGuid)]

    public class Fortifications : BaseUnityPlugin
    {

        internal static Fortifications Instance { get; private set; }
        public void Awake()
        {

            SpikeWall.Init();
            //GrindStonePiece.Init();

            var harmony = new Harmony(PluginInfo.Guid);
            harmony.PatchAll();


            Instance = this;

        }
        public void Update()
        {
           
        }
       /* public static class Patches
        {
            [HarmonyPrefix]
            [HarmonyPatch(typeof(Aoe), "OnTriggerEnter")]
            public static void Aoe_OnTriggerEnter()
            {
                Console.instance.Print("works");
            }
        }*/
    }
}