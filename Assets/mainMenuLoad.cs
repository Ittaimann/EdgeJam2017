using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class mainMenuLoad : MonoBehaviour {

    public void ReturnButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
