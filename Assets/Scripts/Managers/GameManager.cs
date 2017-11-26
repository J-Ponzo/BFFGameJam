using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum PlayerRole
    {
        Medic,
        Dealer,
        Talky,
        Dwarf,
        None
    }
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
            DontDestroyOnLoad(this.gameObject);
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
    private GameObject sequenceManagerPfb;

    [SerializeField]
    private GameObject lobbyPfb;
    [SerializeField]
    private LobbyPlayer[] lobbyPlayers;

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

    [SerializeField]
    private AudioClip menuClip;
    [SerializeField]
    private AudioClip gameClip;

    private AudioSource source; 


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

    public LobbyPlayer[] LobbyPlayers
    {
        get
        {
            return lobbyPlayers;
        }

        set
        {
            lobbyPlayers = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        //Init Managers
        GameObject inputManager = Instantiate(inputManagerPfb);
        inputManager.transform.parent = this.transform;
        GameObject sequenceManager = Instantiate(sequenceManagerPfb);
        sequenceManager.transform.parent = this.transform;

        lobbyPlayers = new LobbyPlayer[4];
        lobbyPlayers[0] = Instantiate(lobbyPfb).GetComponent<LobbyPlayer>();
        lobbyPlayers[0].PlayerId = 0;
        lobbyPlayers[1] = Instantiate(lobbyPfb).GetComponent<LobbyPlayer>();
        lobbyPlayers[1].PlayerId = 1;
        lobbyPlayers[2] = Instantiate(lobbyPfb).GetComponent<LobbyPlayer>();
        lobbyPlayers[2].PlayerId = 2;
        lobbyPlayers[3] = Instantiate(lobbyPfb).GetComponent<LobbyPlayer>();
        lobbyPlayers[3].PlayerId = 3;
        foreach (LobbyPlayer lobby in lobbyPlayers)
        {
            lobby.PlayerRole = PlayerRole.None;
            string joystickName = InputManager.instance.GetJoystickNameFromPlayerId(lobby.PlayerId);
            if (joystickName != null)
            {
                lobby.KeyMap = InputManager.instance.GetKeymapFromJoytickName(joystickName);
            }
        }
        source = GetComponent<AudioSource>();
        MusiqueOnline();
       
    }

    public void CreatePlayersFromLobby()
    {
        InitPlayers();
        InitViewPorts();
        InitCameraLayers();
    }

    private void InitPlayers()
    {
        //Create Players
        player1 = Instantiate(playerPfb, new Vector3(-13f, 0f, -8f), Quaternion.identity);
        PlayerController player1Ctrl = player1.GetComponent<PlayerController>();
        player1Ctrl.PlayerId = 0;

        player2 = Instantiate(playerPfb, new Vector3(-11f, 0f, -8f), Quaternion.identity);
        PlayerController player2Ctrl = player2.GetComponent<PlayerController>();
        player2Ctrl.PlayerId = 1;

        player3 = Instantiate(playerPfb, new Vector3(-11f, 0f, -6f), Quaternion.identity);
        PlayerController player3Ctrl = player3.GetComponent<PlayerController>();
        player3Ctrl.PlayerId = 2;

        player4 = Instantiate(playerPfb, new Vector3(-13f, 0f, -6f), Quaternion.identity);
        PlayerController player4Ctrl = player4.GetComponent<PlayerController>();
        player4Ctrl.PlayerId = 3;

        players = new GameObject[4];
        players[0] = player1;
        players[1] = player2;
        players[2] = player3;
        players[3] = player4;

        //Set Controls
        for (int i = 0; i < players.Length; i++)
        {
            PlayerController playerCtrl = players[i].GetComponent<PlayerController>();
            playerCtrl.PlayerId = lobbyPlayers[i].PlayerId;
            playerCtrl.KeyMap = lobbyPlayers[i].KeyMap;
            playerCtrl.PlayerRole = lobbyPlayers[i].PlayerRole;
        }
    }

    private void InitCameraLayers()
    {
        //Get Layers & masks
        int player1ArmsLayer = LayerMask.NameToLayer("Player1Arms");
        int player2ArmsLayer = LayerMask.NameToLayer("Player2Arms");
        int player3ArmsLayer = LayerMask.NameToLayer("Player3Arms");
        int player4ArmsLayer = LayerMask.NameToLayer("Player4Arms");
        int player1ArmsLayerMask = 1 << player1ArmsLayer;
        int player2ArmsLayerMask = 1 << player2ArmsLayer;
        int player3ArmsLayerMask = 1 << player3ArmsLayer;
        int player4ArmsLayerMask = 1 << player4ArmsLayer;
        int player1BodyLayer = LayerMask.NameToLayer("Player1Body");
        int player2BodyLayer = LayerMask.NameToLayer("Player2Body");
        int player3BodyLayer = LayerMask.NameToLayer("Player3Body");
        int player4BodyLayer = LayerMask.NameToLayer("Player4Body");
        int player1BodyLayerMask = 1 << player1BodyLayer;
        int player2BodyLayerMask = 1 << player2BodyLayer;
        int player3BodyLayerMask = 1 << player3BodyLayer;
        int player4BodyLayerMask = 1 << player4BodyLayer;

        //Set Arms Layers
        Camera player1Cam = player1.GetComponentInChildren<Camera>();
        GameObject player1Arms = player1Cam.transform.Find("Arms").gameObject;
        SetLayerOnSubTree(player1Arms, player1ArmsLayer);

        Camera player2Cam = player2.GetComponentInChildren<Camera>();
        GameObject player2Arms = player2Cam.transform.Find("Arms").gameObject;
        SetLayerOnSubTree(player2Arms, player2ArmsLayer);

        Camera player3Cam = player3.GetComponentInChildren<Camera>();
        GameObject player3Arms = player3Cam.transform.Find("Arms").gameObject;
        SetLayerOnSubTree(player3Arms, player3ArmsLayer);

        Camera player4Cam = player4.GetComponentInChildren<Camera>();
        GameObject player4Arms = player4Cam.transform.Find("Arms").gameObject;
        SetLayerOnSubTree(player4Arms, player4ArmsLayer);

        //Set Body Layers
        GameObject player1Body = player1.transform.Find("Body").gameObject;
        SetLayerOnSubTree(player1Body, player1BodyLayer);

        GameObject player2Body = player2.transform.Find("Body").gameObject;
        SetLayerOnSubTree(player2Body, player2BodyLayer);

        GameObject player3Body = player3.transform.Find("Body").gameObject;
        SetLayerOnSubTree(player3Body, player3BodyLayer);

        GameObject player4Body = player4.transform.Find("Body").gameObject;
        SetLayerOnSubTree(player4Body, player4BodyLayer);

        //Set Camera Culling Masks
        int mask = 0;
        mask = player1BodyLayerMask | player2ArmsLayerMask | player3ArmsLayerMask | player4ArmsLayerMask;
        player1Cam.cullingMask = ~mask;

        mask = 0;
        mask = player1ArmsLayerMask | player2BodyLayerMask | player3ArmsLayerMask | player4ArmsLayerMask;
        player2Cam.cullingMask = ~mask;

        mask = 0;
        mask = player1ArmsLayerMask | player2ArmsLayerMask | player3BodyLayerMask | player4ArmsLayerMask;
        player3Cam.cullingMask = ~mask;

        mask = 0;
        mask = player1ArmsLayerMask | player2ArmsLayerMask | player3ArmsLayerMask | player4BodyLayerMask;
        player4Cam.cullingMask = ~mask;
    }

    private void SetLayerOnSubTree(GameObject obj, int layer)
    {
        obj.layer = layer;

        for (int i = 0; i < obj.transform.childCount; i++)
        {
            Transform child = obj.transform.GetChild(i);
            SetLayerOnSubTree(child.gameObject, layer);
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

    public void MusiqueOnline()
    {
        if (SequenceManager.instance.CurSequence == SequenceManager.Sequence.InGame)
        {
            source.clip = gameClip;
            source.volume = 0.1f;
            source.Play(2);
        }
        else
        {
            source.clip = menuClip;
            source.volume = 0.3f;
            source.Play(2);
        }
    }
}
