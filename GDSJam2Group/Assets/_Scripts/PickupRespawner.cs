using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupRespawner : MonoBehaviour
{

    public static event Action ScrapRespawn;
    public static event Action RefinedRespawn;


    private void OnEnable()
    {
        Pickup.PickupDestroyed += RespawnPickup;
    }

    private void OnDisable()
    {
        Pickup.PickupDestroyed += RespawnPickup;
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
            InvokeRefinedRespawn();
        }
        else
        {
            InvokeScrapRespawn();
        }
        
    }

    public static void InvokeScrapRespawn()
    {
        ScrapRespawn?.Invoke();
    }

    public static void InvokeRefinedRespawn()
    {
        RefinedRespawn?.Invoke();
    }


}

