using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableLineRenderer : MonoBehaviour
{
    public List<Transform> joints;
    LineRenderer lineRenderer;


    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        foreach (var joint in joints)
        {
            joint.GetComponent<SpriteRenderer>().enabled = false;
        }
    }


    public void RenderCable()
    {
        for (int i = 0; i < joints.Count; i++)
        {
            lineRenderer.SetPosition(i, joints[i].position);
        }
    }


    private void Update()
    {
        // for now
        RenderCable();
    }
}
