using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smelter : MonoBehaviour
{
    [SerializeField]
    GameObject refined;

    [SerializeField]
    Transform output;

    GameObject currentRefined;

    public static event Action OnSmelted;

    private void OnEnable()
    {
        PickupRespawner.RefinedRespawn += SpawnRefined;
    }

    private void OnDisable()
    {
        PickupRespawner.RefinedRespawn -= SpawnRefined;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Checks if the inputed object is scrap - if so destroys the scrap and attempts to dispense refined metal
        Prop prop = collision.GetComponent<Prop>();
        if(prop.prop == Prop.Props.Scrap)
        {
            Destroy(collision.gameObject);
            SpawnRefined();
        }
    }

    public void SpawnRefined()
    {

        currentRefined = Instantiate(refined, output.position, output.rotation);
        OnSmelted?.Invoke();

        
    }


}
