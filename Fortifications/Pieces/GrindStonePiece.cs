using Fortifications.Util;
using System;
using System.Collections.Generic;
using Unity;
using UnityEngine;
using ValheimLib;
using ValheimLib.ODB;


namespace Fortifications
{
    class GrindStonePiece 
    {
        internal static void Init()
        {

            ObjectDBHelper.OnAfterInit += AddPieceFromPrefab;
        }
          
          public static GameObject PiecePrefab = AssetBundleHelper.assetBundle.LoadAsset<GameObject>("Assets/CustomPieces/GrindStone.prefab");
        private static void AddPieceFromPrefab()
        {
           // AssetBundle assetBundle = AssetBundleHelper.GetFromResources("basedef");
           // GameObject PiecePrefab = assetBundle.LoadAsset<GameObject>("Assets/CustomPieces/GrindStone.prefab");
            PiecePrefab.FixReferences();
            //PiecePrefab.AddComponent<GrindStone>();
            GameObject cloned = PiecePrefab.InstantiateClone("GrindStone");

            GameObject hammerPrefab = Prefab.Cache.GetPrefab<GameObject>("_HammerPieceTable");
            PieceTable hammerTable = hammerPrefab.GetComponent<PieceTable>();

            
            var customRequirements = new List<Piece.Requirement>
            {
                MockRequirement.Create("RoundLog", 10),
                MockRequirement.Create("SharpeningStone", 1),
                MockRequirement.Create("Copper", 4)

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
