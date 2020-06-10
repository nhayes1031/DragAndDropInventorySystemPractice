using UnityEngine;

public class Hotbar : MonoBehaviour
{
    private Inventory _inventory;
    private Slot[] _slots;
    private Player _player;

    private void OnEnable()
    {
        _inventory = FindObjectOfType<Inventory>();
        _inventory.ItemPickedUp += ItemPickedUp;
        _slots = GetComponentsInChildren<Slot>();
        _player = FindObjectOfType<Player>();
        PlayerInput.Instance.HotKeyPressed += HotKeyPressed;
    }

    private void OnDisable()
    {
        _inventory.ItemPickedUp -= ItemPickedUp;
        PlayerInput.Instance.HotKeyPressed -= HotKeyPressed;
    }

    private void HotKeyPressed(int index)
    {
        if (index >= _slots.Length || index < 0) 
            return;

        if (!_slots[index].IsEmpty)
        {
            _inventory.Equip(_slots[index].Item);
        }
    }

    private void ItemPickedUp(Item item)
    {
        Slot slot = FindNextOpenSlot();
        if (slot != null)
        {
            slot.SetItem(item);
        }
    }

    private Slot FindNextOpenSlot()
    {
        foreach (var slot in _slots)
        {
            if (slot.IsEmpty)
                return slot;
        }
        return null;
    }
}
