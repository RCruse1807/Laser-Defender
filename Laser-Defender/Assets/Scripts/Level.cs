using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] private float delayTime = 1f;

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
        
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
        FindObjectOfType<GameSession>().RestartGame();
    }

    public void LoadGameOver()
    {
        StartCoroutine(DelayGameOver());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator DelayGameOver()
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(2);
    }
}
