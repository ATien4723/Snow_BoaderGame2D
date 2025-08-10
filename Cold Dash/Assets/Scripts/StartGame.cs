using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartGame : MonoBehaviour
{
    public void ButtonPressed() 
    {
        SceneManager.LoadScene(1);
    }

    public void Close()
    {
        Application.Quit();
    }
}
