using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class KeyPressUI : MonoBehaviour
{
    private Camera camera;
    public Sprite activeSprite;
    public Sprite inactiveSprite;

    [Header("Targets")]
    //UI Images
    public GameObject leftUpperGO;
    public GameObject leftLowerGO;
    public GameObject rightUpperGO;
    public GameObject rightLowerGO;
    public GameObject tailGO;

    public Image leftUpperImage;
    public Image leftLowerImage;
    public Image rightUpperImage;
    public Image rightLowerImage;
    public Image tailImage;

    private RectTransform leftUpperRTransform;
    private RectTransform leftLowerRTransform;
    private RectTransform rightUpperRTransform;
    private RectTransform rightLowerRTransform;
    private RectTransform tailRTransform;

    public GameObject leftUpperAttachGO;
    public GameObject leftLowerAttachGO;
    public GameObject rightUpperAttachGO;
    public GameObject rightLowerAttachGO;
    public GameObject tailAttachGO;

    [Header("Follow Targets")]
    public RectTransform canvas;
    public Transform leftUpperTransform;
    public Transform leftLowerTransform;
    public Transform rightUpperTransform;
    public Transform rightLowerTransform;
    public Transform tailTransform;


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

        leftUpperRTransform = leftUpperGO.GetComponent<RectTransform>();
        leftLowerRTransform = leftLowerGO.GetComponent<RectTransform>();
        rightUpperRTransform = rightUpperGO.GetComponent<RectTransform>();
        rightLowerRTransform = rightLowerGO.GetComponent<RectTransform>();
        tailRTransform = tailGO.GetComponent<RectTransform>();

        PlayerController.OnMonkeyExpired += ClearAllKeys;
        MonkeyManager.OnMonkeySpawned += ReattachKeys;

        camera = Camera.main;

        leftUpperAttachGO.SetActive(false);
        leftLowerAttachGO.SetActive(false);
        rightUpperAttachGO.SetActive(false);
        rightLowerAttachGO.SetActive(false);
        tailAttachGO.SetActive(false);
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
        leftUpperImage.sprite = toggle ? activeSprite : inactiveSprite;
    }

    public void ShowKeyLeftLower(bool toggle)
    {
        leftLowerImage.sprite = toggle ? activeSprite : inactiveSprite;
    }

    public void ShowKeyRightLower(bool toggle)
    {
        
        rightLowerImage.sprite = toggle ? activeSprite : inactiveSprite;
    }

    public void ShowKeyRightUpper(bool toggle)
    {
        //if not attached, turn inactive.
        rightUpperImage.sprite = toggle ? activeSprite : inactiveSprite;
    }

    public void ShowKeyTail(bool toggle)
    {
        //if not attached, turn inactive
        tailImage.sprite = toggle ? activeSprite : inactiveSprite;
    }

    //Show Attachments
    public void LeftUpperAttached(bool toggle)
    {
        leftUpperAttachGO.SetActive(toggle);
    }
    public void LeftLowerAttached(bool toggle)
    {
        leftLowerAttachGO.SetActive(toggle);
    }
    public void RightUpperAttached(bool toggle)
    {
        rightUpperAttachGO.SetActive(toggle);
    }
    public void RightLowerAttached(bool toggle)
    {
        rightLowerAttachGO.SetActive(toggle);
    }
    public void TailAttached(bool toggle)
    {
        tailAttachGO.SetActive(toggle);
    }

    public void ClearAllKeys()
    {
        if (canvas)
        {
            leftUpperGO.SetActive(false);
            leftLowerGO.SetActive(false);
            rightUpperGO.SetActive(false);
            rightLowerGO.SetActive(false);
            tailGO.SetActive(false);
        }

        leftUpperImage.sprite = inactiveSprite;
        leftLowerImage.sprite = inactiveSprite;
        rightUpperImage.sprite = inactiveSprite;
        rightLowerImage.sprite = inactiveSprite;
        tailImage.sprite = inactiveSprite;
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
            leftUpperGO.SetActive(false);
            leftLowerGO.SetActive(false);
            rightUpperGO.SetActive(false);
            rightLowerGO.SetActive(false);
            tailGO.SetActive(false);
        }
    }
}
