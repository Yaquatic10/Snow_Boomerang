using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    
    public void Changeschene(string name)
    {

        SceneManager.LoadScene(name);
    }

    public void Closegame()
    {

        Application.Quit();

    }
}


