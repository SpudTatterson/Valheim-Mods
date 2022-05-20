using GrindStone.Util;
using System;
using System.Collections.Generic;
using Unity;
using UnityEngine;
using ValheimLib;
using ValheimLib.ODB;


namespace GrindStone.piece
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
           
            PiecePrefab.FixReferences();
            
            GameObject cloned = PiecePrefab.InstantiateClone("GrindStone");

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

            AssetBundleHelper.assetBundle.Unload(true);
        }
        
    
    }
}
