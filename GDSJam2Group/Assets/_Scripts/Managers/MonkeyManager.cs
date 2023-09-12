using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyManager : MonoBehaviour
{
    public float respawnTimer;
    public int monkeysExpired;
    public Transform respawnPoint;
    public GameObject monkeyPrefab;
    public PlayerController currentMonkey;

    public static event Action<GameObject> OnMonkeySpawned;

    void OnEnable()
    {
        PlayerController.OnMonkeyExpired += RespawnMonkey;
    }

    void OnDisable()
    {
        PlayerController.OnMonkeyExpired -= RespawnMonkey;
    }

    void RespawnMonkey()
    {
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTimer);

        monkeysExpired++;
        GameObject monkey = Instantiate(monkeyPrefab, transform);
        monkey.name = "Monkey";

        monkey.transform.position = respawnPoint.position;
        currentMonkey = monkey.GetComponent<PlayerController>();
        OnMonkeySpawned?.Invoke(monkey);

        yield return null;
    }

}
