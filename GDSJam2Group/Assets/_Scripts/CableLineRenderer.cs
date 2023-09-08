using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableLineRenderer : MonoBehaviour
{
    [SerializeField]
    [Range(2, 20)]
    private int cableLengthHidden;
    private int previousLength;
    
    public int CableLength
    {
        get { return cableLengthHidden; }
        set 
        { 
            if (cableLengthHidden != previousLength)
            {
                cableLengthHidden = value;
                RefreshCableRenderer();
            }
        }
    }
    public GameObject jointPrefab;

    [Header("Assigned at Runtime:")]
    public List<Transform> joints;
    public LineRenderer lineRenderer;

    Vector3 offset = new Vector3(0, -0.5f, 0);
    bool readyToRender;



    private void Start()
    {
        previousLength = cableLengthHidden;
        lineRenderer = GetComponent<LineRenderer>();

        for (int i = 0; i < cableLengthHidden; i++)
        {
            if (i >= 2)
            {
                GameObject joint = Instantiate(jointPrefab, joints[(i - 1)].position + offset, Quaternion.identity, transform);
                joints.Add(joint.transform);

                joints[(joints.Count - 2)].GetComponent<HingeJoint2D>().connectedBody = joint.GetComponent<Rigidbody2D>();
            }

            joints[i].GetComponent<SpriteRenderer>().enabled = false;
        }

        joints[(joints.Count - 1)].GetComponent<HingeJoint2D>().enabled = false;
        lineRenderer.positionCount = joints.Count;
        readyToRender = true;
    }



    void RefreshCableRenderer()
    {
        readyToRender = false;

        int difference = cableLengthHidden - previousLength;

        if (difference > 0)
        {
            for (int i = 0; i < difference; i++)
            {
                GameObject joint = Instantiate(jointPrefab, joints[(joints.Count - 1)].position + offset, Quaternion.identity, transform);
                joints.Add(joint.transform);
                joint.GetComponent<SpriteRenderer>().enabled = false;

                joints[(joints.Count - 2)].GetComponent<HingeJoint2D>().enabled = true;
                joints[(joints.Count - 2)].GetComponent<HingeJoint2D>().connectedBody = joint.GetComponent<Rigidbody2D>();
            }
        }
        else
        {
            for (int i = 0; i > difference; i--)
            {
                GameObject joint = joints[(joints.Count -1)].gameObject;
                joints.RemoveAt((joints.Count - 1));
                Destroy(joint);

                joints[(joints.Count - 1)].GetComponent<HingeJoint2D>().connectedBody = null;
            }
        }

        joints[(joints.Count - 1)].GetComponent<HingeJoint2D>().enabled = false;
        previousLength = cableLengthHidden;
        lineRenderer.positionCount = joints.Count;
        readyToRender = true;
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
        // for now this is on Update but we'll wanna move it elsewhere soon!
        if (readyToRender) { RenderCable(); }
        CableLength = cableLengthHidden;
    }
}
