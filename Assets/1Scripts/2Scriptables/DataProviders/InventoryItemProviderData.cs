using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Silversong.Data.Providers
{
    [CreateAssetMenu(menuName = "Scriptable/Providers/InventoryItemsProvider")]

    public class InventoryItemProviderData : ScriptableObject
    {
        [SerializeField] private InventoryItem[] _items;

        [SerializeField] private InventoryTierSlot[] _itemsByTier;


        public InventoryItem GetItem(int id)
        {
            if (id > _items.Length - 1)
            {
                Debug.LogError("# Get InventoryItem - no InventoryItem with id " + id);
                return null;
            }

            return _items[id];
        }


        public List<InventoryItem> GetItemsByTier(int tier) // 1, 2 ,3 ,4
        {
            List<InventoryItem> result = new List<InventoryItem>();

            for(int i = 0; i < _itemsByTier[tier - 1].ItemsIds.Count; i ++)
            {
                result.Add(GetItem(_itemsByTier[tier - 1].ItemsIds[i]));
            }

            return result;
        }














        [ContextMenu("# Initialize")]
        private void Initialize()
        {
#if UNITY_EDITOR
            InventoryItem[] items = Resources.LoadAll<InventoryItem>("3Items");

            _itemsByTier = new InventoryTierSlot[4];

            _items = new InventoryItem[items.Length];

            for (int i = 0; i < items.Length; i++)
            {
                _items[i] = items[i];
                _items[i].Id = i;
                Sort(_items[i]);
            }           
#endif
        }

        private void Sort(InventoryItem item)
        {
            if(_itemsByTier[item.Tier - 1] == null)
            {
                _itemsByTier[item.Tier - 1] = new InventoryTierSlot();
            }

            if(_itemsByTier[item.Tier - 1].ItemsIds == null)
            {
                _itemsByTier[item.Tier - 1].ItemsIds = new List<int>();
            }

            _itemsByTier[item.Tier - 1].ItemsIds.Add(item.Id);
        }

    }

    [System.Serializable]
    public class InventoryTierSlot
    {
        public List<int> ItemsIds; 
    }
}