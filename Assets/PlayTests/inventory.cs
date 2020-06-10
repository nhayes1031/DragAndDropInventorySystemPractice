using NUnit.Framework;
using UnityEngine;

namespace inventory
{
    public class inventory
    {
        // Add Items
        [Test]
        public void can_add_items()
        {
            Inventory inventory = new GameObject("INVENTORY").AddComponent<Inventory>();
            Item item = new GameObject("ITEM", typeof(SphereCollider)).AddComponent<Item>();
            inventory.Pickup(item);

            Assert.AreEqual(1, inventory.Count);
        }

        //Place into specific slot
        [Test]
        public void can_place_item_into_slot()
        {
            Inventory inventory = new GameObject("INVENTORY").AddComponent<Inventory>();
            Item item = new GameObject("ITEM", typeof(SphereCollider)).AddComponent<Item>();

            inventory.Pickup(item, 5);

            Assert.AreEqual(item, inventory.GetItemInSlot(5));
        }

        // Change Slots / Move
        [Test]
        public void can_move_to_empty_slot()
        {
            Inventory inventory = new GameObject("INVENTORY").AddComponent<Inventory>();
            Item item = new GameObject("ITEM", typeof(SphereCollider)).AddComponent<Item>();
            inventory.Pickup(item);

            Assert.AreEqual(item, inventory.GetItemInSlot(0));

            inventory.Move(0, 4);
            Assert.AreEqual(item, inventory.GetItemInSlot(4));
            Assert.IsNull(inventory.GetItemInSlot(0));
        }

        // Remove Items
        // Drop Items on Ground
        // Hotkey/Hotbar assignment
        // Change visuals
        // Modify stats
        // Persist & Load
    }
}
