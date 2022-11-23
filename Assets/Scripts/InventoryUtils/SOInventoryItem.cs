using UnityEngine;

[CreateAssetMenu(fileName = "Item",menuName = "Inventory/Item",order = 0)]
public class SOInventoryItem : ScriptableObject
{
    public Sprite Icon;
    public GameObject ItemPrefab;
    [TextArea(2, 5)]
    public string Description;

    public virtual bool Use()
    {
        //to not get destroyed return false;
        return false;
    }
}
