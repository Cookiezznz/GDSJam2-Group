using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropRespawner : MonoBehaviour
{
    [SerializeField]
    Transform respawnLocation;

    [SerializeField]
    Vector2 forceAppliedOnRespawn;

    Rigidbody2D rb;

    
    // Update is called once per frame
    void OnEnable(){
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    public void RespawnProp(){
        rb.velocity = Vector2.zero;
        transform.position = respawnLocation.position;
        transform.rotation = respawnLocation.rotation;
        rb.AddForce(forceAppliedOnRespawn, ForceMode2D.Impulse);
    }

}
