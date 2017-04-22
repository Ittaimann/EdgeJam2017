using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    public enum StateType
    {
        DEFAULT,      //Fall-back state, should never happen
        PLAYING,      //waiting for other player to finish his turn
        SHOP,    //Once, on start of each player's turn
        GAMEOVER,
    };

    public enum Direction
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }

    GameObject playerObject;

    private bool isDead;
    public float respawnTIme;

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
       playerObject = FindObjectOfType<PlayerMovement>().gameObject;
    } 

    private void Update()
    {
        if (isDead)
        {

        }
    }

    public void PlayerDied()
    {
        StartCoroutine(PlayerDiedCoroutine());
    }

    public IEnumerator PlayerDiedCoroutine()
    {
        isDead = true;
        yield return new WaitForSeconds(respawnTIme);
        Respawn();
    }

    public void Respawn()
    {
        Transform respawnPoint = GameObject.FindGameObjectWithTag("RespawnPoint").transform;
        playerObject.SetActive(true);
        playerObject.transform.position = respawnPoint.position;
        isDead = false;
    }
}
