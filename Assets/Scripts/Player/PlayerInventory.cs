using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private GameObject _uiHolder;
    [SerializeField]
    private GameObject _iconPrefab;
    [SerializeField]
    private GameObject _itemDescription;

    private List<SOInventoryItem> _items = new List<SOInventoryItem>();
    private List<ItemUI> _ui = new List<ItemUI>();

    private SOInventoryItem _choosenItem;
    private bool _isDescriptionEnabled;

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

        if (item == _choosenItem)
        {
            _choosenItem = null;
            _isDescriptionEnabled = false;
        }

        Destroy(_ui[index].gameObject);
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

        if (_choosenItem != _items[index])
            _isDescriptionEnabled = false;

        _choosenItem = _items[index];
        if (!_isDescriptionEnabled)
            StartCoroutine(ShowDescriptionCoroutine());
        else
            _isDescriptionEnabled = false;

        for (int i = 0; i < _items.Count; i++)
            if (i != index)
                _ui[i].DeselectItem();
            else
                _ui[i].SelectItem();
    }

    private IEnumerator ShowDescriptionCoroutine()
    {
        float timePassed = 0;
        _isDescriptionEnabled = true;

        _itemDescription.GetComponentInChildren<TextMeshProUGUI>().text = _choosenItem.Description;
        _itemDescription.SetActive(true);

        while (timePassed < 5 && _isDescriptionEnabled)
        {
            timePassed += Time.deltaTime;
            yield return null;
        }

        _itemDescription.SetActive(false);
        _isDescriptionEnabled = false;
    }

    public bool IsItemInInventory(SOInventoryItem item) => _items.Contains(item);
}
