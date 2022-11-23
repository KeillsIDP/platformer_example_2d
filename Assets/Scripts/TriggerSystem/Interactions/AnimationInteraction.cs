using UnityEngine;

[RequireComponent(typeof(Animation))]
public class AnimationInteraction : Interactable
{
    [SerializeField]
    private AnimationClip _clip;

    public override void Interact(GameObject obj)
    {
        if(GetComponent<Collider2D>())
            Destroy(GetComponent<Collider2D>());

        var anim = GetComponent<Animation>();
        anim.AddClip(_clip, "Interaction");
        anim.Play("Interaction");
    }
}
