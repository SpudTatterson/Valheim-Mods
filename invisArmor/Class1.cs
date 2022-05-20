using System.IO;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;


namespace invisarmor
{
    [BepInPlugin("Spuds.invisarmor", "InvisArmor", "1.3.2")]
    public class Config : BaseUnityPlugin
    {

        public void Awake()
        {
            hidearmor = Config.Bind<KeyCode>("General", "Hide armor", KeyCode.H, "The key is used to hide the chestplate and cape");
            hidecape = Config.Bind<bool>("General", "Hide Cape", true, "Hides cape");
            hidechest = Config.Bind<bool>("General", "Hide Chestplate", true, "Hides Chestplate");
            hideutil = Config.Bind<bool>("General", "Hide Utility", true, "Hides Utility(belt)");
            hidepants = Config.Bind<bool>("General", "Hide Pants", false, "Hides Pants");

            var harmony = new Harmony("Spuds.InvisArmor");
            harmony.PatchAll();
        }



        // public static ConfigFile configFile = new ConfigFile(Path.Combine(Paths.ConfigPath, "InvisArmor.cfg"), true);
        public static ConfigEntry<KeyCode> hidearmor;
        public static ConfigEntry<bool> hidecape;
        public static ConfigEntry<bool> hidechest;
        public static ConfigEntry<bool> hideutil;
        public static ConfigEntry<bool> hidepants;


        //public static bool CapeOn = true;
        public static bool On = true;


