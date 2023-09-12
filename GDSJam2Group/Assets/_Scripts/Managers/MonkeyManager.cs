using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonkeyManager : MonoBehaviour
{
    public float respawnTimer;
    public int monkeysExpired;
    public Transform respawnPoint;
    public GameObject monkeyPrefab;
    public PlayerController currentMonkey;

    public static event Action<GameObject> OnMonkeySpawned;
    public static event Action<int> OnMonkeyExpired;

    void OnEnable()
    {
        PlayerController.OnMonkeyExpired += NewExpiry;
    }

    void OnDisable()
    {
        PlayerController.OnMonkeyExpired -= NewExpiry;
    }

    void NewExpiry()
    {
        monkeysExpired++;
        OnMonkeyExpired?.Invoke(monkeysExpired);
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTimer);
        
        GameObject monkey = Instantiate(monkeyPrefab, transform);
        monkey.name = "Monkey";

        monkey.transform.position = respawnPoint.position;
        currentMonkey = monkey.GetComponent<PlayerController>();
        OnMonkeySpawned?.Invoke(monkey);

        yield return null;
    }

}
