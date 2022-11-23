using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionZoneTrigger : MonoBehaviour, ITrigger
{
    [SerializeField]
    private GameObject _interactionEffectPrefab;
    [SerializeField]
    private List<Interactable> _interactions;

    private GameObject _interactionVFX;

    public void Activate(GameObject obj)
    {
        if (!obj.GetComponent<PlayerMovement>())
            return;

        if (_interactionEffectPrefab)
        {
            if (!_interactionVFX)
                _interactionVFX = Instantiate(_interactionEffectPrefab, transform.position + new Vector3(0, 0, -1), Quaternion.identity, transform);
            else
                _interactionVFX.SetActive(true);
        }
        StartCoroutine(WaitForInputCoroutine(obj));
    }

    public void Deactivate(GameObject obj)
    {
        StopAllCoroutines();
        if(_interactionVFX)
            _interactionVFX.SetActive(false);
    }

    private IEnumerator WaitForInputCoroutine(GameObject obj)
    {
        while(true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                foreach (var interaction in _interactions)
                    interaction.Interact(obj);
                if (_interactionVFX)
                    Destroy(_interactionVFX);
                yield break;
            }

            yield return null;
        }
    }
}
