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

    private AudioSource radioSource;
    private AudioClip currentClip;

    // Start is called before the first frame update
    void Start()
    {
        radioSource = GetComponent<AudioSource>();
        currentClip = bananaPopStation;
        radioSource.PlayOneShot(currentClip);
    }

    // Update is called once per frame
    void Update()
    {
        if (monkeyTransform == null)
            monkeyTransform = GameObject.FindGameObjectWithTag("Player").transform;

        float dist = Vector3.Distance(transform.position, monkeyTransform.position);

        if (dist < minDistance)
        {
            radioSource.volume = 1;
        }
        else if (dist > maxDistance)
        {
            radioSource.volume = 0;
        }
        else
        {
            radioSource.volume = 1 - ((dist - minDistance) / (maxDistance - minDistance));
        }


        if (!radioSource.isPlaying)
        {
            if (currentClip == bananaPopStation)
                currentClip = stationOptions[Random.Range(0, stationOptions.Length - 1)];

            else
                currentClip = bananaPopStation;

            radioSource.PlayOneShot(currentClip);
        }
    }
}
