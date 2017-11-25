using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    private int playerId = 0;
    [SerializeField]
    private InputManager.KeyMapping keyMap = InputManager.KeyMapping.KeyBoard;

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

    // Use this for initialization
    void Start () {
        playerCam = GetComponentInChildren<Camera>().gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        HandleMotion();
        HandleAim();
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
