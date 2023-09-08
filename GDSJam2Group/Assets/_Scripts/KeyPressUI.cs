using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public Color inactiveColour;

    //Event Subscriptions
    void OnEnable()
    {
        InputManager.onLeftUpper += ShowKeyLeftUpper;
        InputManager.onLeftLower += ShowKeyLeftLower;
        InputManager.onRightUpper += ShowKeyRightUpper;
        InputManager.onRightLower += ShowKeyRightLower;
        InputManager.onTail += ShowKeyTail;
    }

    void OnDisable()
    {
        InputManager.onLeftUpper -= ShowKeyLeftUpper;
        InputManager.onLeftLower -= ShowKeyLeftLower;
        InputManager.onRightUpper -= ShowKeyRightUpper;
        InputManager.onRightLower -= ShowKeyRightLower;
        InputManager.onTail -= ShowKeyTail;
    }

    public void ShowKeyLeftUpper(bool toggle)
    {
        if (toggle)
        {
            leftUpperImage.color = activeColour;
        }
        else
        {
            leftUpperImage.color = inactiveColour;
        }
    }

    public void ShowKeyLeftLower(bool toggle)
    {
        if (toggle)
        {
            leftLowerImage.color = activeColour;
        }
        else
        {
            leftLowerImage.color = inactiveColour;
        }
    }

    public void ShowKeyRightLower(bool toggle)
    {
        if (toggle)
        {
            rightLowerImage.color = activeColour;
        }
        else
        {
            rightLowerImage.color = inactiveColour;
        }
    }

    public void ShowKeyRightUpper(bool toggle)
    {
        if (toggle)
        {
            rightUpperImage.color = activeColour;
        }
        else
        {
            rightUpperImage.color = inactiveColour;
        }
    }

    public void ShowKeyTail(bool toggle)
    {
        if (toggle)
        {
            tailImage.color = activeColour;
        }
        else
        {
            tailImage.color = inactiveColour;
        }
    }
}
