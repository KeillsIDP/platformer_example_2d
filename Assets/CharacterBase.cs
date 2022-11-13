using System.Collections;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    [SerializeField]
    private float _maxHealth;
    [SerializeField]
    private float _invulnerabilityTime;

    private float _health;
    private bool _isInvulnerable;

    private void Start()
    {
        _health = _maxHealth;
    }

    public void DealDamage(float damage, bool useInvulnerability)
    {
        if (damage <= 0 || (_isInvulnerable && useInvulnerability))
            return;

        _health -= damage;

        if (_health <= 0)
            gameObject.SetActive(false);

        StartCoroutine(InvulnerabilityCoroutine());
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        _isInvulnerable = true;
        yield return new WaitForSeconds(_invulnerabilityTime);
        _isInvulnerable = false;
    }
}
