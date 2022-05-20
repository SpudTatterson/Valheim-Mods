using Fortifications.Util;
using System.Collections.Generic;
using UnityEngine;
using ValheimLib;
using ValheimLib.ODB;


namespace Fortifications
{
    class SpikeWall
    {
        internal static void Init()
        {
            
            ObjectDBHelper.OnAfterInit += AddPieceFromPrefab;
        }
        private static void AddPieceFromPrefab()
        {
            //AssetBundle assetBundle = AssetBundleHelper.GetFromResources("basedef");
            GameObject wallPrefab = AssetBundleHelper.assetBundle.LoadAsset<GameObject>("Assets/CustomPieces/SpikeWall.prefab");
            wallPrefab.FixReferences();
            GameObject cloned = wallPrefab.InstantiateClone("SpikeWall");

            GameObject hammerPrefab = Prefab.Cache.GetPrefab<GameObject>("_HammerPieceTable");
            PieceTable hammerTable = hammerPrefab.GetComponent<PieceTable>();

           
            var customRequirements = new List<Piece.Requirement>
            {
                MockRequirement.Create("RoundLog", 3),
                MockRequirement.Create("Wood", 5),
                
            };
            var custompiece = cloned.GetComponent<Piece>();
            custompiece.m_resources = customRequirements.ToArray();
            var CraftingStationReq = Mock<CraftingStation>.Create("piece_workbench");
            CraftingStationReq.FixReferences();
            custompiece.m_craftingStation = CraftingStationReq;

            custompiece.FixReferences();

            hammerTable.m_pieces.Add(cloned.gameObject);

            AssetBundleHelper.assetBundle.Unload(false);
        }

    
       
    }
}
