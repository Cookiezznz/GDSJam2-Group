using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController controller;
    [Header("Stats")]
    public float limbMovementSpeed;
    public float limbMaxSpeed;
    [Tooltip("The radius (units) from which limbs can grab objects.")]
    public float grabRadius;
    public bool toggleGravityOnActivate;
    public float spawnPipeForce;


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
        InputManager.OnLookUpdated += MoveLimbs;
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
        InputManager.OnLookUpdated -= MoveLimbs;
        InputManager.OnPrimaryPressed -= AttachLimbs;
        InputManager.OnSecondaryPressed -= DetachLimbs;
    }

    void Start()
    {
        body2D.AddForce(Vector2.right * spawnPipeForce, ForceMode2D.Impulse);
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

    private void MoveLimbs(Vector2 movementDelta)
    {
        if (controller.monkeyExpired) return;

        float limbSpeed = Mathf.Min(limbMovementSpeed * movementDelta.magnitude, limbMaxSpeed);
        Vector2 limbMovement = limbSpeed * Time.fixedDeltaTime * movementDelta.normalized;
        
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

        //If any limb is attached AND active, move the body.
        if (leftUpperAttached || leftLowerAttached 
            || rightUpperAttached || rightLowerAttached
            || tailAttached)
        {
            body2D.MovePosition((Vector2)transform.position + limbMovement);
        }
    }

    void AttachLimbs()
    {
        if (leftUpperActive)
        {
            if (TryGrabHand(leftUpperHand))
            {
                leftUpperAttached = true;
                OnLeftUpperAttached?.Invoke(true);
            }
        }

        if (leftLowerActive)
        {
            if (TryGrabHand(leftLowerHand))
            {
                leftLowerAttached = true;
                OnLeftLowerAttached?.Invoke(true);
            }


        }

        if (rightLowerActive)
        {
            if (TryGrabHand(rightLowerHand))
            {
                rightLowerAttached = true;
                OnRightLowerAttached?.Invoke(true);
            }
        }

        if (rightUpperActive)
        {
            if(TryGrabHand(rightUpperHand))
            {
                rightUpperAttached = true;
                OnRightUpperAttached?.Invoke(true);
            }
            
        }

        if (tailActive)
        {
            if (TryGrabHand(tailHand))
            {
                tailAttached = true;
                OnTailAttached?.Invoke(true);
            }
        }
        
    }

    private bool TryGrabHand(Rigidbody2D hand)
    {
        List<RaycastHit2D> grabHits;
        
        grabHits = Physics2D.CircleCastAll(hand.transform.position, grabRadius,
            Vector2.zero, 0, LayerMask.GetMask("Grabable")).ToList();
        
        foreach (var hit in grabHits)
        {
            //Test for hazards
            if (hit.transform.CompareTag("Hazard"))
            {
                //Grabbed a hazard, what happens?
                return true;
            }
            //Test for props
            if (hit.transform.CompareTag("Prop"))
            {
                //Grabbed a prop
                /*hit.transform.parent = transform;*/
                //return true;
            }

            DistanceJoint2D socket = hit.transform.GetComponent<DistanceJoint2D>();
            //Else grabbable surface
            if (socket != null)
            {
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
            else { hand.constraints = RigidbodyConstraints2D.FreezeAll; }
            return true;
        }

        return false;
    }

    void DetachLimbs()
    {
        if (leftUpperActive)
        {
            leftUpperAttached = false;
            leftUpperHand.constraints = RigidbodyConstraints2D.None;
            OnLeftUpperAttached?.Invoke(false);
            RemoveSocket(LeftHandJointSocket);
        }

        if (leftLowerActive)
        {
            leftLowerAttached = false;
            leftLowerHand.constraints = RigidbodyConstraints2D.None;
            OnLeftLowerAttached?.Invoke(false);
            RemoveSocket(LeftFootJointSocket);

        }

        if (rightLowerActive)
        {
            rightLowerAttached = false;
            rightLowerHand.constraints = RigidbodyConstraints2D.None;
            OnRightLowerAttached?.Invoke(false);
            RemoveSocket(RightHandJointSocket);
        }

        if (rightUpperActive)
        {
            rightUpperAttached = false;
            rightUpperHand.constraints = RigidbodyConstraints2D.None;
            OnRightUpperAttached?.Invoke(false);
            RemoveSocket(RightFootJointSocket);
        }

        if (tailActive)
        {            
            tailAttached = false;
            tailHand.constraints = RigidbodyConstraints2D.None;
            OnTailAttached?.Invoke(false);
            RemoveSocket(TailJointSocket);
        }
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
            socket.connectedBody = socket.gameObject.GetComponent<Rigidbody2D>();
            socket.enabled = false;
            socket = null;
        }
    }
}
