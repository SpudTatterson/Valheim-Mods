using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using ValheimLib;
using ValheimLib.ODB;
using UnityObject = UnityEngine.Object;

namespace Totem.Util
{
    public static class AssetHelper
    {
       

        public const string AssetBundleName = "item_eikthyrtotem";
        public static AssetBundle TotemAssetBundle;

        public const string totemEPrefabPath = "Assets/Totem/TotemE.prefab";
        public static GameObject TotemEPrefab;

        public const string totemBPrefabPath = "Assets/Totem/TotemB.prefab";
        public static GameObject TotemBPrefab;

        public const string totemTPrefabPath = "Assets/Totem/TotemT.prefab";
        public static GameObject TotemTPrefab;

        public const string totemMPrefabPath = "Assets/Totem/TotemM.prefab";
        public static GameObject TotemMPrefab;

        public const string totemYPrefabPath = "Assets/Totem/TotemY.prefab";
        public static GameObject TotemYPrefab;

        public static void Init()
        {
        
            TotemAssetBundle = GetAssetBundleFromResources(AssetBundleName);
            TotemEPrefab = TotemAssetBundle.LoadAsset<GameObject>(totemEPrefabPath);            
            TotemBPrefab = TotemAssetBundle.LoadAsset<GameObject>(totemBPrefabPath);
            TotemTPrefab = TotemAssetBundle.LoadAsset<GameObject>(totemTPrefabPath);
            TotemMPrefab = TotemAssetBundle.LoadAsset<GameObject>(totemMPrefabPath);
            TotemYPrefab = TotemAssetBundle.LoadAsset<GameObject>(totemYPrefabPath);
        }

        
        public static AssetBundle GetAssetBundleFromResources(string fileName)
        {
            var execAssembly = Assembly.GetExecutingAssembly();

            var resourceName = execAssembly.GetManifestResourceNames()
                .Single(str => str.EndsWith(fileName));

            using var stream = execAssembly.GetManifestResourceStream(resourceName);

            return AssetBundle.LoadFromStream(stream);
        }
    }
}