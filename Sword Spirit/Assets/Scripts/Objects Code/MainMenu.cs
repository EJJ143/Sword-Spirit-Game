using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadScene("Scenes/Castle");
    }

    public void rules()
    {
        SceneManager.LoadScene("Scenes/Rules Screen");
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("Scenes/Splash Screen");
    }
}
