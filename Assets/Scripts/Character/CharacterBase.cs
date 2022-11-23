using System.Collections;
using TMPro;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    [SerializeField]
    private float _maxHealth;
    [SerializeField]
    private float _invulnerabilityTime;
    [SerializeField]
    private GameObject _damageIndicator;

    private static Transform _canvas;
    private float _health;
    private bool _isInvulnerable;
    private float _stunCooldown;

    private void Awake()
    {
        _canvas = FindObjectOfType<Canvas>().transform;
        _health = _maxHealth;
    }

    private void Update()
    {
        _stunCooldown += Time.deltaTime;
    }

    public void DealDamage(float damage, bool useInvulnerability, bool stun)
    {
        if (damage <= 0 || (_isInvulnerable && useInvulnerability))
            return;

        _health -= damage;

        var pos = new Vector3(Random.Range(-70, 70), Random.Range(50, 100));
        var damageIndicator = Instantiate(_damageIndicator, Camera.main.WorldToScreenPoint(transform.position) + pos, Quaternion.identity, _canvas);

        damageIndicator.GetComponentInChildren<TextMeshProUGUI>().text = "-" + damage.ToString("F0");
        Destroy(damageIndicator,1);

        if (_health <= 0)
            gameObject.SetActive(false);

        if (useInvulnerability)
            StartCoroutine(InvulnerabilityCoroutine());
        if (stun)
            StartCoroutine(StunCoroutine());
    }

    public float GetHealth() => _health;

    private IEnumerator InvulnerabilityCoroutine()
    {
        _isInvulnerable = true;
        yield return new WaitForSeconds(_invulnerabilityTime);
        _isInvulnerable = false;
    }

    private IEnumerator StunCoroutine()
    {
        if (_stunCooldown < 1)
            yield break;

        _stunCooldown = -.3f;
        float timePassed = 0;
        var rigidbody = GetComponent<Rigidbody2D>();

        if (!rigidbody)
            yield break;

        rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;

        while (true)
        {
            timePassed += Time.deltaTime;

            if (timePassed > .2f)
                break;

            yield return null;    
        }

        rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
