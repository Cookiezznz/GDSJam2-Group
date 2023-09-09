using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Prop : MonoBehaviour
{
    [Serializable]
    public enum Props
    {
        Banana,
        Refined,
        Scrap,
    }

    public Props prop;
    [SerializeField]
    bool respawnAsRefined;
    public Vector2 forceRangeXOnSpawn;
    public Vector2 forceRangeYOnSpawn;

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
        PickupDestroyed?.Invoke(respawnAsRefined);
        Destroy(gameObject);
    }

    public void LaunchProp()
    {
        rb.velocity = Vector2.zero;
        Vector2 force = new Vector2(Random.Range(forceRangeXOnSpawn.x, forceRangeXOnSpawn.y),
            Random.Range(forceRangeYOnSpawn.x, forceRangeYOnSpawn.y));
        rb.AddForce(force, ForceMode2D.Impulse);
    }

}
