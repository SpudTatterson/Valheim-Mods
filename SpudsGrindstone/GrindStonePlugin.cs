using BepInEx;
using HarmonyLib;
using GrindStone.piece;

namespace Grindstone
{
    public class SpudsGrindStone
    {
        public static class PluginInfo
        {
            public const string Guid = "Spuds.SpudsGrindstone";
            public const string Name = "SpudsGrindstone";
            public const string Ver = "1.0.0";
        }

         [BepInPlugin("Spuds.SpudsGrindstone", "SpudsGrindstone", "1.0.0")]        
         [BepInDependency(ValheimLib.ValheimLib.ModGuid)]

        public class GrindStonePlugin : BaseUnityPlugin
        {
            internal static GrindStonePlugin Instance { get; private set; }
            public void Awake()
            {
                
                GrindStonePiece.Init();

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
}
