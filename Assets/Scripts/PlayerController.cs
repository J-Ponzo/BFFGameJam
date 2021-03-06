﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    private int playerId = 0;
    [SerializeField]
    private GameManager.PlayerRole playerRole;
    [SerializeField]
    private InputManager.KeyMapping keyMap = InputManager.KeyMapping.KeyBoard;
    [SerializeField]
    private GameObject hudPfb;

    public float range = 2f;
    
    [SerializeField]
    private float moveSpeed = 3f;
    [SerializeField]
    private float aimSpeed = 90f;
    [SerializeField]
    private float axisThreshold = 0.1f;

    private GameObject playerCam;


    public GunController gun;
    private HUDManager hudScript;
	private Animator anim;

    public int bullet = 30;
    public int totalBullet = 30;
    public int sacADos = 50;
    private int maxSacADos = 50;

    private bool unluck = false;

    [SerializeField]
    private ShkumunManager.Malus malus;
    [SerializeField]
    private float leggMalusFactor = 0.1f;
    [SerializeField]
    private float runFactor = 3f;
    [SerializeField]
    private bool isRunning = false;

    public int nbTir = 0;

    public int PlayerId {
        get {

            return playerId;
        }

        set
        {
            playerId = value;
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

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start () {
		anim = GetComponent<Animator> ();
        playerCam = GetComponentInChildren<Camera>().gameObject;

        GameObject hud = Instantiate(hudPfb);
        hudScript = hud.GetComponent<HUDManager>();

        hudScript.SetAmmoCurrentValue(bullet);
        hudScript.SetAmmoMax(sacADos);

        hud.transform.SetParent(this.transform);
        Canvas hudCanvas = hud.GetComponent<Canvas>();
        hudCanvas.worldCamera = playerCam.GetComponent<Camera>();
        hudCanvas.planeDistance = 0.35f;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		anim.SetFloat("speed", 0);
		anim.SetBool("shoot", false);
        HandleMotion();
        HandleAim();
        HandleAction();
    }

    private void HandleAction()
    {
        if (InputManager.instance.GetKeyDown(playerId, keyMap, InputManager.ActionControl.Fire))
        {
            Fire(playerId);
        }
        if (InputManager.instance.GetKeyDown(playerId, keyMap, InputManager.ActionControl.Action))
        {
            RoleAction();
        }
        if (InputManager.instance.GetKeyDown(playerId, keyMap, InputManager.ActionControl.Reload))
        {
            Reload();
        }
        if (InputManager.instance.GetKeyDown(playerId, keyMap, InputManager.ActionControl.Run))
        {
            StartRunning();
        }
        if (InputManager.instance.GetKeyUp(playerId, keyMap, InputManager.ActionControl.Run))
        {
            StopRunning();
        }
    }

    private void StopRunning()
    {
        isRunning = false;
    }

    private void StartRunning()
    {
        isRunning = true;
    }

    private void RoleAction()
    {
        switch(playerRole)
        {
            case GameManager.PlayerRole.Dealer:
                DealAmmo();
                break;
            case GameManager.PlayerRole.Dwarf:
                CloseDoor();
                break;
            case GameManager.PlayerRole.Medic:
                Heal();
                break;
            case GameManager.PlayerRole.Talky:
                CallComander();
                break;
        }
    }

    private void CallComander() {
        Debug.Log("CallComander");
    }

    private void Heal() {
        Debug.Log("Heal");

        GameObject[] players = new GameObject[4];

        players[0] = GameManager.instance.Player1;
        players[1] = GameManager.instance.Player2;
        players[2] = GameManager.instance.Player3;
        players[3] = GameManager.instance.Player4;

        for (int i = 0; i < players.Length; i++) {

            if (players[i].GetComponent<PlayerController>().playerId != this.playerId && Vector3.Distance(transform.position, players[i].transform.position) <= range) {
                Debug.Log("SOIGNE !");
                break;
            }
        }

    }

    private void CloseDoor() {
        foreach (GameObject openShop in SpawnManager.instance.GetOpenShops())
        {
            if (Vector3.Distance(this.transform.position, openShop.transform.position) <= range)
            {
                ShopScript shop = openShop.GetComponent<ShopScript>();
                shop.Enable = false;
            }
        }
    }

    private void DealAmmo() {
        Debug.Log("DealAmmo");

        GameObject[] players = new GameObject[4];

        players[0] = GameManager.instance.Player1;
        players[1] = GameManager.instance.Player2;
        players[2] = GameManager.instance.Player3;
        players[3] = GameManager.instance.Player4;

        for (int i = 0; i < players.Length; i++) {

            if (players[i].GetComponent<PlayerController>().playerId != this.playerId && Vector3.Distance(transform.position, players[i].transform.position) <= range) {
                players[i].GetComponent<PlayerController>().sacADos = maxSacADos;
                Debug.Log("AMMO DONNE !");
                break;
            }
        }

    }

    private void Reload() {
        Debug.Log("Reload");
        int balleRecharger = (totalBullet - bullet);

        int balles = Mathf.Min(balleRecharger, sacADos);

        sacADos -= balles;

        bullet += balles;

        //bullet = totalBullet;
        hudScript.SetAmmoCurrentValue(bullet);
        hudScript.SetAmmoMax(sacADos);
    }

    public void SetShumun(ShkumunManager.Malus malus) {
        this.malus = malus;
    }

    private void Fire(int playerId) {
        if (this.malus == ShkumunManager.Malus.ShityGun)
        {
            gun.isBroken = true;
			anim.SetBool("shoot", true);
        } else
        {
            gun.isBroken = false;
        }

        if (!isRunning && bullet > 0) {
            bool fired = gun.Shoot(playerId);

            if (fired)
            {
                nbTir++;
                Debug.Log(nbTir);
                hudScript.SetAmmoCurrentValue(--bullet);
            }
        }
    }


    private void HandleAim()
    {
        float axisVal = InputManager.instance.GetAxis(playerId, keyMap, InputManager.ActionControl.AimUp);
        if (axisVal > axisThreshold)
        {
            playerCam.transform.Rotate(-Time.deltaTime * aimSpeed * axisVal, 0, 0);
        }

        axisVal = InputManager.instance.GetAxis(playerId, keyMap, InputManager.ActionControl.AimDown);
        if (axisVal < -axisThreshold)
        {
            playerCam.transform.Rotate(-Time.deltaTime * aimSpeed * axisVal, 0, 0);
        }

        axisVal = InputManager.instance.GetAxis(playerId, keyMap, InputManager.ActionControl.AimRight);
        if (axisVal < -axisThreshold)
        {
            this.transform.Rotate(0, -Time.deltaTime * aimSpeed * axisVal, 0);
        }

        axisVal = InputManager.instance.GetAxis(playerId, keyMap, InputManager.ActionControl.AimLeft);
        if (axisVal > axisThreshold)
        {
            this.transform.Rotate(0, -Time.deltaTime * aimSpeed * axisVal, 0);
        }
    }

    private void HandleMotion()
    {
        float actualSpeed = moveSpeed;
        if (isRunning)
        {
            actualSpeed *= runFactor;
        }
        if (this.malus == ShkumunManager.Malus.Legg)
        {
            actualSpeed *= leggMalusFactor;
        }

        if (InputManager.instance.GetAxis(playerId, keyMap, InputManager.ActionControl.MoveFwd) > axisThreshold)
        {
            anim.SetFloat("speed", 1);
            
            this.transform.position += transform.forward * actualSpeed * Time.deltaTime;
        }
        if (InputManager.instance.GetAxis(playerId, keyMap, InputManager.ActionControl.MoveBck) < -axisThreshold)
        {
            anim.SetFloat("speed", -1);
            
            this.transform.position -= transform.forward * actualSpeed * Time.deltaTime;
        }
        if (InputManager.instance.GetAxis(playerId, keyMap, InputManager.ActionControl.StraffRight) < -axisThreshold)
        {
            anim.SetFloat("speed", 1);
            
            this.transform.position += transform.right * actualSpeed * Time.deltaTime;
        }
        if (InputManager.instance.GetAxis(playerId, keyMap, InputManager.ActionControl.StraffLeft) > axisThreshold)
        {
            anim.SetFloat("speed", -1);
            this.transform.position -= transform.right * actualSpeed * Time.deltaTime;
        }
    }
}
