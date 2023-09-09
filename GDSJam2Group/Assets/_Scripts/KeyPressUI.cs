using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class KeyPressUI : MonoBehaviour
{
    //UI Images
    public Image leftUpperImage;
    public Image leftLowerImage;
    public Image rightUpperImage;
    public Image rightLowerImage;
    public Image tailImage;

    public Transform leftUpperTransform;
    public Transform leftLowerTransform;
    public Transform rightUpperTransform;
    public Transform rightLowerTransform;
    public Transform tailTransform;

    private RectTransform leftUpperRTransform;
    private RectTransform leftLowerRTransform ;
    private RectTransform rightUpperRTransform;
    private RectTransform rightLowerRTransform;
    private RectTransform tailRTransform;

    private Camera camera;
    public RectTransform canvas;

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
        
        PlayerMovement.OnLeftUpperAttached += LeftUpperAttached;
        PlayerMovement.OnLeftLowerAttached += LeftLowerAttached;
        PlayerMovement.OnRightUpperAttached += RightUpperAttached;
        PlayerMovement.OnRightLowerAttached += RightLowerAttached;
        PlayerMovement.OnTailAttached += TailAttached;

        leftUpperRTransform = leftUpperImage.GetComponent<RectTransform>();
        leftLowerRTransform = leftLowerImage.GetComponent<RectTransform>();
        rightUpperRTransform = rightUpperImage.GetComponent<RectTransform>();
        rightLowerRTransform = rightLowerImage.GetComponent<RectTransform>();
        tailRTransform = tailImage.GetComponent<RectTransform>();

        PlayerController.OnMonkeyExpired += ClearAllKeys;
        MonkeyManager.OnMonkeySpawned += ReattachKeys;

        camera = Camera.main;
    }

    void OnDisable()
    {
        InputManager.onLeftUpper -= ShowKeyLeftUpper;
        InputManager.onLeftLower -= ShowKeyLeftLower;
        InputManager.onRightUpper -= ShowKeyRightUpper;
        InputManager.onRightLower -= ShowKeyRightLower;
        InputManager.onTail -= ShowKeyTail;
        PlayerController.OnMonkeyExpired -= ClearAllKeys;


        PlayerMovement.OnLeftUpperAttached -= LeftUpperAttached;
        PlayerMovement.OnLeftLowerAttached -= LeftLowerAttached;
        PlayerMovement.OnRightUpperAttached -= RightUpperAttached;
        PlayerMovement.OnRightLowerAttached -= RightLowerAttached;
        PlayerMovement.OnTailAttached -= TailAttached;

        MonkeyManager.OnMonkeySpawned += ReattachKeys;
    }

    public void Update()
    {
        if (!canvas || !camera) return;
        Vector2 screenPosition;
        if (leftUpperTransform)
        {
            screenPosition = camera.WorldToScreenPoint(leftUpperTransform.position);
            leftUpperRTransform.position = screenPosition;
        }

        if (leftLowerTransform)
        {
            screenPosition = camera.WorldToScreenPoint(leftLowerTransform.position);
            leftLowerRTransform.position = screenPosition;
        }

        if (rightUpperTransform)
        {
            screenPosition = camera.WorldToScreenPoint(rightUpperTransform.position);
            rightUpperRTransform.position = screenPosition;
        }

        if (rightLowerTransform)
        {
            screenPosition = camera.WorldToScreenPoint(rightLowerTransform.position);
            rightLowerRTransform.position = screenPosition;
        }

        if (tailTransform)
        {
            screenPosition = camera.WorldToScreenPoint(tailTransform.position);
            tailRTransform.position = screenPosition;
        }
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

    public void ClearAllKeys()
    {
        if (canvas)
        {
            leftUpperImage.gameObject.SetActive(false);
            leftLowerImage.gameObject.SetActive(false);
            rightUpperImage.gameObject.SetActive(false);
            rightLowerImage.gameObject.SetActive(false);
            tailImage.gameObject.SetActive(false);
        }

        leftUpperImage.color = inactiveColour;
        leftLowerImage.color = inactiveColour;
        rightUpperImage.color = inactiveColour;
        rightLowerImage.color = inactiveColour;
        tailImage.color = inactiveColour;
    }

    public void ReattachKeys(GameObject newMonkey)
    {
        PlayerMovement movement = newMonkey.GetComponent<PlayerMovement>();
        leftUpperTransform = movement.leftUpperHand.transform;
        leftLowerTransform = movement.leftLowerHand.transform;
        rightUpperTransform = movement.rightUpperHand.transform;
        rightLowerTransform = movement.rightLowerHand.transform;
        tailTransform = movement.tailHand.transform;

        if (canvas)
        {
            leftUpperImage.gameObject.SetActive(true);
            leftLowerImage.gameObject.SetActive(true);
            rightUpperImage.gameObject.SetActive(true);
            rightLowerImage.gameObject.SetActive(true);
            tailImage.gameObject.SetActive(true);
        }
    }
}
