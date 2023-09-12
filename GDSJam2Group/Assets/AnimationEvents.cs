using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvents : MonoBehaviour
{
    public UnityEvent onAnimationEnd;
    
    public void AnimationEnded()
    {
        onAnimationEnd?.Invoke();
    }
    
}
