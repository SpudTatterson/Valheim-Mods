using System.Linq;
using System.Reflection;
using UnityEngine;
using ValheimLib;
using ValheimLib.ODB;
using UnityObject = UnityEngine.Object;

namespace Backpack.Util
{
    public static class AssetHelper
    {
        public const string AssetBundleName = "item_backpack";
        public static AssetBundle BackpackAssetBundle;

        public const string BackpackPrefabPath = "Assets/Mock/backpack.prefab";
        public static GameObject BackpackPrefab;

        public static void Init()
        {
            BackpackAssetBundle = GetAssetBundleFromResources(AssetBundleName);
            BackpackPrefab = BackpackAssetBundle.LoadAsset<GameObject>(BackpackPrefabPath);
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