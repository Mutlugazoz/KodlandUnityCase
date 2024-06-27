using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private List<Alien> aliens;
    [SerializeField] private GameObject Hud, VictoryPanel, GameOverPanel;
    [SerializeField] private PlayerController playerController;

    private void Start()
    {
        for (int i = 0; i < aliens.Count; i++)
            aliens[i].Died += OnAlienDied;

        playerController.Died += OnPlayerDied;
    }

    private void OnAlienDied(Alien alien)
    {
        aliens.Remove(alien);
        alien.Died -= OnAlienDied;

        if (aliens.Count > 0) return;

        VictoryPanel.SetActive(true);
        Hud.SetActive(false);
        playerController.OnGameEnded();
    }

    private void OnPlayerDied()
    {
        for (int i = 0; i < aliens.Count; i++)
            aliens[i].Died -= OnAlienDied;

        playerController.Died -= OnPlayerDied;
        playerController.OnGameEnded();

        GameOverPanel.SetActive(true);
        Hud.SetActive(false);
    }
}
