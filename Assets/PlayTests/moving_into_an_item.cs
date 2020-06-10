﻿using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace a_player
{
    public class moving_into_an_item
    {
        private Player player;
        private Item item;

        [UnitySetUp]
        public IEnumerator Init()
        {
            PlayerInput.Instance = Substitute.For<IPlayerInput>();
            yield return Helpers.LoadItemsTestsScene();
            player = Helpers.GetPlayer();
            item = Object.FindObjectOfType<Item>();
        }

        [UnityTest]
        public IEnumerator picks_up_and_equips_item()
        {
            Assert.AreNotSame(item, player.GetComponent<Inventory>().ActiveItem);

            item.transform.position = player.transform.position;
            yield return new WaitForFixedUpdate();

            Assert.AreSame(item, player.GetComponent<Inventory>().ActiveItem);
        }

        [UnityTest]
        public IEnumerator changes_crosshair_to_item_crosshair()
        {
            Crosshair crosshair = Object.FindObjectOfType<Crosshair>();

            Assert.AreNotSame(item.CrosshairDefinition.Sprite, crosshair.GetComponent<Image>().sprite);

            item.transform.position = player.transform.position;
            yield return new WaitForFixedUpdate();

            Assert.AreEqual(item.CrosshairDefinition.Sprite, crosshair.GetComponent<Image>().sprite);
        }

        [UnityTest]
        public IEnumerator changes_slot_1_icon_to_match_item_icon()
        {
            Hotbar hotBar = Object.FindObjectOfType<Hotbar>();
            Slot slot1 = hotBar.GetComponentInChildren<Slot>();

            Assert.AreNotSame(item.Icon, slot1.IconImage.sprite);

            item.transform.position = player.transform.position;
            yield return new WaitForFixedUpdate();

            Assert.AreEqual(item.Icon, slot1.IconImage.sprite);
        }
    }
}
