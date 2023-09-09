using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fabricator : MonoBehaviour
{

    [SerializeField]
    GameObject banana;

    [SerializeField]
    Transform output;

    GameObject reward;

    public static event Action OnFabricated;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Checks if the inputed object is scrap - if so destroys the scrap and attempts to dispense refined metal
        Prop prop = collision.GetComponent<Prop>();
        if (prop.prop == Prop.Props.Refined)
        {
            Destroy(collision.gameObject);
            SpawnReward();
        }
    }

    private void SpawnReward()
    {
        // Checks that there are no other instances of refined metal before despensing then spawns a new refined metal.
        if(reward is null)
        {
            reward = Instantiate(banana, output.position, output.rotation);
            OnFabricated?.Invoke();
        }
        else
        {
            Debug.Log("Tried to dispense refined metal, but some already exists!");
        }
    }
}
