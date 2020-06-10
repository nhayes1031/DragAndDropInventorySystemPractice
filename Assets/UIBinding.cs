using NSubstitute.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBinding : MonoBehaviour
{
    private IEnumerator Start()
    {
        var player = FindObjectOfType<Player>();
        while (player == null)
        {
            yield return null;
            player = FindObjectOfType<Player>();
        }

        GetComponent<UIInventoryPanel>().Bind(player.GetComponent<Inventory>());
    }
}
