using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public PlayerMovement movement;
    public Rigidbody2D hat;
    public float hatForce;
    public float hatTorque;

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
        hat.transform.parent = null;
        hat.simulated = true;
        Vector2 dir = Vector2.up;
        dir.x += Random.Range(-0.1f, 0.1f);
        hat.AddForce(dir * hatForce, ForceMode2D.Impulse);
        hat.AddTorque(dir.x > 0 ? hatTorque : -hatTorque);
        hat.gameObject.layer = default;

        yield return new WaitForSeconds(expiryDelay);

        Destroy(gameObject);

        OnMonkeyExpired?.Invoke();

        yield return null;
    }

    
}
