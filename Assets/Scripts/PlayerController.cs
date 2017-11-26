using System;
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

    [SerializeField]
    private float moveSpeed = 3f;
    [SerializeField]
    private float aimSpeed = 90f;
    [SerializeField]
    private float axisThreshold = 0.1f;

    private GameObject playerCam;

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
        playerCam = GetComponentInChildren<Camera>().gameObject;

        GameObject hud = Instantiate(hudPfb);
        hud.transform.SetParent(this.transform);
        Canvas hudCanvas = hud.GetComponent<Canvas>();
        hudCanvas.worldCamera = playerCam.GetComponent<Camera>();
        hudCanvas.planeDistance = 0.35f;
    }
	
	// Update is called once per frame
	void Update () {
        HandleMotion();
        HandleAim();
        HandleAction();
    }

    private void HandleAction()
    {
        if (InputManager.instance.GetKeyDown(playerId, keyMap, InputManager.ActionControl.Fire))
        {
            Fire();
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
        Debug.Log("StopRunning");
    }

    private void StartRunning()
    {
        Debug.Log("StartRunning");
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

    private void CallComander()
    {
        Debug.Log("CallComander");
    }

    private void Heal()
    {
        Debug.Log("Heal");
    }

    private void CloseDoor()
    {
        Debug.Log("CloseDoor");
    }

    private void DealAmmo()
    {
        Debug.Log("DealAmmo");
    }

    private void Reload()
    {
        Debug.Log("Reload");
    }

    private void Fire()
    {
        Debug.Log("Fire");
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
        if (InputManager.instance.GetAxis(playerId, keyMap, InputManager.ActionControl.MoveFwd) > axisThreshold)
        {
            this.transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        if (InputManager.instance.GetAxis(playerId, keyMap, InputManager.ActionControl.MoveBck) < -axisThreshold)
        {
            this.transform.position -= transform.forward * moveSpeed * Time.deltaTime;
        }
        if (InputManager.instance.GetAxis(playerId, keyMap, InputManager.ActionControl.StraffRight) < -axisThreshold)
        {
            this.transform.position += transform.right * moveSpeed * Time.deltaTime;
        }
        if (InputManager.instance.GetAxis(playerId, keyMap, InputManager.ActionControl.StraffLeft) > axisThreshold)
        {
            this.transform.position -= transform.right * moveSpeed * Time.deltaTime;
        }
    }
}
