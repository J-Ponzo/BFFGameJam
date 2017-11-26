using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPlayer : MonoBehaviour {
    [SerializeField]
    private int playerId;
    [SerializeField]
    private InputManager.KeyMapping keyMap = InputManager.KeyMapping.KeyBoard;
    [SerializeField]
    private GameManager.PlayerRole playerRole = GameManager.PlayerRole.None;
    private bool isReady = false;

    private static int nbPlayerReady = 4;
    private int time;

    private static bool menuIsLoad = false; 
    private GameObject rolePlayer1;
    private GameObject rolePlayer2;
    private GameObject rolePlayer3;
    private GameObject rolePlayer4;

    private GameObject imagePlayer1;
    private GameObject imagePlayer2;
    private GameObject imagePlayer3;
    private GameObject imagePlayer4;

    [SerializeField]
    private Sprite MedicSprite;
    [SerializeField]
    private Sprite DispenserSprite;
    [SerializeField]
    private Sprite DwarfSprite;
    [SerializeField]
    private Sprite FloorWalkerSprite;
    [SerializeField]
    private Sprite DefaultSprite; 
   
    private static List<int> listRole = new List<int>() { 
        0,
        1,
        2,
        3,};

    private static List<int> playerIdList = new List<int>() {
        0,
        1,
        2,
        3 };

    public int PlayerId
    {
        get
        {
            return playerId;
        }

        set
        {
            playerId = value;
        }
    }

    public GameManager.PlayerRole PlayerRole
    {
        get
        {
            return playerRole;
        }

        set
        {
            playerRole = value;
        }
    }

    public InputManager.KeyMapping KeyMap
    {
        get
        {
            return keyMap;
        }

        set
        {
            keyMap = value;
        }
    }

    private void Start()
    {
    
    }

    private void Awake()
    {
        
        DontDestroyOnLoad(this.gameObject);
    }

   
    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("MainCanvas") != null)
        {
            if (GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<HelloWorld>().IsPlayMenuActive == true && listRole.Count > 0)
            {
                int index = listRole[Random.Range(0, listRole.Count)];
                if (InputManager.instance.GetKeyDown(playerId, keyMap, InputManager.ActionControl.Pause))
                {
                    if (!isReady)
                    {

                        playerRole = (GameManager.PlayerRole)index;
                        listRole.Remove((int)playerRole);
                        isReady = true;


                        nbPlayerReady--;
                        GameObject.Find("NbPlayerTxt").GetComponent<Text>().text = "" + nbPlayerReady;

                        switch (playerId)
                        {
                            case 0:
                                rolePlayer1 = GameObject.Find("PlayerRoleTxt");
                                rolePlayer1.GetComponent<Text>().text = RoleToString(playerRole);
                                imagePlayer1 = GameObject.Find("ImagePlayer");
                                imagePlayer1.GetComponent<Image>().sprite = RoleToImage(playerRole);
                                break;
                            case 1:
                                rolePlayer2 = GameObject.Find("PlayerRole1Txt");
                                rolePlayer2.GetComponent<Text>().text = RoleToString(playerRole);
                                imagePlayer2 = GameObject.Find("ImagePlayer1");
                                imagePlayer2.GetComponent<Image>().sprite = RoleToImage(playerRole);
                                break;
                            case 2:
                                rolePlayer3 = GameObject.Find("PlayerRole2Txt");
                                rolePlayer3.GetComponent<Text>().text = RoleToString(playerRole);
                                imagePlayer3 = GameObject.Find("ImagePlayer2");
                                imagePlayer3.GetComponent<Image>().sprite = RoleToImage(playerRole);
                                break;
                            case 3:
                                rolePlayer4 = GameObject.Find("PlayerRole3Txt");
                                rolePlayer4.GetComponent<Text>().text = RoleToString(playerRole);
                                imagePlayer4 = GameObject.Find("ImagePlayer3");
                                imagePlayer4.GetComponent<Image>().sprite = RoleToImage(playerRole);
                                break;
                        }
                    }
                    else
                    {
                        listRole.Add((int)playerRole);
                        playerRole = GameManager.PlayerRole.None;
                        nbPlayerReady++;
                        GameObject.Find("NbPlayerTxt").GetComponent<Text>().text = "" + nbPlayerReady;
                        Reinit();
                        isReady = false;
                    }
                }

            }

            if (nbPlayerReady == 0 && !menuIsLoad)
            {
                GameObject.Find("NbPlayerTxt").GetComponent<Text>().text = "";
                menuIsLoad = true;
                StartCoroutine(Countdown());
            }
        }


    }

    private IEnumerator Countdown()
    {
        float duration = 5f; // 3 seconds you can change this 
                             //to whatever you want
        float temp = 0;
        while (temp <= 4.9f)
        {
            GameObject.Find("PlayersLeftTxt").GetComponent<Text>().text = "Starting in " + (int)(duration-temp);
            temp += Time.deltaTime ;
            yield return null;
        }
       
        SequenceManager.instance.LoadSequence(SequenceManager.Sequence.InGame);
        GameManager.instance.MusiqueOnline(); 
    }




    private void Reinit()
    {
        switch (playerId)
        {
            case 0:
                rolePlayer1 = GameObject.Find("PlayerRoleTxt");
                rolePlayer1.GetComponent<Text>().text = "UNKNOWN";
                imagePlayer1 = GameObject.Find("ImagePlayer");
                imagePlayer1.GetComponent<Image>().sprite = DefaultSprite;
                break;
            case 1:
                rolePlayer2 = GameObject.Find("PlayerRole1Txt");
                rolePlayer2.GetComponent<Text>().text = "UNKNOWN";
                imagePlayer2 = GameObject.Find("ImagePlayer1");
                imagePlayer2.GetComponent<Image>().sprite = DefaultSprite;
                break;
            case 2:
                rolePlayer3 = GameObject.Find("PlayerRole2Txt");
                rolePlayer3.GetComponent<Text>().text = "UNKNOWN";
                imagePlayer3 = GameObject.Find("ImagePlayer2");
                imagePlayer3.GetComponent<Image>().sprite = DefaultSprite;
                break;
            case 3:
                rolePlayer4 = GameObject.Find("PlayerRole3Txt");
                rolePlayer4.GetComponent<Text>().text = "UNKNOWN";
                imagePlayer4 = GameObject.Find("ImagePlayer3");
                imagePlayer4.GetComponent<Image>().sprite = DefaultSprite;
                break;
        }
    }

    private string RoleToString(GameManager.PlayerRole playerRole)
    {
        switch(playerRole)
        {
            case GameManager.PlayerRole.Dealer:
                return "Dispenser";
                break;
            case GameManager.PlayerRole.Dwarf:
                return "Passe-Partout";
                break;
            case GameManager.PlayerRole.Medic:
                return "Medic";
                break;
            case GameManager.PlayerRole.Talky:
                return "floorWalker";
                break;
            default: return "WTF";
        }
    }

    private Sprite RoleToImage (GameManager.PlayerRole playerRole)
    {
        switch (playerRole)
        {
            case GameManager.PlayerRole.Dealer:
                return DispenserSprite;
                break;
            case GameManager.PlayerRole.Dwarf:
                return DwarfSprite;
                break;
            case GameManager.PlayerRole.Medic:
                return MedicSprite;
                break;
            case GameManager.PlayerRole.Talky:
                return FloorWalkerSprite;
                break;
            default: return DefaultSprite;
                    
                    ;
        }

    }
    
}
