using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadlevel : MonoBehaviour {

    public  void load()
    {
        SceneManager.LoadScene("JohnnyTest");
    }

    public void exit()
    {
        Application.Quit();
    }
}
