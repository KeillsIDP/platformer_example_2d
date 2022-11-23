using System.Collections;
using UnityEngine;

public class ItemPickUpInteraction : Interactable
{
    [SerializeField]
    private SOInventoryItem _inventoryItem;

    private static PlayerInventory _inventory;

    private void Start() => _inventory = FindObjectOfType<PlayerInventory>(); 

    public override void Interact(GameObject obj)
    {
        if (!_inventory.AddItem(_inventoryItem))
            return;

        Destroy(GetComponent<Collider2D>());
        StartCoroutine(MoveCoroutine(obj));
    }

    private IEnumerator MoveCoroutine(GameObject obj)
    {
        while (true)
        {
            var dest = obj.transform.position + Vector3.up;
            transform.position = Vector2.MoveTowards(transform.position, dest, Time.deltaTime * 5);
            yield return new WaitForEndOfFrame();

            if(Vector2.Distance(transform.position, dest) <0.05f)
            {
                Destroy(gameObject, 0.2f);
                yield break;
            }
        }
    }
}
