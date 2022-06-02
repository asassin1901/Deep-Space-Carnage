using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseScreen;
    public bool isPaused = false;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && isPaused == false)
        {
            isPaused = true;
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
        }   else if(Input.GetKeyDown(KeyCode.Escape) && isPaused == true)
        {
            isPaused = false;
            Time.timeScale = 1f;
            pauseScreen.SetActive(false);
        }
    }

    public void Resume(){
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
    }

    public void Menu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void Exit(){
        Application.Quit();
    }
}
