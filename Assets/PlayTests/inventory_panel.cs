using NUnit.Framework;

namespace inventory_panel
{
    public class inventory_panel
    {
        [Test]
        public void has_25_slots()
        {
            var inventoryPanel = inventory_helpers.GetInventoryPanelWithItems(0);

            Assert.AreEqual(25, inventoryPanel.SlotCount);
        }

        [Test]
        public void bound_to_empty_inventory_has_all_slots_empty()
        {
            var inventoryPanel = inventory_helpers.GetInventoryPanelWithItems(0);
            var inventory = inventory_helpers.GetInventory();

            inventoryPanel.Bind(inventory);

            foreach (var slot in inventoryPanel.Slots)
            {
                Assert.IsTrue(slot.IsEmpty);
            }
        }

        [Test]
        public void bound_to_inventory_fills_slot_for_each_item([NUnit.Framework.Range(0, 25)] int numberOfItems)
        {
            var inventoryPanel = inventory_helpers.GetInventoryPanelWithItems(0);
            var inventory = inventory_helpers.GetInventory(numberOfItems);

            foreach (var slot in inventoryPanel.Slots)
            {
                Assert.IsTrue(slot.IsEmpty);
            }

            inventoryPanel.Bind(inventory);
            for (int i = 0; i < inventoryPanel.SlotCount; i++)
            {
                bool shouldBeEmpty = i >= numberOfItems;
                Assert.AreEqual(shouldBeEmpty, inventoryPanel.Slots[i].IsEmpty);
            }
        }

        [Test]
        public void places_item_in_slot_when_item_is_added_to_inventory()
        {
            var inventoryPanel = inventory_helpers.GetInventoryPanelWithItems(0);
            var inventory = inventory_helpers.GetInventory();
            var item = inventory_helpers.GetItem();

            inventoryPanel.Bind(inventory);
            Assert.IsTrue(inventoryPanel.Slots[0].IsEmpty);
            inventory.Pickup(item);
            Assert.IsFalse(inventoryPanel.Slots[0].IsEmpty);
        }

        [Test]
        public void should_have_empty_slots_when_bound_to_null_inventory()
        {
            var inventoryPanel = inventory_helpers.GetInventoryPanelWithItems(0);
            inventoryPanel.Bind(null);
            foreach (var slot in inventoryPanel.Slots)
            {
                Assert.IsTrue(slot.IsEmpty);
            }
        }

        [Test]
        public void bound_to_valid_inventory_then_bound_to_null_inventory_has_empty_slots()
        {
            var inventoryPanel = inventory_helpers.GetInventoryPanelWithItems(0);
            var inventory = inventory_helpers.GetInventory(1);

            inventoryPanel.Bind(inventory);
            Assert.IsFalse(inventoryPanel.Slots[0].IsEmpty);

            inventoryPanel.Bind(null);
            foreach (var slot in inventoryPanel.Slots)
            {
                Assert.IsTrue(slot.IsEmpty);
            }
        }

        [Test]
        public void updates_slots_when_items_are_moved()
        {
            var inventoryPanel = inventory_helpers.GetInventoryPanelWithItems(0);
            var inventory = inventory_helpers.GetInventory(1);
            inventoryPanel.Bind(inventory);

            inventory.Move(0, 4);
            Assert.AreSame(inventory.GetItemInSlot(4), inventoryPanel.Slots[4].Item);
        }
    }
}
