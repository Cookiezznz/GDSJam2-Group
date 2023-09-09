using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnCollision : MonoBehaviour
{
    public string[] collisionTags;

    public UnityEvent<string> onCollisionEnter;

    private void OnCollisionEnter2D(Collision2D other)
    {
        string hitTag = other.gameObject.tag;
        foreach (string tag in collisionTags)
        {
            if (tag == hitTag)
            {
                onCollisionEnter?.Invoke(tag);
            }
        }
        
    }
    
    public UnityEvent<string> onCollisionExit;

    private void OnCollisionExit2D(Collision2D other)
    {
        string hitTag = other.gameObject.tag;
        foreach (string tag in collisionTags)
        {
            if (tag == hitTag)
            {
                onCollisionExit?.Invoke(tag);
            }
        }
    }
}
