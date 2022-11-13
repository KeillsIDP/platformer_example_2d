using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class DamageZoneTrigger : MonoBehaviour,ITrigger
{
    [SerializeField]
    private float _damagePerSecond;
    [SerializeField]
    private bool _damageEnemy;
    [SerializeField]
    private GameObject _damageEffect;
    [SerializeField]
    private float _afterEffectTime;

    private List<CharacterBase> _enteredCharacters = new List<CharacterBase>();

    public void Activate(GameObject obj)
    {
        var character = obj.GetComponent<CharacterBase>();
        if (!character)
            return;

        _enteredCharacters.Add(character);
        StartCoroutine(DamageCoroutine(character));
    }

    public void Deactivate(GameObject obj)
    {
        _enteredCharacters.Remove(obj.GetComponent<CharacterBase>());
    }

    private IEnumerator DamageCoroutine(CharacterBase character)
    {
        float timePassed = 0;
        while(_enteredCharacters.Contains(character) || timePassed < _afterEffectTime)
        {
            float damage = _damagePerSecond * Time.deltaTime;
            if (!_enteredCharacters.Contains(character))
                timePassed += Time.deltaTime;

            character.DealDamage(damage, false);
            yield return new WaitForEndOfFrame();
        }
    }
}
