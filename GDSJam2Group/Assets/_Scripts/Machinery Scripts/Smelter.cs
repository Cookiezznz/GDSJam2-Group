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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Checks if the inputed object is scrap - if so destroys the scrap and attempts to dispense refined metal
        if(collision.tag == "scrap")
        {
            Debug.Log("Scrap inputed into smelter.");
            GameObject.Destroy(collision.gameObject);
            SpawnRefined();
        }
    }

    public void SpawnRefined()
    {
        // Checks that there are no other instances of refined metal before despensing then spawns a new refined metal.
        if(currentRefined == null)
        {
            currentRefined = GameObject.Instantiate(refined, output.position, output.rotation);
            Debug.Log("Dispensed Refined Metal");
            InvokeSmelted();
        }
        else
        {
            Debug.Log("Tried to dispense refined metal, but some already exists!");
        }
        
    }

    public static void InvokeSmelted()
    {
        Debug.Log("Invoking OnSmelted event");
        OnSmelted?.Invoke();
    }


}
