using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerMovement movement;

    public bool monkeyExpired;
    [Tooltip("The time from when the Monkey expires, to when a new monkey is spawned.")]
    public float expiryDelay;
    public static event Action OnMonkeyExpired;

    // Start is called before the first frame update
    public void Collide(string tag)
    {
        if (monkeyExpired) return;

        if (tag == "Hazard")
        {
            StartCoroutine(MonkeyExpired());
        }
    }

    IEnumerator MonkeyExpired()
    {
        if (monkeyExpired) yield return null;
        Debug.LogWarning("Monkey Expired");
        monkeyExpired = true;

        yield return new WaitForSeconds(expiryDelay);

        Destroy(gameObject);

        OnMonkeyExpired?.Invoke();

        yield return null;
    }

    
}
