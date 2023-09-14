using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public static bool gameIsPaused;
    private GameObject menuCanvas;
    // Start is called before the first frame update
    private void Start()
    {
        menuCanvas = GameObject.Find("Panel");
        menuCanvas.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuCanvas.SetActive(!menuCanvas.activeSelf);
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
    }

    void PauseGame ()
    {
        if(gameIsPaused)
        {
            Time.timeScale = 0f;
        }
        else 
        {
            Time.timeScale = 1;
        }
    }

    public void Resume()
    {
        menuCanvas.SetActive(false);
        gameIsPaused = false;
        PauseGame();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadMenu()
    {
        gameIsPaused = false;
        PauseGame();
        SceneManager.LoadScene("Menu");
    }
}
