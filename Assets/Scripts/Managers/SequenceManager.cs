using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SequenceManager : MonoBehaviour {
    public enum Sequence
    {
        Menus,
        InGame
    }

    /// <summary>
    /// The static instance of the Singleton for external access
    /// </summary>
    public static SequenceManager instance = null;

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
    private string MENU_SCENE_NAME = "MenuScene";
    [SerializeField]
    private string GAME_SCENE_NAME = "testScene"; 

     [SerializeField]
    private Sequence curSequence = Sequence.Menus;

    public Sequence CurSequence
    {
        get
        {
            return curSequence;
        }

        set
        {
            curSequence = value;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (curSequence == Sequence.Menus)
            {
                LoadSequence(Sequence.InGame);
            } else
            {
                LoadSequence(Sequence.Menus);
            }
        }
    }

    public void LoadSequence(Sequence newSequence)
    {
        switch(newSequence)
        {
            case Sequence.Menus:
                LoadMenus();
                break;
            case Sequence.InGame:
                LoadGame();
                break;
        }
        curSequence = newSequence;
    }

    private void LoadGame()
    {
        SceneManager.LoadScene(GAME_SCENE_NAME);
        GameManager.instance.CreatePlayersFromLobby();
    }

    private void LoadMenus()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            Destroy(players[i]);
        }
        SceneManager.LoadScene(MENU_SCENE_NAME);
        foreach(LobbyPlayer lobby in GameManager.instance.LobbyPlayers) {
            lobby.PlayerRole = GameManager.PlayerRole.None;
        }
    }
}
