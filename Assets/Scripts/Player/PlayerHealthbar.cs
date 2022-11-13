using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthbar : MonoBehaviour
{
    [SerializeField]
    private Slider _bar;
    
    private CharacterBase _character;

    private void Start()
    {
        _character = GetComponent<CharacterBase>();
        _bar.maxValue = _character.GetHealth();
        _bar.value = _character.GetHealth();
    }

    private void Update() => _bar.value = _character.GetHealth();
    
}
