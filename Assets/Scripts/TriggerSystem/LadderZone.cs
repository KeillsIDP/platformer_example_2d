using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderZone : MonoBehaviour, ITrigger
{
    private List<CharacterBase> _charactersOnLadder = new List<CharacterBase>();

    public void Activate(GameObject obj)
    {
        _charactersOnLadder.Add(obj.GetComponent<CharacterBase>());

        var player = obj.GetComponent<PlayerMovement>();
        if (player && !player.IsOnLadder)
            StartCoroutine(PlayerCoroutine(obj));
        
    }

    public void Deactivate(GameObject obj)
    {
        _charactersOnLadder.Remove(obj.GetComponent<CharacterBase>());
    }

    private IEnumerator PlayerCoroutine(GameObject obj)
    {
        
        var character = obj.GetComponent<CharacterBase>();
        var rigid = obj.GetComponent<Rigidbody2D>();
        var player = obj.GetComponent<PlayerMovement>();

        while (_charactersOnLadder.Contains(character))
        {
            yield return new WaitForEndOfFrame();

            while (_charactersOnLadder.Contains(character))
            {
                if (Input.GetKeyDown(KeyCode.E) && !player.IsOnLadder)
                {
                    player.IsOnLadder = true;
                    rigid.constraints = RigidbodyConstraints2D.FreezeAll;
                    break;
                }

                yield return null; 
            }


            while (_charactersOnLadder.Contains(character))
            {
                var input = Input.GetAxisRaw("Vertical");
                if (input > 0)
                    GoUp(obj);
                else if (input < 0)
                    GoDown(obj);
                yield return null;

                if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.E))
                {
                    rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
                    player.IsOnLadder = false;
                    break;
                }
            }
        }
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        player.IsOnLadder = false;
    }

    private void GoUp(GameObject obj)
    {
        var startPos = obj.transform.position;
        obj.transform.position = Vector2.Lerp(startPos, startPos + Vector3.up, Time.deltaTime * 3);
    }

    private void GoDown(GameObject obj)
    {
        var startPos = obj.transform.position;
        obj.transform.position = Vector2.Lerp(startPos, startPos - Vector3.up, Time.deltaTime * 3);
    }
}
