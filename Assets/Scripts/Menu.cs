using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public void menuStart()
    {
        SceneManager.LoadScene("FinalGame Scene");
    }

    public void menuCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void menuOptions()
    {
        SceneManager.LoadScene("Options");
    }

    public void menuInfo()
    {
        SceneManager.LoadScene("GameInfo");
    }

    public void menuExit()
    {
        Application.Quit();
    }

    public void menuReturn()
    {
        SceneManager.LoadScene("Start Scene");
    }

}
