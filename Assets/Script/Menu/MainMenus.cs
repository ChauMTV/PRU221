using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenus : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //BackGroundMusic.;
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

}
