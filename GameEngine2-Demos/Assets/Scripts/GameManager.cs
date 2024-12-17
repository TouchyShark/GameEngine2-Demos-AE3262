using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject player;
    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (gameOverUI.activeInHierarchy) 
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else 
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void GameOver() 
    {
        gameOverUI.SetActive(true);
    }

    public void restart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Restart");
    }
    
    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Going to MainMenu");
    }

    public void quit() 
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
