using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public void GoToMainMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void GoToGame() {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
