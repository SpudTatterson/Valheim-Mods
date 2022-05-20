using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;
using Totem.Util;
using Totem.logic;
using Totem.Items.TotemE;
using Totem.Items.TotemB;
using ValheimLib.ODB;






namespace Totem
{
    public static class PluginInfo
    {
        public const string Guid = "Spuds.SpudsForsakenTotems";
        public const string Name = "SpudsSpudsForsakenTotems";
        public const string Ver = "2.2.1";
    }

    [BepInPlugin(PluginInfo.Guid, PluginInfo.Name, PluginInfo.Ver)]
    [BepInDependency(ValheimLib.ValheimLib.ModGuid)]

    public class Totems : BaseUnityPlugin
    {

        // public static ConfigFile configFile = new ConfigFile(Path.Combine(Paths.ConfigPath, "SpudsForsakenTotems.cfg"), true);
        public static ConfigEntry<int> craftingdif;
        //public static StatusEffect[] effects;


        internal static Totems Instance { get; private set; }
        public void Awake()
        {
            craftingdif = Config.Bind("General", "Crafting difficulty", 1, "Changes crafting difficulty 1 being the easiest." +
            " as of right now u can only use 1 or 2");

          
            AssetHelper.Init();

            //EffectContainer effect = AssetHelper.TotemEPrefab.GetComponent<EffectContainer>();
            //Console.instance.Print(effect.SharpnessEffects[0].m_name);

            ItemDataE.Init(craftingdif.Value);
            ItemDataB.Init(craftingdif.Value);
            ItemDataT.Init(craftingdif.Value);
            ItemDataM.Init(craftingdif.Value);
            ItemDataY.Init(craftingdif.Value);

            var harmony = new Harmony(PluginInfo.Guid);
            harmony.PatchAll();
           

            Instance = this;
            
        }
      
        public void Update()
        {
            if (InventoryGui.m_instance != null)
            {
                if (InventoryGui.m_instance.InUpradeTab() == true)
                {
                    UpgradeEffect();
                }
            }
        }
        public void UpgradeEffect()
        {
            var player = Player.m_localPlayer;
            var item = player.m_inventory.GetItem(AssetHelper.TotemEPrefab.GetComponent<ItemDrop>().m_itemData.m_shared.m_name);
            int lvl = UpgradeController.Upgradechecker();
            if (lvl == 4)
            {
                return;
            }
            item.m_shared.m_equipStatusEffect = AssetHelper.TotemEPrefab.GetComponent<EffectContainer>().SharpnessEffects[lvl];
            if (lvl == 3)
            {
                item.m_shared.m_movementModifier = 0.05f;
            }

            Console.instance.Print("upgraded effect");
        }
        [HarmonyPatch]
        public static class Patches
        {
            [HarmonyPrefix]
            [HarmonyPatch(typeof(Player), "StartGuardianPower")]
            public static bool Player_StartGuardianPower()
            {

                var player = Player.m_localPlayer;
                if ((ZInput.GetButtonDown("GPower") || ZInput.GetButtonDown("JoyGPower"))
                    && player.m_eqipmentStatusEffects.Contains(AssetHelper.TotemEPrefab.GetComponent<ItemDrop>().m_itemData.m_shared.m_equipStatusEffect)
                    || player.m_eqipmentStatusEffects.Contains(AssetHelper.TotemBPrefab.GetComponent<ItemDrop>().m_itemData.m_shared.m_equipStatusEffect)
                    || player.m_eqipmentStatusEffects.Contains(AssetHelper.TotemTPrefab.GetComponent<ItemDrop>().m_itemData.m_shared.m_equipStatusEffect)
                    || player.m_eqipmentStatusEffects.Contains(AssetHelper.TotemYPrefab.GetComponent<ItemDrop>().m_itemData.m_shared.m_equipStatusEffect)
                    || player.m_eqipmentStatusEffects.Contains(AssetHelper.TotemMPrefab.GetComponent<ItemDrop>().m_itemData.m_shared.m_equipStatusEffect))
                {
                    return false;
                }

                return true;
            }
            
        }
           
    }



}

