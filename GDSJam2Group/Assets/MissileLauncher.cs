using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    public List<Animator> missiles;
    public float launchDialogDuration;

    void OnEnable()
    {
        Fabricator.OnFabricated += LaunchMissile;
    }

    void OnDisable()
    {
        Fabricator.OnFabricated += LaunchMissile;
    }

    void LaunchMissile(int numberOfFabrications)
    {
        StartCoroutine(LaunchMissile());
    }

    IEnumerator LaunchMissile()
    {
        AudioManager.Instance.PlayDialogue("missileLaunch");
        //Start dialogue
        yield return new WaitForSeconds(launchDialogDuration);
        missiles[0].SetTrigger("Launch");
        missiles.RemoveAt(0);

        yield return null;
    }

    
}
