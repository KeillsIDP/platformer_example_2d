using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealZone : MonoBehaviour,ITrigger
{
    [SerializeField]
    private float _healPerSecond;

    [SerializeField]
    private bool _destroyable;
    [SerializeField]
    private float _lifeTime;

    private List<CharacterBase> _charactersInZone = new List<CharacterBase>();

    public void Activate(GameObject obj)
    {
        var charBase = obj.GetComponent<CharacterBase>();

        if (!charBase.GetComponent<PlayerMovement>())
            return;

        _charactersInZone.Add(charBase);
        StartCoroutine(HealCoroutine(charBase));
    }

    public void Deactivate(GameObject obj)
    {
        _charactersInZone.Remove(obj.GetComponent<CharacterBase>());
    }

    private IEnumerator HealCoroutine(CharacterBase character)
    {
        float timePassed = 0;

        while(_charactersInZone.Contains(character))
        {
            timePassed += 1f;
            character.Heal(_healPerSecond);

            if (_destroyable && timePassed > _lifeTime)
                break;

            yield return new WaitForSeconds(1f);
        }

        if (_destroyable)
            Destroy(gameObject);
    }
}
