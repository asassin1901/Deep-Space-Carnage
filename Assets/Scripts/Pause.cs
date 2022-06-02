using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseScreen;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
        }    
    }

    public void Resume(){
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
    }

    public void Menu(){
        SceneManager.LoadScene("Menu");
    }

    public void Exit(){
        Application.Quit();
    }
}
