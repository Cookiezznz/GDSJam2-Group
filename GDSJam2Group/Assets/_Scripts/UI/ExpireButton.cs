using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpireButton : MonoBehaviour
{
    public float holdDurationToExpire;
    private float timer;
    private bool isHeld;
    public static event Action OnForceExpire;

    public Image progress;

    void OnEnable()
    {
        InputManager.onExpire += ToggleExpire;
    }
    
    void OnDisable()
    {
        InputManager.onExpire -= ToggleExpire;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isHeld) return;

        //Increment timer
        timer += Time.deltaTime;

        UpdateFill();

        //If timer is finished.
        if (timer >= holdDurationToExpire)
        {
            OnForceExpire?.Invoke();
            EndExpire();
        }
    }

    public void ToggleExpire(bool toggle)
    {
        isHeld = toggle;
        if (!isHeld)
        {
            timer = 0;
            UpdateFill();
        }
    }

    public void EndExpire()
    {
        isHeld = false;
        timer = 0;
        UpdateFill();
    }

    void UpdateFill()
    {
        progress.fillAmount = timer / holdDurationToExpire;
    }
}
