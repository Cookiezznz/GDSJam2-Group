using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Dispenser : MonoBehaviour
{
    [SerializeField]
    Transform output;

    [SerializeField]
    GameObject scrap;

    GameObject currentScrap;

    public static event Action OnDispensed;

    private void OnEnable()
    {
        Fabricator.OnFabricated += Fabricated;
    }

    private void OnDisable()
    {
        Fabricator.OnFabricated -= Fabricated;
    }

    private void Start()
    {
        SpawnScrap();
    }

    private void Fabricated()
    {
        SpawnScrap();
    }

    public void SpawnScrap()
    {
        //just creates a new scrap gameobject if there is not already an existing one
        if(currentScrap == null)
        {
            currentScrap = GameObject.Instantiate(scrap, output.position, output.rotation);
            Debug.Log("Scrap Spawned");
            InvokeDispensed();
            
        }
        else
        {
            Debug.Log("Tried spawning scrap, but one already exists!");
        }
        
    }


    public static void InvokeDispensed()
    {
        Debug.Log("Dispenser Event called.");
        OnDispensed?.Invoke();
    }
}
