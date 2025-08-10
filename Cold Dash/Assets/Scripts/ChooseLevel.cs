using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadEasyLevel()
    {
        // Load level 1 when Easy is selected
        SceneManager.LoadScene ("Level1"); // Replace "Level1" with your actual scene name or index
    }

    public void LoadMediumLevel()
    {
        SceneManager.LoadScene ("Level2"); // Replace "Level2" with your actual scene name or index
    }

    public void LoadHardLevel()
    {
        // Load level 3 when Hard is selected
        SceneManager.LoadScene ("Level3"); // Replace "Level3" with your actual scene name or index
    }
}
