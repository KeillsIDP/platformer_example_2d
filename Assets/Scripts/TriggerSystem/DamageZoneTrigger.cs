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
    [SerializeField]
    private bool _stuns = false;
    [SerializeField]
    private bool _enableInvulnerability = false;

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
        GameObject vfx = null;
        if (_damageEffect)
        {
            vfx = Instantiate(_damageEffect, character.transform);
            vfx.transform.localPosition = new Vector2(0, .8f);
        }

        while(_enteredCharacters.Contains(character)|| timePassed < _afterEffectTime)
        {
            if (!_enteredCharacters.Contains(character))
                timePassed += .5f;
            else if (timePassed > 0)
                break;

            character.DealDamage(_damagePerSecond/2, _enableInvulnerability, _stuns);
            yield return new WaitForSeconds(.5f);
        }

        if (vfx != null)
        {
            foreach (var svfx in vfx.GetComponentsInChildren<ParticleSystem>())
                svfx.Stop();

            Destroy(vfx, 0.5f);
        }
    }
}
