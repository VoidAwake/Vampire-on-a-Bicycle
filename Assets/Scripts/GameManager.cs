using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Hardware;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerController playerController;
    public LightController lightController;
    public Generator generator;
    public GameObject restartContainer;
    public Button restartButton;
    public Button quitButton;
    public Image restartOverlay;
    public Text scoreText;
    public int overlayTime;

    private int overlayTimer;

    private void Start() {
        restartButton.onClick.AddListener(Restart);
        quitButton.onClick.AddListener(Application.Quit);
    }

    private void Update() {
        if (overlayTimer > 0) {
            overlayTimer--;
            restartOverlay.color = new Color(1, 1, 1, 1 - (float)overlayTimer / overlayTime);

            if (--overlayTimer == 0) restartContainer.SetActive(true);
        }
    }

    public void Restart () {
        playerController.Restart();
        lightController.Restart();
        generator.Restart();

        restartContainer.SetActive(false);

        restartOverlay.color = new Color(1, 1, 1, 0);

        scoreText.color = new Color(1, 1, 1);
    }

    public void Disable () {
        playerController.enabled = false;
        lightController.enabled = false;
        generator.enabled = false;

        overlayTimer = overlayTime;

        scoreText.color = new Color(0, 0, 0);
    }
}

