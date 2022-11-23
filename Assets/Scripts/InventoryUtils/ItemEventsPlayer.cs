using UnityEngine;

public class ItemEventsPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    private void Start()
    {
        Debug.Log(_player);
    }

    public void HealPlayer(float amount) => _player.GetComponent<CharacterBase>().Heal(amount);
    
}
