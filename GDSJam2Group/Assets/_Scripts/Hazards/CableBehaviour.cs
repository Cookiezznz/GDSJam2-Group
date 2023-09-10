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

    public int[] activeStages;
    bool active;



    private void OnEnable()
    {
        GameStateManager.StageStarted += CheckForActivate;
    }

    private void OnDisable()
    {
        GameStateManager.StageStarted -= CheckForActivate;
    }




    void CheckForActivate(int currentStage)
    {
        foreach (var activeStage in activeStages)
        {
            ToggleActive(currentStage == activeStage);
        }
    }


    void ToggleActive(bool checkActiveStage)
    {
        if (checkActiveStage == active) return;
        active = checkActiveStage;

        if (active)
        {
            if (electrified) { Electrify(); }
            if (alternatesOnOff) { BeginAlternatingElectricity(); }
        }
        else
        {
            if (electrified) { Delectrify(); }
            if (alternatesOnOff) { EndAlternatingElectricity(); }
        }
    }



    private void Start()
    {
        cableVisual = GetComponent<CableLineRenderer>();
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
