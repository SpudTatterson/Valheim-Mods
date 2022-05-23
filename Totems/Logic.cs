using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Totem.Util;
using BepInEx.Logging;
using System;



namespace Totem.logic
{
    class UpgradeController
    {
       // public StatusEffect[] Effects;
       // public ItemDrop itemDrop;

        public void Awake()
        {
           
        }

        public static int Upgradechecker()
        {

            //var effect = AssetHelper.TotemEPrefab.GetComponent<ItemDrop>().m_itemData.m_shared.m_equipStatusEffect;
            var player = Player.m_localPlayer;
            var item = player.m_inventory.GetItem(AssetHelper.TotemEPrefab.GetComponent<ItemDrop>().m_itemData.m_shared.m_name);
            if(item == null)
            {
                return 4;
            }
            //var item = player.m_inventory.GetItem(itemdrop.m_itemData.m_shared.m_name);
            
            
            return item.m_quality - 1;
        }




    }
}