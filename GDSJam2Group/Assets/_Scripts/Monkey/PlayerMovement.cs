using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController controller;
    [Header("Stats")]
    public float limbMovementSpeed;
    public float bodyMovementSpeed;
    [Tooltip("The radius (units) from which limbs can grab objects.")]
    public float grabRadius;
    public bool toggleGravityOnActivate;
    public Vector2 spawnPipeForce;
    private Vector2 moveDirection;


    [Header("Limbs")] 
    public Rigidbody2D body2D;
    [Header("Left Upper")]
    public bool leftUpperActive;
    public bool leftUpperAttached;
    public Rigidbody2D leftUpper;
    public Rigidbody2D leftUpperHand;
    public static event Action<bool> OnLeftUpperAttached;
    public DistanceJoint2D LeftHandJointSocket;

    [Header("Left Lower")]
    public bool leftLowerActive;
    public bool leftLowerAttached;
    public Rigidbody2D leftLower;
    public Rigidbody2D leftLowerHand;
    public static event Action<bool> OnLeftLowerAttached;
    public DistanceJoint2D LeftFootJointSocket;

    [Header("Right Lower")]
    public bool rightLowerActive;
    public bool rightLowerAttached;
    public Rigidbody2D rightLower;
    public Rigidbody2D rightLowerHand;
    public static event Action<bool> OnRightLowerAttached;
    public DistanceJoint2D RightFootJointSocket;

    [Header("Right Upper")]
    public bool rightUpperActive;
    public bool rightUpperAttached;
    public Rigidbody2D rightUpper;
    public Rigidbody2D rightUpperHand;
    public static event Action<bool> OnRightUpperAttached;
    public DistanceJoint2D RightHandJointSocket;

    [Header("Tail")]
    public bool tailActive;
    public bool tailAttached;
    public Rigidbody2D tail;
    public Rigidbody2D tailHand;
    public static event Action<bool> OnTailAttached;
    public DistanceJoint2D TailJointSocket;


    //Event Subscriptions
    void OnEnable()
    {
        InputManager.onLeftUpper += ToggleLeftUpper;
        InputManager.onLeftLower += ToggleLeftLower;
        InputManager.onRightUpper += ToggleRightUpper;
        InputManager.onRightLower += ToggleRightLower;
        InputManager.onTail += ToggleTail;
        InputManager.OnLookUpdated += SetMoveDirection;
        InputManager.OnPointerUpdated += SetMoveDirection;
        InputManager.OnPrimaryPressed += AttachLimbs;
        InputManager.OnSecondaryPressed += DetachLimbs;
    }

    void OnDisable()
    {
        InputManager.onLeftUpper -= ToggleLeftUpper;
        InputManager.onLeftLower -= ToggleLeftLower;
        InputManager.onRightUpper -= ToggleRightUpper;
        InputManager.onRightLower -= ToggleRightLower;
        InputManager.onTail -= ToggleTail;
        InputManager.OnLookUpdated -= SetMoveDirection;
        InputManager.OnPointerUpdated -= SetMoveDirection;
        InputManager.OnPrimaryPressed -= AttachLimbs;
        InputManager.OnSecondaryPressed -= DetachLimbs;
    }

    void Start()
    {
        body2D.AddForce(Vector2.right * Random.Range(spawnPipeForce.x, spawnPipeForce.y), ForceMode2D.Impulse);
    }

    public void MovementUpdate()
    {
        MoveLimbs();
    }

    public void SetMoveDirection(Vector2 moveDir)
    {
        moveDirection = moveDir;
    }
    private void ToggleLeftUpper(bool toggle)
    {
        leftUpperActive = toggle;
        if(toggleGravityOnActivate)
            leftUpper.gravityScale = toggle ? 0 : 1;
    }
    private void ToggleLeftLower(bool toggle)
    {
        leftLowerActive = toggle;
        if (toggleGravityOnActivate)
            leftLower.gravityScale = toggle ? 0 : 1;
    }
    private void ToggleRightLower(bool toggle)
    {
        rightLowerActive = toggle;
        if (toggleGravityOnActivate)
            rightLower.gravityScale = toggle ? 0 : 1;
    }
    private void ToggleRightUpper(bool toggle)
    {
        rightUpperActive = toggle;
        if (toggleGravityOnActivate)
            rightUpper.gravityScale = toggle ? 0 : 1;
    }
    private void ToggleTail(bool toggle)
    {
        tailActive = toggle;
        if (toggleGravityOnActivate)
            tail.gravityScale = toggle ? 0 : 1;
    }



    private void MoveLimbs()
    {
        Vector2 limbMovement = limbMovementSpeed * Time.fixedDeltaTime * moveDirection.normalized;
        
        if (leftUpperActive && !leftUpperAttached)
        {
            leftUpperHand.MovePosition((Vector2)leftUpperHand.transform.position + limbMovement);
        }

        if (leftLowerActive && !leftLowerAttached)
        {
            leftLowerHand.MovePosition((Vector2)leftLowerHand.transform.position + limbMovement);
        }

        if (rightLowerActive && !rightLowerAttached)
        {
            rightLowerHand.MovePosition((Vector2)rightLowerHand.transform.position + limbMovement);
        }

        if (rightUpperActive && !rightUpperAttached)
        {
            rightUpperHand.MovePosition((Vector2)rightUpperHand.transform.position + limbMovement);
        }

        if (tailActive && !tailAttached)
        {
            tailHand.MovePosition((Vector2)tailHand.transform.position + limbMovement);
        }
        
        Vector2 bodyMovement = bodyMovementSpeed * Time.fixedDeltaTime * moveDirection.normalized;

        //If any limb is attached
        if (leftUpperAttached || leftLowerAttached 
            || rightUpperAttached || rightLowerAttached
            || tailAttached)
        {
            //if any limb is active, then move the body
            if (leftUpperActive || leftLowerActive
                || rightUpperActive || rightLowerActive
                || tailActive)
            {
                body2D.MovePosition((Vector2)transform.position + bodyMovement);
            }
        }
    }

    void AttachLimbs()
    {
        bool success = false;
        if (leftUpperActive && !leftUpperAttached)
        {
            if (TryGrabHand(leftUpperHand))
            {
                leftUpperAttached = true;
                OnLeftUpperAttached?.Invoke(true);
                success = true;
            }
        }

        if (leftLowerActive && !leftLowerAttached)
        {
            if (TryGrabHand(leftLowerHand))
            {
                leftLowerAttached = true;
                OnLeftLowerAttached?.Invoke(true);
                success = true;
            }


        }

        if (rightLowerActive && !rightLowerAttached)
        {
            if (TryGrabHand(rightLowerHand))
            {
                rightLowerAttached = true;
                OnRightLowerAttached?.Invoke(true);
                success = true;
            }
        }

        if (rightUpperActive && !rightUpperAttached)
        {
            if(TryGrabHand(rightUpperHand))
            {
                rightUpperAttached = true;
                OnRightUpperAttached?.Invoke(true);
                success = true;
            }
            
        }

        if (tailActive && !tailAttached)
        {
            if (TryGrabHand(tailHand))
            {
                tailAttached = true;
                OnTailAttached?.Invoke(true);
                success = true;
            }
        }

        if(success) 
            AudioManager.Instance.PlaySound("stick");

    }

    private bool TryGrabHand(Rigidbody2D hand)
    {
        List<RaycastHit2D> grabHits;
        
        grabHits = Physics2D.CircleCastAll(hand.transform.position, grabRadius,
            Vector2.zero, 0, LayerMask.GetMask("Grabable", "Grating")).ToList();
        
        foreach (var hit in grabHits)
        {
            //Test for hazards
            if (hit.transform.CompareTag("Hazard"))
            {
                //Grabbed a hazard, what happens?
                return true;
            }

            DistanceJoint2D socket = hit.transform.GetComponent<DistanceJoint2D>();
            //Else grabbable surface
            if (socket != null)
            {
                if (hit.transform.CompareTag("Prop"))
                {
                    Prop prop = hit.transform.GetComponent<Prop>();
                    switch (prop.prop)
                    {
                        case Prop.Props.Banana:
                            EatBanana(prop);
                            break;
                        case Prop.Props.Scrap:
                            AudioManager.Instance.PlaySound("pickupscrap");
                            break;
                        case Prop.Props.Refined:
                            AudioManager.Instance.PlaySound("pickuprefined");
                            break;

                    }
                }
                socket.enabled = true;
                socket.connectedBody = hand;

                if (hand == leftUpperHand)
                {
                    LeftHandJointSocket = socket;
                }
                if (hand == leftLowerHand)
                {
                    LeftFootJointSocket = socket;
                }
                if (hand == rightUpperHand)
                {
                    RightHandJointSocket = socket;
                }
                if (hand == rightLowerHand)
                {
                    RightFootJointSocket = socket;
                }
                if (hand == tailHand)
                {
                    TailJointSocket = socket;
                }
            }
            else
            {
                hand.constraints = RigidbodyConstraints2D.FreezeAll;
            }
            
            return true;
        }

        return false;
    }

    void DetachLimbs()
    {
        bool success = false;
        if (leftUpperActive)
        {
            leftUpperAttached = false;
            leftUpperHand.constraints = RigidbodyConstraints2D.None;
            OnLeftUpperAttached?.Invoke(false);
            RemoveSocket(LeftHandJointSocket);
            success = true;
        }

        if (leftLowerActive)
        {
            leftLowerAttached = false;
            leftLowerHand.constraints = RigidbodyConstraints2D.None;
            OnLeftLowerAttached?.Invoke(false);
            RemoveSocket(LeftFootJointSocket);
            success = true;

        }

        if (rightLowerActive)
        {
            rightLowerAttached = false;
            rightLowerHand.constraints = RigidbodyConstraints2D.None;
            OnRightLowerAttached?.Invoke(false);
            RemoveSocket(RightFootJointSocket);
            success = true;
        }

        if (rightUpperActive)
        {
            rightUpperAttached = false;
            rightUpperHand.constraints = RigidbodyConstraints2D.None;
            OnRightUpperAttached?.Invoke(false);
            RemoveSocket(RightHandJointSocket);
            success = true;
        }

        if (tailActive)
        {            
            tailAttached = false;
            tailHand.constraints = RigidbodyConstraints2D.None;
            OnTailAttached?.Invoke(false);
            RemoveSocket(TailJointSocket);
            success = true;
        }

        if(success)
            AudioManager.Instance.PlaySound("unstick");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        
        Gizmos.DrawWireSphere(leftUpper.transform.GetChild(0).position, grabRadius);
        Gizmos.DrawWireSphere(leftLower.transform.GetChild(0).position, grabRadius);
        Gizmos.DrawWireSphere(rightUpper.transform.GetChild(0).position, grabRadius);
        Gizmos.DrawWireSphere(rightLower.transform.GetChild(0).position, grabRadius);
        Gizmos.DrawWireSphere(tail.transform.GetChild(0).position, grabRadius);
    }


    void RemoveSocket(DistanceJoint2D socket)
    {
        if (socket != null)
        {
            socket.connectedBody = null;
            socket.enabled = false;
            socket = null;
        }
    }

    void EatBanana(Prop banana)
    {
        AudioManager.Instance.PlaySound("eat");
        Destroy(banana.gameObject);
    }
}
