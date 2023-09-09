using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField]
    bool respawnAsRefined;

    [SerializeField]
    Vector2 forceAppliedOnSpawn;

    Rigidbody2D rb;

    bool outOfBounds = false;


    public static event Action<bool> PickupDestroyed;

    private void OnEnable()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        LaunchProp();
    }

    public void DestroyPickup()
    {
        outOfBounds = true;
        GameObject.Destroy(gameObject);
    }

    private void OnDisable()
    {
        if (outOfBounds)
        {
            InvokePickupDestroyed(respawnAsRefined);
        }
        
    }

    public static void InvokePickupDestroyed(bool refined)
    {
        PickupDestroyed?.Invoke(refined);
    }

    public void LaunchProp()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(forceAppliedOnSpawn, ForceMode2D.Impulse);
    }

}
