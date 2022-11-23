using UnityEngine;

public class HealPotionInteraction : Interactable
{
    [SerializeField]
    private float _healAmount;

    public override void Interact(GameObject obj)
    {
        obj.GetComponent<CharacterBase>().Heal(_healAmount);
    }
}
