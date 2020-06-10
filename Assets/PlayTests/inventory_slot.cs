using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace inventory_panel
{
    public class inventory_slot
    {
        [Test]
        public void when_item_is_set_changes_icon_to_match()
        {
            var inventoryPanel = inventory_helpers.GetInventoryPanelWithItems(0);
            var slot = inventoryPanel.Slots[0];
            var item = Substitute.For<IItem>();
            var sprite = Sprite.Create(Texture2D.redTexture, new Rect(0, 0, 4, 4), Vector2.zero);
            item.Icon.Returns(sprite);

            slot.SetItem(item);

            Assert.AreSame(sprite, item.Icon);
        }
    }
}
