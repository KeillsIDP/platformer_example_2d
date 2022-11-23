using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _selector;
    [SerializeField]
    private Image _icon;

    public void Init(Sprite icon)
    {
        _icon.sprite = icon;
        var textRect = icon.textureRect;

        int mult = 0;

        if (textRect.width >= textRect.height)
            mult = 100 / (int)textRect.width;
        else
            mult = 100 / (int)textRect.height;

        _icon.rectTransform.sizeDelta = textRect.size * mult;
    }

    public void DeselectItem()
    {
        _selector.SetActive(false);
    }

    public void SelectItem()
    {
        _selector.SetActive(true);
    }
}
