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
        if(collision.tag == "refined")
        {
            GameObject.Destroy(collision.gameObject);
            reward = GameObject.Instantiate(banana, output.position, output.rotation);
            InvokeFabricated();
        }
    }

    public static void InvokeFabricated()
    {
        Debug.Log("Invoking Fabricated Event.");
        OnFabricated?.Invoke();
    }
}
