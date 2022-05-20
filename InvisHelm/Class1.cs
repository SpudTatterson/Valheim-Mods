using System.IO;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;


namespace invishelm
{
	[BepInPlugin("Spuds.invishelm", "invishelm", "1.1.3")]
	public class invishelm : BaseUnityPlugin
	{
		public static ConfigEntry<bool> ConsoleComments;
		public static ConfigEntry<KeyCode> hidehelmet;
		public void Awake()
		{
			hidehelmet = Config.Bind<KeyCode>("General", "Hide Key", KeyCode.H, "The key is used to hide the helmet");
			ConsoleComments = Config.Bind<bool>("General", "Show in console", false, "Show in console if you are in hide or show mode");

			var harmony = new Harmony("Spuds.invishelm");
			harmony.PatchAll();
		}



		public static bool On = true;

		[HarmonyPatch]
		public static class Patches
		{
			[HarmonyPrefix]
			[HarmonyPatch(typeof(VisEquipment), "SetHelmetEquiped")]
			public static bool VisEquipment_SetHelmetEquiped(ref VisEquipment __instance, int hash, int hairHash)
			{
				if (hash == 0)
				{
					return true;
				}

				if (hash == 703889544)
				{

					return true;
				}
				//player.m_helmetItem.m_shared.m_helmetHideHair = false;
				if (Input.GetKeyDown(invishelm.hidehelmet.Value) && On == true)
				{

					if (__instance.m_helmetItemInstance)
					{
						UnityEngine.Object.Destroy(__instance.m_helmetItemInstance);
					}
					__instance.m_helmetItemInstance = __instance.AttachItem(hash, 0, __instance.m_helmet, true);

					On = false;
					if (ConsoleComments.Value)
					{
						Console.instance.Print("Show helmet mode");
					}

					return false;//

				}
				if (Input.GetKeyDown(invishelm.hidehelmet.Value) && On == false)
				{

					UnityEngine.Object.Destroy(__instance.m_helmetItemInstance);
					On = true;
					if (ConsoleComments.Value)
					{
						Console.instance.Print("Hide helmet mode");
					}

					return false;//
				}

				return false;//
			}
			[HarmonyPrefix]
			[HarmonyPatch(typeof(VisEquipment), "SetHairEquiped")]
			public static bool VisEquipment_SetHairEquiped(ref VisEquipment __instance, int hash)
			{

				if (hash == 703889544)
				{
					return true;
				}
				if (hash == 0)
				{
					return true;
				}

				if (Input.GetKeyDown(invishelm.hidehelmet.Value) && On == false)
				{

					UnityEngine.Object.Destroy(__instance.m_hairItemInstance);
					return true;
				}
				if (Input.GetKeyDown(invishelm.hidehelmet.Value) && On == true)
				{

					if (__instance.m_hairItemInstance)
					{
						UnityEngine.Object.Destroy(__instance.m_hairItemInstance);
					}
					__instance.m_hairItemInstance = __instance.AttachItem(hash, 0, __instance.m_helmet, true);
				}
				return true;
			}
		}
	}
}