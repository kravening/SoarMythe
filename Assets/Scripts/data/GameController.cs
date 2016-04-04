﻿using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameController {

    static bool gameEnded = false;

    public static void WinGame() {
        if (!gameEnded) {
            gameEnded = true;

            // You win! Woo hoo!
            // Bring out the cake!
            Debug.Log("You win! TAKE CAKE!! NOW!!");
        }
    }

    public static void LoseGame() {
        if (!gameEnded) {
            gameEnded = true;

            // Trigger Lose UI.
            Debug.Log("Seems like you died.. How tragic.");
        }
    }

    public static void RestartCurrentScene() {
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name);
    }
}
