using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// The static instance of the Singleton for external access
    /// </summary>
    public static GameManager instance = null;

    /// <summary>
    /// Enforce Singleton properties
    /// </summary>
    void Awake()
    {
        //Check if instance already exists and set it to this if not
        if (instance == null)
        {
            instance = this;
        }

        //Enforce the unicity of the Singleton
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    [SerializeField]
    private GameObject inputManagerPfb;

    [SerializeField]
    private GameObject playerPfb;
    [SerializeField]
    private GameObject[] players;
    [SerializeField]
    private GameObject player1;
    [SerializeField]
    private GameObject player2;
    [SerializeField]
    private GameObject player3;
    [SerializeField]
    private GameObject player4;

    public GameObject Player1
    {
        get
        {
            return player1;
        }

        set
        {
            player1 = value;
        }
    }

    public GameObject Player2
    {
        get
        {
            return player2;
        }

        set
        {
            player2 = value;
        }
    }

    public GameObject Player3
    {
        get
        {
            return player3;
        }

        set
        {
            player3 = value;
        }
    }

    public GameObject Player4
    {
        get
        {
            return player4;
        }

        set
        {
            player4 = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        //Init Managers
        GameObject inputManager = Instantiate(inputManagerPfb);
        inputManager.transform.parent = this.transform;

        //Init Game State
        InitPlayers();
        InitViewPorts();
    }

    private void InitPlayers()
    {
        player1 = Instantiate(playerPfb);
        PlayerController player1Ctrl = player1.GetComponent<PlayerController>();
        player1Ctrl.PlayerId = 0;

        player2 = Instantiate(playerPfb);
        PlayerController player2Ctrl = player2.GetComponent<PlayerController>();
        player2Ctrl.PlayerId = 1;

        player3 = Instantiate(playerPfb);
        PlayerController player3Ctrl = player3.GetComponent<PlayerController>();
        player3Ctrl.PlayerId = 2;

        player4 = Instantiate(playerPfb);
        PlayerController player4Ctrl = player4.GetComponent<PlayerController>();
        player4Ctrl.PlayerId = 3;

        players = new GameObject[4];
        players[0] = player1;
        players[1] = player2;
        players[2] = player3;
        players[3] = player4;

        foreach (GameObject player in players)
        {
            PlayerController playerCtrl = player.GetComponent<PlayerController>();
            string joystickName = InputManager.instance.GetJoystickNameFromPlayerId(playerCtrl.PlayerId);
            if (joystickName != null)
            {
                playerCtrl.KeyMap = InputManager.instance.GetKeymapFromJoytickName(joystickName);
            }
        }
    }

    public void InitViewPorts()
    {
        Camera player1Cam = GameManager.instance.Player1.GetComponentInChildren<Camera>();
        player1Cam.rect = new Rect(0f, 0f, 0.5f, 0.5f);

        Camera player2Cam = GameManager.instance.Player2.GetComponentInChildren<Camera>();
        player2Cam.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);

        Camera player3Cam = GameManager.instance.Player3.GetComponentInChildren<Camera>();
        player3Cam.rect = new Rect(0f, 0.5f, 0.5f, 0.5f);

        Camera player4Cam = GameManager.instance.Player4.GetComponentInChildren<Camera>();
        player4Cam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
