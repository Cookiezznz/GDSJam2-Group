using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class KeyPressUI : MonoBehaviour
{
    //UI Images
    public Image leftUpperImage;
    public Image leftLowerImage;
    public Image rightUpperImage;
    public Image rightLowerImage;
    public Image tailImage;

    public Color activeColour;
    [FormerlySerializedAs("attachedcColour")] public Color attachedColour;
    public Color inactiveColour;

    //Event Subscriptions
    void OnEnable()
    {
        InputManager.onLeftUpper += ShowKeyLeftUpper;
        InputManager.onLeftLower += ShowKeyLeftLower;
        InputManager.onRightUpper += ShowKeyRightUpper;
        InputManager.onRightLower += ShowKeyRightLower;
        InputManager.onTail += ShowKeyTail;
        
        PlayerController.OnLeftUpperAttached += LeftUpperAttached;
        PlayerController.OnLeftLowerAttached += LeftLowerAttached;
        PlayerController.OnRightUpperAttached += RightUpperAttached;
        PlayerController.OnRightLowerAttached += RightLowerAttached;
        PlayerController.OnTailAttached += TailAttached;
    }

    void OnDisable()
    {
        InputManager.onLeftUpper -= ShowKeyLeftUpper;
        InputManager.onLeftLower -= ShowKeyLeftLower;
        InputManager.onRightUpper -= ShowKeyRightUpper;
        InputManager.onRightLower -= ShowKeyRightLower;
        InputManager.onTail -= ShowKeyTail;
        
        PlayerController.OnLeftUpperAttached -= LeftUpperAttached;
        PlayerController.OnLeftLowerAttached -= LeftLowerAttached;
        PlayerController.OnRightUpperAttached -= RightUpperAttached;
        PlayerController.OnRightLowerAttached -= RightLowerAttached;
        PlayerController.OnTailAttached -= TailAttached;
    }

    //Show Keys Activated
    public void ShowKeyLeftUpper(bool toggle)
    {
        //if not attached, turn inactive.
        if(leftUpperImage.color != attachedColour)
        leftUpperImage.color = toggle ? activeColour : inactiveColour;
    }

    public void ShowKeyLeftLower(bool toggle)
    {
        //if not attached, turn inactive.
        if(leftLowerImage.color != attachedColour)
        leftLowerImage.color = toggle ? activeColour : inactiveColour;
    }

    public void ShowKeyRightLower(bool toggle)
    {
        //if not attached, turn inactive.
        if(rightLowerImage.color != attachedColour)
        rightLowerImage.color = toggle ? activeColour : inactiveColour;
    }

    public void ShowKeyRightUpper(bool toggle)
    {
        //if not attached, turn inactive.
        if(rightUpperImage.color != attachedColour)
            rightUpperImage.color = toggle ? activeColour : inactiveColour;
    }

    public void ShowKeyTail(bool toggle)
    {
        //if not attached, turn inactive.
        if(tailImage.color != attachedColour)
            tailImage.color = toggle ? activeColour : inactiveColour;
    }

    //Show Attachments
    public void LeftUpperAttached(bool toggle)
    {
        //If detaching, revert to active or not
        leftUpperImage.color = toggle ? attachedColour : InputManager.Instance.leftUpper ? activeColour : inactiveColour;
    }
    public void LeftLowerAttached(bool toggle)
    {
        //If detaching, revert to active or not
        leftLowerImage.color = toggle ? attachedColour : InputManager.Instance.leftLower ? activeColour : inactiveColour;
    }
    public void RightUpperAttached(bool toggle)
    {
        //If detaching, revert to active or not
        rightUpperImage.color = toggle ? attachedColour : InputManager.Instance.rightUpper ? activeColour : inactiveColour;
    }
    public void RightLowerAttached(bool toggle)
    {
        //If detaching, revert to active or not
        rightLowerImage.color = toggle ? attachedColour : InputManager.Instance.rightLower ? activeColour : inactiveColour;
    }
    public void TailAttached(bool toggle)
    {
        //If detaching, revert to active or not
        tailImage.color = toggle ? attachedColour : InputManager.Instance.tail ? activeColour : inactiveColour;
    }
}
