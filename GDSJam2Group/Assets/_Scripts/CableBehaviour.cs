using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableBehaviour : MonoBehaviour
{
    public bool electrified;
    public bool alternatesOnOff;
    public float delayBetweenAlternating;
    public float delayBeforeStart;
    CableLineRenderer cableVisual;

    public Material cableMaterial;
    public Material electrifiedCableMaterial;
    
    private void Start()
    {
        cableVisual = GetComponent<CableLineRenderer>();
        if (electrified) { Electrify(); }
        if (alternatesOnOff) { BeginAlternatingElectricity(); }
    }



    public void ToggleElectricity()
    {
        if (electrified) { Delectrify(); }
        else { Electrify(); }
    }



    /* We'll want to define what happens to the Monkey when they touch the electric cable, tho maybe in another script? On Collision? */
    public void Electrify()
    {
        electrified = true;
        cableVisual.lineRenderer.material = electrifiedCableMaterial;
        cableVisual.Electrify();
    }



    public void Delectrify()
    {
        electrified = false;
        cableVisual.lineRenderer.material = cableMaterial;
        cableVisual.Delectrify();
    }



    public void BeginAlternatingElectricity()
    {
        StopAllCoroutines();
        alternatesOnOff = true;
        StartCoroutine(ElectricTick());
    }



    public void EndAlternatingElectricity()
    {
        alternatesOnOff = false;
        StopAllCoroutines();
    }



    IEnumerator ElectricTick()
    {
        yield return new WaitForSeconds(delayBeforeStart);

        while (alternatesOnOff)
        {
            ToggleElectricity();
            yield return new WaitForSeconds(delayBetweenAlternating);
        }
    }
}
