using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFace : MonoBehaviour
{
    public SpriteRenderer face;

    public Sprite[] faces;
    
    // Start is called before the first frame update
    void Start()
    {
        face.sprite = faces[Random.Range(0, faces.Length)];
    }

}
