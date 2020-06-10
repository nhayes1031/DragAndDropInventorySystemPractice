using NUnit.Framework;

namespace inventory_panel
{
    public class inventory_selection_with_slot_selected
    {
        [Test]
        public void clicking_slot_selects_item()
        {
            var inventoryPanel = inventory_helpers.GetInventoryPanelWithItems(1);
            var slot = inventoryPanel.Slots[0];
            slot.OnPointerClick(null);
            Assert.AreSame(slot, inventoryPanel.Selected);
        }

        [Test]
        public void clicking_empty_slot_does_not_select_slot()
        {
            var inventoryPanel = inventory_helpers.GetInventoryPanelWithItems(0);
            var slot = inventoryPanel.Slots[0];
            slot.OnPointerClick(null);
            Assert.IsNull(inventoryPanel.Selected);
        }
    }
}
