using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagementController : MonoBehaviour
{


    void Start()
    {

    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Finish")) {
            LaunchNextScene();
        }
        if (collision.gameObject.CompareTag("Quit")) {
            print("Quitting is for losers");
            Application.Quit();
        }
        if (collision.gameObject.CompareTag("Level 0")) {
            LaunchScene(1);
        }
        if (collision.gameObject.CompareTag("Level 1")) {
            LaunchScene(2);
        }
        if (collision.gameObject.CompareTag("Level 2")) {
            LaunchScene(3);
        }
        if (collision.gameObject.CompareTag("Level 3")) {
            LaunchScene(4);
        }
    }

    private void LaunchNextScene() {
        int SceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin"));
        if(SceneIndex == SceneManager.sceneCountInBuildSettings - 1) {
            SceneManager.LoadScene(0);
        }
        else {
            SceneManager.LoadScene(SceneIndex +1);
        }
    }
    private void LaunchScene(int sceneToLaunch) {
        PlayerPrefs.SetInt("Coin", 0);
        SceneManager.LoadScene(sceneToLaunch);
    }
}
