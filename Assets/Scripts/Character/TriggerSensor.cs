using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSensor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var trig = collision.gameObject.GetComponent<ITrigger>();
        if (trig != null)
            trig.Activate(gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var trig = other.gameObject.GetComponent<ITrigger>();
        if (trig != null)
            trig.Deactivate(gameObject);
    }
}
