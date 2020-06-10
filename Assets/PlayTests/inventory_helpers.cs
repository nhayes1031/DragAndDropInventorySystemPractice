using UnityEditor;
using UnityEngine;

namespace inventory_panel
{
    public static class inventory_helpers
    {
        public static Item GetItem()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Item>("Assets/Prefabs/Items/TestItem.prefab");
            return Object.Instantiate(prefab);
        }

        public static UISelectionCursor GetSelectionCursor()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<UISelectionCursor>("Assets/Prefabs/UI/SelectionCursor.prefab");
            return GameObject.Instantiate(prefab);
        }

        public static Inventory GetInventory(int numberOfItems = 0)
        {
            var inventory = new GameObject("Inventory").AddComponent<Inventory>();
            for (int i = 0; i < numberOfItems; i++)
            {
                var item = GetItem();
                inventory.Pickup(item);
            }
            return inventory;
        }

        public static UIInventoryPanel GetInventoryPanelWithItems(int numberOfItems)
        {
            var prefab = AssetDatabase.LoadAssetAtPath<UIInventoryPanel>("Assets/Prefabs/UI/InventoryPanel.prefab");
            var panel = GameObject.Instantiate(prefab);
            var inventory = GetInventory(numberOfItems);
            panel.Bind(inventory);
            return panel;
        }
    }
}
