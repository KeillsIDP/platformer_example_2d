using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private GameObject _uiHolder;
    [SerializeField]
    private GameObject _iconPrefab;

    private List<SOInventoryItem> _items = new List<SOInventoryItem>();
    private List<ItemUI> _ui = new List<ItemUI>();

    private void Update()
    {
        SelectItem(KeyCode.Alpha1);
        SelectItem(KeyCode.Alpha2);
        SelectItem(KeyCode.Alpha3);
    }

    public bool AddItem(SOInventoryItem item)
    {
        if (_items.Count > 3)
            return false;

        var itemUI = Instantiate(_iconPrefab, _uiHolder.transform).GetComponent<ItemUI>();
        itemUI.Init(item.Icon);

        _items.Add(item);
        _ui.Add(itemUI);
        return true;
    }

    public GameObject DropItem(SOInventoryItem item)
    {
        if (!_items.Contains(item))
            return null;

        int index = _items.IndexOf(item);

        Destroy(_ui[index]);
        _ui.RemoveAt(index);

        _items.Remove(item);

        return Instantiate(item.ItemPrefab, transform.position, Quaternion.identity);
    }

    private void SelectItem(KeyCode key)
    {
        if (!Input.GetKeyDown(key))
            return;

        var index = (int)key - 49;
        if (_items.Count < index + 1)
            return;

        for (int i = 0; i < _items.Count; i++)
            if (i != index)
                _ui[i].DeselectItem();
            else
                _ui[i].SelectItem();
    }

    public bool IsItemInInventory(SOInventoryItem item) => _items.Contains(item);
}
