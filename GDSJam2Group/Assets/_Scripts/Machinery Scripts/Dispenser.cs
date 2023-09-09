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
        Fabricator.OnFabricated += SpawnScrap;
        PickupRespawner.ScrapRespawn += SpawnScrap;
    }

    private void OnDisable()
    {
        Fabricator.OnFabricated -= SpawnScrap;
        PickupRespawner.ScrapRespawn -= SpawnScrap;
    }

    private void Start()
    {
        SpawnScrap();
    }

    public void SpawnScrap()
    {
        currentScrap = Instantiate(scrap, output.position, output.rotation);
        OnDispensed?.Invoke();
    }


}
