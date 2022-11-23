using UnityEngine;

[CreateAssetMenu(fileName = "Item",menuName = "Inventory/Item",order = 0)]
public class SOInventoryItem : ScriptableObject
{
    public Sprite Icon;
    public GameObject ItemPrefab;
}
