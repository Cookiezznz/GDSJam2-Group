using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject gameOverScreen;
    public TextMeshProUGUI gameOverWinLossText;
    public TextMeshProUGUI gameOverScore;

    private void OnEnable()
    {
        GameStateManager.OnGameOver += OnGameOver;
        GameStateManager.OnPaused += OnPaused;
    }
    
    private void OnDisable()
    {
        GameStateManager.OnGameOver -= OnGameOver;
        GameStateManager.OnPaused -= OnPaused;

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
    
}