        [HarmonyPatch]
        public static class Patches
        {
            [HarmonyPrefix]
            [HarmonyPatch(typeof(VisEquipment), "SetChestEquiped")]
            public static bool VisEquipment_SetChestEquiped(ref VisEquipment __instance, int hash)
            {

                if (hash == 0)
                {
                    return true;
                }

                if (Input.GetKeyDown(invisarmor.Config.hidearmor.Value) && On == true)
                {
                    if (hidechest.Value == true)
                    {
                        GameObject itemPrefab = ObjectDB.instance.GetItemPrefab(hash);
                        if (itemPrefab == null)
                        {
                            ZLog.Log("Missing chest item " + hash);
                            return true;
                        }
                        ItemDrop component = itemPrefab.GetComponent<ItemDrop>();
                        if (component.m_itemData.m_shared.m_armorMaterial)
                        {
                            __instance.m_bodyModel.material.SetTexture("_ChestTex", component.m_itemData.m_shared.m_armorMaterial.GetTexture("_ChestTex"));
                            __instance.m_bodyModel.material.SetTexture("_ChestBumpMap", component.m_itemData.m_shared.m_armorMaterial.GetTexture("_ChestBumpMap"));
                            __instance.m_bodyModel.material.SetTexture("_ChestMetal", component.m_itemData.m_shared.m_armorMaterial.GetTexture("_ChestMetal"));
                        }
                        __instance.m_chestItemInstances = __instance.AttachArmor(hash, -1);
                    }
                    On = false;
                    return true;
                }
                if (Input.GetKeyDown(invisarmor.Config.hidearmor.Value) && On == false)
                {
                    if (hidechest.Value == true)
                    {
                        foreach (GameObject gameObject in __instance.m_chestItemInstances)
                        {
                            if (__instance.m_lodGroup)
                            {
                                Utils.RemoveFromLodgroup(__instance.m_lodGroup, gameObject);
                            }
                            UnityEngine.Object.Destroy(gameObject);
                        }
                        __instance.m_bodyModel.material.SetTexture("_ChestTex", __instance.m_emptyBodyTexture);
                        __instance.m_bodyModel.material.SetTexture("_ChestBumpMap", null);
                        __instance.m_bodyModel.material.SetTexture("_ChestMetal", null);
                    }
                    On = true;
                    return false;
                }

                return !hidechest.Value;
            }
            [HarmonyPrefix]
            [HarmonyPatch(typeof(VisEquipment), "SetShoulderEquiped")]
            public static bool VisEquipment_SetShoulderEquiped(ref VisEquipment __instance, int hash, int variant)
            {

                if (hash == 0)
                {
                    return true;
                }
                if (hidecape.Value == true)
                {
                    if (Input.GetKeyDown(hidearmor.Value) && On == true)
                    {
                        foreach (GameObject gameObject in __instance.m_shoulderItemInstances)
                        {
                            if (__instance.m_lodGroup)
                            {
                                Utils.RemoveFromLodgroup(__instance.m_lodGroup, gameObject);
                            }
                            UnityEngine.Object.Destroy(gameObject);
                        }

                        return false;
                    }
                    if (Input.GetKeyDown(hidearmor.Value) && On == false)
                    {
                        __instance.m_shoulderItemInstances = __instance.AttachArmor(hash, variant);

                        return false;
                    }
                }
                return !hidecape.Value;
            }
            [HarmonyPrefix]
            [HarmonyPatch(typeof(VisEquipment), "SetUtilityEquiped")]
            public static bool VisEquipment_SetUtilityEquiped(ref VisEquipment __instance, int hash)
            {

                if (hash == 0)
                {

                    return true;
                }
                if (hideutil.Value == true)
                {
                    if (Input.GetKeyDown(hidearmor.Value) && On == true)
                    {
                        foreach (GameObject gameObject in __instance.m_utilityItemInstances)
                        {
                            if (__instance.m_lodGroup)
                            {
                                Utils.RemoveFromLodgroup(__instance.m_lodGroup, gameObject);
                            }
                            UnityEngine.Object.Destroy(gameObject);
                        }

                        return false;
                    }
                    if (Input.GetKeyDown(hidearmor.Value) && On == false)
                    {
                        __instance.m_utilityItemInstances = __instance.AttachArmor(hash, -1);

                        return false;
                    }
                }
                return !hideutil.Value;
            }
            [HarmonyPrefix]
            [HarmonyPatch(typeof(VisEquipment), "SetLegEquiped")]
            public static bool VisEquipment_SetLegEquiped(ref VisEquipment __instance, int hash)
            {
                if (hash == 0)
                {
                    return true;
                }
                if (hidepants.Value == true)
                {
                    if (Input.GetKeyDown(hidearmor.Value) && On == true)
                    {

                        foreach (GameObject obj in __instance.m_legItemInstances)
                        {
                            UnityEngine.Object.Destroy(obj);
                        }
                        __instance.m_legItemInstances = null;
                        __instance.m_bodyModel.material.SetTexture("_LegsTex", __instance.m_emptyBodyTexture);
                        __instance.m_bodyModel.material.SetTexture("_LegsBumpMap", null);
                        __instance.m_bodyModel.material.SetTexture("_LegsMetal", null);
                        return false;
                    }
                    if (Input.GetKeyDown(hidearmor.Value) && On == false)
                    {
                        GameObject itemPrefab = ObjectDB.instance.GetItemPrefab(hash);
                        if (itemPrefab == null)
                        {
                            ZLog.Log("Missing legs item " + hash);
                            return true;
                        }
                        ItemDrop component = itemPrefab.GetComponent<ItemDrop>();
                        if (component.m_itemData.m_shared.m_armorMaterial)
                        {
                            __instance.m_bodyModel.material.SetTexture("_LegsTex", component.m_itemData.m_shared.m_armorMaterial.GetTexture("_LegsTex"));
                            __instance.m_bodyModel.material.SetTexture("_LegsBumpMap", component.m_itemData.m_shared.m_armorMaterial.GetTexture("_LegsBumpMap"));
                            __instance.m_bodyModel.material.SetTexture("_LegsMetal", component.m_itemData.m_shared.m_armorMaterial.GetTexture("_LegsMetal"));
                        }
                        __instance.m_legItemInstances = __instance.AttachArmor(hash, -1);
                        return false;
                    }

                }
                return !hidepants.Value;
            }
        }



    }
}


