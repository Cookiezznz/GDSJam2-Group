using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public PlayerMovement movement;
    public ParticleSystem bananasPS;
    public Rigidbody2D hat;
    public float hatForce;
    public float hatTorque;

    public bool monkeyExpired;
    [Tooltip("The time from when the Monkey expires, to when a new monkey is spawned.")]
    public float expiryDelay;
    public static event Action OnMonkeyExpired;

    void OnEnable()
    {
        ExpireButton.OnForceExpire += ForceExpire;
    }
    void OnDisable()
    {
        ExpireButton.OnForceExpire -= ForceExpire;
    }
    // Start is called before the first frame update
    public void Collide(string tag)
    {
        if (monkeyExpired) return;

        if (tag == "Hazard")
        {
            MonkeyExpire();
        }
    }

    void ForceExpire()
    {
        StartCoroutine(EMonkeyExpired(true));
    }
    
    void MonkeyExpire()
    {
        StartCoroutine(EMonkeyExpired(false));
    }

    IEnumerator EMonkeyExpired(bool skipTimer)
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

        bananasPS.transform.parent = null;
        bananasPS.Play();

        if(!skipTimer)
            yield return new WaitForSeconds(expiryDelay);

        Destroy(gameObject);

        OnMonkeyExpired?.Invoke();

        yield return null;
    }

    
}
