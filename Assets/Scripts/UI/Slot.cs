using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] TMP_Text _text;

    public Item Item { get; private set; }
    public bool IsEmpty => Item == null;
    public Image IconImage => _iconImage;

    private void OnValidate()
    {
        _text = GetComponentInChildren<TMP_Text>();

        int hotKeyNumber = transform.GetSiblingIndex() + 1;
        _text.SetText(hotKeyNumber.ToString());
        gameObject.name = "Slot " + hotKeyNumber;
    }

    public void SetItem(Item item)
    {
        Item = item;
        _iconImage.sprite = item.Icon;
    }
}
