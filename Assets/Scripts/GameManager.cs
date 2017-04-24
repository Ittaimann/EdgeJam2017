using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    public enum StateType
    {
        DEFAULT,      //Fall-back state, should never happen
        PLAYING,      //waiting for other player to finish his turn
        SHOP,    //Once, on start of each player's turn
        GAMEOVER,
    };

    GameObject playerObject;

    private bool isDead;
    public float respawnTIme;

    private int currentLevel;
    private List<Camera> cameras;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        isDead = false;
        playerObject = FindObjectOfType<PlayerMovement>().gameObject;
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        cameras = new List<Camera>();
        foreach(Camera cam in Camera.allCameras)
        {
            if (cam != Camera.main)
                cameras.Add(cam);
        }
    }

    private void Update()
    {
        if (isDead)
        {
            if (Input.anyKeyDown)
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void PlayerDied()
    {

        isDead = true;
        //StartCoroutine(PlayerDiedCoroutine());
    }

    //public IEnumerator PlayerDiedCoroutine()
    //{
    //    isDead = true;
    //    yield return new WaitForSeconds(respawnTIme);
    //    //Respawn();
    //}

    public void Respawn()
    {
        Transform respawnPoint = GameObject.FindGameObjectWithTag("RespawnPoint").transform;
        playerObject.SetActive(true);
        playerObject.transform.position = respawnPoint.position;
        isDead = false;
    }

    public void LoadNextLevel()
    {
        currentLevel++;
        if (currentLevel < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(currentLevel);
    }

    public Camera CurrentCamera()
    {
        Vector3 viewPos;
        foreach(Camera cam in cameras)
        {
            viewPos = cam.WorldToViewportPoint(playerObject.transform.position);
            if (viewPos.x > 0 && viewPos.x < 1 && viewPos.y > 0 && viewPos.y < 1)
                return cam;

        }
        return null;
    }
}
