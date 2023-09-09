using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomHatColour : MonoBehaviour
{
    public SpriteRenderer hat;

    public Color[] hatColours;
    
    // Start is called before the first frame update
    void Start()
    {
        hat.color = hatColours[Random.Range(0, hatColours.Length)];
    }

}
