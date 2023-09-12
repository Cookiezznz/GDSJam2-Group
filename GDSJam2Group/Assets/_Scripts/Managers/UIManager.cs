using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject gameOverScreen;
    public TextMeshProUGUI gameOverWinLossText;
    public TextMeshProUGUI gameOverScore;

    public Animator expireUIAnim;
    public TextMeshProUGUI expireText;

    private void OnEnable()
    {
        GameStateManager.OnGameOver += OnGameOver;
        GameStateManager.OnPaused += OnPaused;
        MonkeyManager.OnMonkeyExpired += UpdateExpireUI;
    }
    
    private void OnDisable()
    {
        GameStateManager.OnGameOver -= OnGameOver;
        GameStateManager.OnPaused -= OnPaused;
        MonkeyManager.OnMonkeyExpired -= UpdateExpireUI;
    }

    void OnGameOver()
    {
        Cursor.lockState = CursorLockMode.Confined;
        menu.SetActive(true);
        gameOverScreen.SetActive(true);
        gameOverWinLossText.text = GameStateManager.Instance.GetGameState().isVictorious ? "Victory" : "Defeat";
        gameOverScore.text = GameStateManager.Instance.GetGameState().score.ToString("00.00");
    }

    void OnPaused(bool toggle)
    {

        Cursor.lockState = toggle ? CursorLockMode.Confined : CursorLockMode.Locked;
    }

    void UpdateExpireUI(int numExpired)
    {
        //Update text, then enable game object to play animation.
        expireText.text = $"{numExpired}";
        expireUIAnim.SetTrigger("Expire");
    }
    
}
