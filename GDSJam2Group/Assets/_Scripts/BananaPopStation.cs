using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaPopStation : MonoBehaviour
{
    [SerializeField] private AudioClip bananaPopStation;
    [SerializeField] private AudioClip[] stationOptions;

    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    [SerializeField] private Transform monkeyTransform;

    [SerializeField] AudioSource[] radioSources;
    private AudioClip currentClip;

    // Start is called before the first frame update
    void Start()
    {
        currentClip = bananaPopStation;
        foreach (var source in radioSources)
        {
            source.PlayOneShot(currentClip);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if (monkeyTransform == null)
            monkeyTransform = GameObject.FindGameObjectWithTag("Player").transform;

        foreach (var source in radioSources)
        {
            if (!source.isPlaying)
            {
                if (currentClip == bananaPopStation)
                    currentClip = stationOptions[Random.Range(0, stationOptions.Length - 1)];

                else
                    currentClip = bananaPopStation;

                source.PlayOneShot(currentClip);
            }
        }
    }

    void OnValidate()
    {
        foreach (var source in radioSources)
        {
            source.minDistance = minDistance;
            source.maxDistance = maxDistance;
        }
    }
}
