using Backpack.Util;
using UnityEngine;

namespace Item.Backpack.Logic
{
    class ItemLogic
    {
        public static Container container = AssetHelper.BackpackPrefab.GetComponent<Container>();
        void Awake()
    {
        
            
    }
        
        public static GameObject Open()
        {
           return container.m_open;
        }
}
}
