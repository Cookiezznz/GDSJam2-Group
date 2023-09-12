using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropRespawner : MonoBehaviour
{
    public static event Action ScrapRespawn;
    public static event Action RefinedRespawn;
    
    private void OnEnable()
    {
        Prop.PickupDestroyed += RespawnPickup;
    }

    private void OnDisable()
    {
        Prop.PickupDestroyed -= RespawnPickup;
    }
    
    public void RespawnPickup(bool respawnAsRefined)
    {
        StartCoroutine(SpawnDelay(respawnAsRefined));
    }

    IEnumerator SpawnDelay(bool respawnAsRefined)
    {
        yield return new WaitForEndOfFrame();
        if (respawnAsRefined)
        {
            RefinedRespawn?.Invoke();
        }
        else
        {
            ScrapRespawn?.Invoke();
        }

        yield return null;

    }
}

