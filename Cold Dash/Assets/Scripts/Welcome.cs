using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;


public class Welcome : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene ("ChooseLevel");
    }

    public void Options()
    {
        // Load the Intro scene (make sure the scene index is correct)
        SceneManager.LoadScene ("Options"); // Replace "Intro" with the actual name of your intro scene
    }

    public void Intro()
    {
        // Load the Intro scene (make sure the scene index is correct)
        SceneManager.LoadScene ("Intro"); // Replace "Intro" with the actual name of your intro scene
    }


    public void BackToMenu()
    {
        // Load the main menu scene (make sure the scene index is correct)
        SceneManager.LoadScene ("Menu"); // Replace "MainMenu" with the actual name of your main menu scene
    }

 


}
