using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    /// <summary>
    /// The static instance of the Singleton for external access
    /// </summary>
    public static InputManager instance = null;

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
        initJoystickIdResultutionTab();
        DontDestroyOnLoad(this.gameObject);
    }

    public enum ActionControl
    {
        MoveFwd,
        MoveBck,
        StraffRight,
        StraffLeft,
        AimRight,
        AimLeft,
        AimUp,
        AimDown,
        Fire,
        Reload,
        Action,
        Run,
        Pause
    }

    public enum KeyMapping
    {
        Disabled,
        KeyBoard,
        LogitechDualAction,
        LogitechF310,
        XBox360,
        PS3,    //Non supporté
    }

    [SerializeField]
    private int[] joysticResTable = { -1, -1, -1, -1 };

    public int[] JoysticResTable
    {
        get
        {
            return joysticResTable;
        }

        set
        {
            joysticResTable = value;
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    private void initJoystickIdResultutionTab()
    {
        int curInd = 0;
        String[] joystickNames = Input.GetJoystickNames();
        for (int i = 0; i < joystickNames.Length; i++)
        {
            if (curInd >= 4)
            {
                break;
            }
            if (joystickNames[i] != null && joystickNames[i] != "")
            {
                JoysticResTable[curInd++] = i;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }



    public String GetJoystickNameFromPlayerId(int playerId)
    {
        int joystickId = InputManager.instance.JoysticResTable[playerId];
        if (joystickId == -1)
        {
            return null;
        }

        String joystickName = Input.GetJoystickNames()[joystickId];
        return joystickName;
    }

    public KeyMapping GetKeymapFromJoytickName(String joystikName)
    {
        switch (joystikName)
        {
            case "Controller (XBOX 360 For Windows)":
                return KeyMapping.XBox360;
            case "Controller (Gamepad F310)":
                return KeyMapping.LogitechF310;
            case "Logitech Dual Action":
                return KeyMapping.LogitechDualAction;
            case "PLAYSTATION(R)3 Controller":
                return KeyMapping.PS3;
        }
        Debug.Log(joystikName + " Not recognized. XBox Map setted by default");
        return KeyMapping.XBox360;
    }

    public float GetAxis(int playerId, KeyMapping keyMap, ActionControl action)
    {
        if (keyMap == KeyMapping.KeyBoard)
        {
            bool isAction = GetKey(playerId, keyMap, action);
            if (isAction)
            {
                switch (action)
                {
                    case ActionControl.MoveFwd:
                        return 1;
                    case ActionControl.MoveBck:
                        return -1;
                    case ActionControl.StraffRight:
                        return -1;
                    case ActionControl.StraffLeft:
                        return 1;
                }
            }
            else
            {
                switch (action)
                {
                    case ActionControl.AimRight:
                    case ActionControl.AimLeft:
                        return -Input.GetAxis("Mouse X");
                    case ActionControl.AimUp:
                    case ActionControl.AimDown:
                        return Input.GetAxis("Mouse Y");
                }
            }
        }
        else
        {
            String axisName = ResolveAxis(playerId, keyMap, action);
            return -Input.GetAxis(axisName);
        }

        return 0;
    }

    public bool GetKeyDown(int playerId, KeyMapping keyMap, ActionControl action)
    {
        KeyCode keyCode = ResolveKeyCode(playerId, keyMap, action);
        if (keyCode != KeyCode.None)
        {
            return Input.GetKeyDown(keyCode);
        }
        return false;
    }

    public bool GetKeyUp(int playerId, KeyMapping keyMap, ActionControl action)
    {
        KeyCode keyCode = ResolveKeyCode(playerId, keyMap, action);
        if (keyCode != KeyCode.None)
        {
            return Input.GetKeyUp(keyCode);
        }
        return false;
    }

    public bool GetKey(int playerId, KeyMapping keyMap, ActionControl action)
    {
        KeyCode keyCode = ResolveKeyCode(playerId, keyMap, action);
        if (keyCode != KeyCode.None)
        {
            return Input.GetKey(keyCode);
        }
        return false;
    }

    private string ResolveAxis(int playerId, KeyMapping keyMap, ActionControl action)
    {
        String axisName = null;
        switch (keyMap)
        {
            case KeyMapping.LogitechDualAction:
                axisName = ResolveLogitechDualActionAxis(playerId, action);
                break;
            case KeyMapping.LogitechF310:
                axisName = ResolveLogitechF310Axis(playerId, action);
                break;
            case KeyMapping.XBox360:
                axisName = ResolveXBox360Axis(playerId, action);
                break;
            case KeyMapping.PS3:
                axisName = ResolvePS3Axis(playerId, action);
                break;
        }
        return axisName;
    }

    private string ResolveXBox360Axis(int playerId, ActionControl action)
    {
        String axisName = null;
        switch (action)
        {
            case ActionControl.MoveFwd:
            case ActionControl.MoveBck:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return "Joystick1_Move_Y";
                    case 1:
                        return "Joystick2_Move_Y";
                    case 2:
                        return "Joystick3_Move_Y";
                    case 3:
                        return "Joystick4_Move_Y";
                    case 4:
                        return "Joystick5_Move_Y";
                    case 5:
                        return "Joystick6_Move_Y";
                    case 6:
                        return "Joystick7_Move_Y";
                    case 7:
                        return "Joystick8_Move_Y";
                }
                break;
            case ActionControl.StraffRight:
            case ActionControl.StraffLeft:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return "Joystick1_Move_X";
                    case 1:
                        return "Joystick2_Move_X";
                    case 2:
                        return "Joystick3_Move_X";
                    case 3:
                        return "Joystick4_Move_X";
                    case 4:
                        return "Joystick5_Move_X";
                    case 5:
                        return "Joystick6_Move_X";
                    case 6:
                        return "Joystick7_Move_X";
                    case 7:
                        return "Joystick8_Move_X";
                }
                break;
            case ActionControl.AimRight:
            case ActionControl.AimLeft:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return "Joystick1_Aim_X";
                    case 1:
                        return "Joystick2_Aim_X";
                    case 2:
                        return "Joystick3_Aim_X";
                    case 3:
                        return "Joystick4_Aim_X";
                    case 4:
                        return "Joystick5_Aim_X";
                    case 5:
                        return "Joystick6_Aim_X";
                    case 6:
                        return "Joystick7_Aim_X";
                    case 7:
                        return "Joystick8_Aim_X";
                }
                break;
            case ActionControl.AimUp:
            case ActionControl.AimDown:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return "Joystick1_Aim_Y";
                    case 1:
                        return "Joystick2_Aim_Y";
                    case 2:
                        return "Joystick3_Aim_Y";
                    case 3:
                        return "Joystick4_Aim_Y";
                    case 4:
                        return "Joystick5_Aim_Y";
                    case 5:
                        return "Joystick6_Aim_Y";
                    case 6:
                        return "Joystick7_Aim_Y";
                    case 7:
                        return "Joystick8_Aim_Y";
                }
                break;
        }
        return axisName;
    }

    private string ResolveLogitechF310Axis(int playerId, ActionControl action)
    {
        String axisName = null;
        switch (action)
        {
            case ActionControl.MoveFwd:
            case ActionControl.MoveBck:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return "Joystick1_Move_Y";
                    case 1:
                        return "Joystick2_Move_Y";
                    case 2:
                        return "Joystick3_Move_Y";
                    case 3:
                        return "Joystick4_Move_Y";
                    case 4:
                        return "Joystick5_Move_Y";
                    case 5:
                        return "Joystick6_Move_Y";
                    case 6:
                        return "Joystick7_Move_Y";
                    case 7:
                        return "Joystick8_Move_Y";
                }
                break;
            case ActionControl.StraffRight:
            case ActionControl.StraffLeft:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return "Joystick1_Move_X";
                    case 1:
                        return "Joystick2_Move_X";
                    case 2:
                        return "Joystick3_Move_X";
                    case 3:
                        return "Joystick4_Move_X";
                    case 4:
                        return "Joystick5_Move_X";
                    case 5:
                        return "Joystick6_Move_X";
                    case 6:
                        return "Joystick7_Move_X";
                    case 7:
                        return "Joystick8_Move_X";
                }
                break;
            case ActionControl.AimRight:
            case ActionControl.AimLeft:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return "Joystick1_Aim_X";
                    case 1:
                        return "Joystick2_Aim_X";
                    case 2:
                        return "Joystick3_Aim_X";
                    case 3:
                        return "Joystick4_Aim_X";
                    case 4:
                        return "Joystick5_Aim_X";
                    case 5:
                        return "Joystick6_Aim_X";
                    case 6:
                        return "Joystick7_Aim_X";
                    case 7:
                        return "Joystick8_Aim_X";
                }
                break;
            case ActionControl.AimUp:
            case ActionControl.AimDown:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return "Joystick1_Aim_Y";
                    case 1:
                        return "Joystick2_Aim_Y";
                    case 2:
                        return "Joystick3_Aim_Y";
                    case 3:
                        return "Joystick4_Aim_Y";
                    case 4:
                        return "Joystick5_Aim_Y";
                    case 5:
                        return "Joystick6_Aim_Y";
                    case 6:
                        return "Joystick7_Aim_Y";
                    case 7:
                        return "Joystick8_Aim_Y";
                }
                break;
        }
        return axisName;
    }

    private string ResolveLogitechDualActionAxis(int playerId, ActionControl action)
    {
        String axisName = null;
        switch (action)
        {
            case ActionControl.MoveFwd:
            case ActionControl.MoveBck:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return "Joystick1_Move_Y";
                    case 1:
                        return "Joystick2_Move_Y";
                    case 2:
                        return "Joystick3_Move_Y";
                    case 3:
                        return "Joystick4_Move_Y";
                    case 4:
                        return "Joystick5_Move_Y";
                    case 5:
                        return "Joystick6_Move_Y";
                    case 6:
                        return "Joystick7_Move_Y";
                    case 7:
                        return "Joystick8_Move_Y";
                }
                break;
            case ActionControl.StraffRight:
            case ActionControl.StraffLeft:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return "Joystick1_Move_X";
                    case 1:
                        return "Joystick2_Move_X";
                    case 2:
                        return "Joystick3_Move_X";
                    case 3:
                        return "Joystick4_Move_X";
                    case 4:
                        return "Joystick5_Move_X";
                    case 5:
                        return "Joystick6_Move_X";
                    case 6:
                        return "Joystick7_Move_X";
                    case 7:
                        return "Joystick8_Move_X";
                }
                break;
            case ActionControl.AimRight:
            case ActionControl.AimLeft:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return "Logitech_DualAction_1_Aim_X";
                    case 1:
                        return "Logitech_DualAction_2_Aim_X";
                    case 2:
                        return "Logitech_DualAction_3_Aim_X";
                    case 3:
                        return "Logitech_DualAction_4_Aim_X";
                    case 4:
                        return "Logitech_DualAction_5_Aim_X";
                    case 5:
                        return "Logitech_DualAction_6_Aim_X";
                    case 6:
                        return "Logitech_DualAction_7_Aim_X";
                    case 7:
                        return "Logitech_DualAction_8_Aim_X";
                }
                break;
                case ActionControl.AimUp:
            case ActionControl.AimDown:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return "Logitech_DualAction_1_Aim_Y";
                    case 1:
                        return "Logitech_DualAction_2_Aim_Y";
                    case 2:
                        return "Logitech_DualAction_3_Aim_Y";
                    case 3:
                        return "Logitech_DualAction_4_Aim_Y";
                    case 4:
                        return "Logitech_DualAction_5_Aim_Y";
                    case 5:
                        return "Logitech_DualAction_6_Aim_Y";
                    case 6:
                        return "Logitech_DualAction_7_Aim_Y";
                    case 7:
                        return "Logitech_DualAction_8_Aim_Y";
                }
                break;
        }
        return axisName;
    }

    private string ResolvePS3Axis(int playerId, ActionControl action)
    {
        String axisName = null;
        switch (action)
        {
            case ActionControl.MoveFwd:
            case ActionControl.MoveBck:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return "Joystick1_Move_Y";
                    case 1:
                        return "Joystick2_Move_Y";
                    case 2:
                        return "Joystick3_Move_Y";
                    case 3:
                        return "Joystick4_Move_Y";
                    case 4:
                        return "Joystick5_Move_Y";
                    case 5:
                        return "Joystick6_Move_Y";
                    case 6:
                        return "Joystick7_Move_Y";
                    case 7:
                        return "Joystick8_Move_Y";
                }
                break;
            case ActionControl.AimRight:
            case ActionControl.AimLeft:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return "Joystick1_Aim_X";
                    case 1:
                        return "Joystick2_Aim_X";
                    case 2:
                        return "Joystick3_Aim_X";
                    case 3:
                        return "Joystick4_Aim_X";
                    case 4:
                        return "Joystick5_Aim_X";
                    case 5:
                        return "Joystick6_Aim_X";
                    case 6:
                        return "Joystick7_Aim_X";
                    case 7:
                        return "Joystick8_Aim_X";
                }
                break;
            case ActionControl.AimUp:
            case ActionControl.AimDown:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return "Joystick1_Aim_Y";
                    case 1:
                        return "Joystick2_Aim_Y";
                    case 2:
                        return "Joystick3_Aim_Y";
                    case 3:
                        return "Joystick4_Aim_Y";
                    case 4:
                        return "Joystick5_Aim_Y";
                    case 5:
                        return "Joystick6_Aim_Y";
                    case 6:
                        return "Joystick7_Aim_Y";
                    case 7:
                        return "Joystick8_Aim_Y";
                }
                break;
        }
        return axisName;
    }

    private KeyCode ResolveKeyCode(int playerId, KeyMapping keyMap, ActionControl action)
    {
        KeyCode keyCode = KeyCode.None;
        switch (keyMap)
        {
            case KeyMapping.KeyBoard:
                keyCode = ResolveKeyBoardKeyCode(action);
                break;
            case KeyMapping.LogitechDualAction:
                keyCode = ResolveLogitechDualActionKeyCode(playerId, action);
                break;
            case KeyMapping.LogitechF310:
                keyCode = ResolveLogitechF310KeyCode(playerId, action);
                break;
            case KeyMapping.XBox360:
                keyCode = ResolveXBox360KeyCode(playerId, action);
                break;
            case KeyMapping.PS3:
                keyCode = ResolvePS3KeyCode(playerId, action);
                break;
        }
        return keyCode;
    }

    private KeyCode ResolveXBox360KeyCode(int playerId, ActionControl action)
    {
        switch (action)
        {
            case ActionControl.Fire:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return KeyCode.Joystick1Button5;
                    case 1:
                        return KeyCode.Joystick2Button5;
                    case 2:
                        return KeyCode.Joystick3Button5;
                    case 3:
                        return KeyCode.Joystick4Button5;
                    case 4:
                        return KeyCode.Joystick5Button5;
                    case 5:
                        return KeyCode.Joystick6Button5;
                    case 6:
                        return KeyCode.Joystick7Button5;
                    case 7:
                        return KeyCode.Joystick8Button5;
                    default:
                        return KeyCode.None;
                }
            case ActionControl.Reload:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return KeyCode.Joystick1Button2;
                    case 1:
                        return KeyCode.Joystick2Button2;
                    case 2:
                        return KeyCode.Joystick3Button2;
                    case 3:
                        return KeyCode.Joystick4Button2;
                    case 4:
                        return KeyCode.Joystick5Button2;
                    case 5:
                        return KeyCode.Joystick6Button2;
                    case 6:
                        return KeyCode.Joystick7Button2;
                    case 7:
                        return KeyCode.Joystick8Button2;
                    default:
                        return KeyCode.None;
                }
            case ActionControl.Action:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return KeyCode.Joystick1Button3;
                    case 1:
                        return KeyCode.Joystick2Button3;
                    case 2:
                        return KeyCode.Joystick3Button3;
                    case 3:
                        return KeyCode.Joystick4Button3;
                    case 4:
                        return KeyCode.Joystick5Button3;
                    case 5:
                        return KeyCode.Joystick6Button3;
                    case 6:
                        return KeyCode.Joystick7Button3;
                    case 7:
                        return KeyCode.Joystick8Button3;
                    default:
                        return KeyCode.None;
                }
            case ActionControl.Run:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return KeyCode.Joystick1Button4;
                    case 1:
                        return KeyCode.Joystick2Button4;
                    case 2:
                        return KeyCode.Joystick3Button4;
                    case 3:
                        return KeyCode.Joystick4Button4;
                    case 4:
                        return KeyCode.Joystick5Button4;
                    case 5:
                        return KeyCode.Joystick6Button4;
                    case 6:
                        return KeyCode.Joystick7Button4;
                    case 7:
                        return KeyCode.Joystick8Button4;
                    default:
                        return KeyCode.None;
                }
            case ActionControl.Pause:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return KeyCode.Joystick1Button7;
                    case 1:
                        return KeyCode.Joystick2Button7;
                    case 2:
                        return KeyCode.Joystick3Button7;
                    case 3:
                        return KeyCode.Joystick4Button7;
                    case 4:
                        return KeyCode.Joystick5Button7;
                    case 5:
                        return KeyCode.Joystick6Button7;
                    case 6:
                        return KeyCode.Joystick7Button7;
                    case 7:
                        return KeyCode.Joystick8Button7;
                    default:
                        return KeyCode.None;
                }
        }
        return KeyCode.None;
    }

    private KeyCode ResolveLogitechF310KeyCode(int playerId, ActionControl action)
    {
        switch (action)
        {
            case ActionControl.Fire:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return KeyCode.Joystick1Button5;
                    case 1:
                        return KeyCode.Joystick2Button5;
                    case 2:
                        return KeyCode.Joystick3Button5;
                    case 3:
                        return KeyCode.Joystick4Button5;
                    case 4:
                        return KeyCode.Joystick5Button5;
                    case 5:
                        return KeyCode.Joystick6Button5;
                    case 6:
                        return KeyCode.Joystick7Button5;
                    case 7:
                        return KeyCode.Joystick8Button5;
                    default:
                        return KeyCode.None;
                }
            case ActionControl.Reload:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return KeyCode.Joystick1Button2;
                    case 1:
                        return KeyCode.Joystick2Button2;
                    case 2:
                        return KeyCode.Joystick3Button2;
                    case 3:
                        return KeyCode.Joystick4Button2;
                    case 4:
                        return KeyCode.Joystick5Button2;
                    case 5:
                        return KeyCode.Joystick6Button2;
                    case 6:
                        return KeyCode.Joystick7Button2;
                    case 7:
                        return KeyCode.Joystick8Button2;
                    default:
                        return KeyCode.None;
                }
            case ActionControl.Action:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return KeyCode.Joystick1Button3;
                    case 1:
                        return KeyCode.Joystick2Button3;
                    case 2:
                        return KeyCode.Joystick3Button3;
                    case 3:
                        return KeyCode.Joystick4Button3;
                    case 4:
                        return KeyCode.Joystick5Button3;
                    case 5:
                        return KeyCode.Joystick6Button3;
                    case 6:
                        return KeyCode.Joystick7Button3;
                    case 7:
                        return KeyCode.Joystick8Button3;
                    default:
                        return KeyCode.None;
                }
            case ActionControl.Run:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return KeyCode.Joystick1Button4;
                    case 1:
                        return KeyCode.Joystick2Button4;
                    case 2:
                        return KeyCode.Joystick3Button4;
                    case 3:
                        return KeyCode.Joystick4Button4;
                    case 4:
                        return KeyCode.Joystick5Button4;
                    case 5:
                        return KeyCode.Joystick6Button4;
                    case 6:
                        return KeyCode.Joystick7Button4;
                    case 7:
                        return KeyCode.Joystick8Button4;
                    default:
                        return KeyCode.None;
                }
            case ActionControl.Pause:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return KeyCode.Joystick1Button7;
                    case 1:
                        return KeyCode.Joystick2Button7;
                    case 2:
                        return KeyCode.Joystick3Button7;
                    case 3:
                        return KeyCode.Joystick4Button7;
                    case 4:
                        return KeyCode.Joystick5Button7;
                    case 5:
                        return KeyCode.Joystick6Button7;
                    case 6:
                        return KeyCode.Joystick7Button7;
                    case 7:
                        return KeyCode.Joystick8Button7;
                    default:
                        return KeyCode.None;
                }
        }
        return KeyCode.None;
    }

    private KeyCode ResolveLogitechDualActionKeyCode(int playerId, ActionControl action)
    {
        switch (action)
        {
            case ActionControl.Fire:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return KeyCode.Joystick1Button5;
                    case 1:
                        return KeyCode.Joystick2Button5;
                    case 2:
                        return KeyCode.Joystick3Button5;
                    case 3:
                        return KeyCode.Joystick4Button5;
                    case 4:
                        return KeyCode.Joystick5Button5;
                    case 5:
                        return KeyCode.Joystick6Button5;
                    case 6:
                        return KeyCode.Joystick7Button5;
                    case 7:
                        return KeyCode.Joystick8Button5;
                    default:
                        return KeyCode.None;
                }
            case ActionControl.Reload:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return KeyCode.Joystick1Button0;
                    case 1:
                        return KeyCode.Joystick2Button0;
                    case 2:
                        return KeyCode.Joystick3Button0;
                    case 3:
                        return KeyCode.Joystick4Button0;
                    case 4:
                        return KeyCode.Joystick5Button0;
                    case 5:
                        return KeyCode.Joystick6Button0;
                    case 6:
                        return KeyCode.Joystick7Button0;
                    case 7:
                        return KeyCode.Joystick8Button0;
                    default:
                        return KeyCode.None;
                }
            case ActionControl.Action:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return KeyCode.Joystick1Button3;
                    case 1:
                        return KeyCode.Joystick2Button3;
                    case 2:
                        return KeyCode.Joystick3Button3;
                    case 3:
                        return KeyCode.Joystick4Button3;
                    case 4:
                        return KeyCode.Joystick5Button3;
                    case 5:
                        return KeyCode.Joystick6Button3;
                    case 6:
                        return KeyCode.Joystick7Button3;
                    case 7:
                        return KeyCode.Joystick8Button3;
                    default:
                        return KeyCode.None;
                }
            case ActionControl.Run:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return KeyCode.Joystick1Button4;
                    case 1:
                        return KeyCode.Joystick2Button4;
                    case 2:
                        return KeyCode.Joystick3Button4;
                    case 3:
                        return KeyCode.Joystick4Button4;
                    case 4:
                        return KeyCode.Joystick5Button4;
                    case 5:
                        return KeyCode.Joystick6Button4;
                    case 6:
                        return KeyCode.Joystick7Button4;
                    case 7:
                        return KeyCode.Joystick8Button4;
                    default:
                        return KeyCode.None;
                }
            case ActionControl.Pause:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return KeyCode.Joystick1Button9;
                    case 1:
                        return KeyCode.Joystick2Button9;
                    case 2:
                        return KeyCode.Joystick3Button9;
                    case 3:
                        return KeyCode.Joystick4Button9;
                    case 4:
                        return KeyCode.Joystick5Button9;
                    case 5:
                        return KeyCode.Joystick6Button9;
                    case 6:
                        return KeyCode.Joystick7Button9;
                    case 7:
                        return KeyCode.Joystick8Button9;
                    default:
                        return KeyCode.None;
                }
        }
        return KeyCode.None;
    }

    private KeyCode ResolvePS3KeyCode(int playerId, ActionControl action)
    {
        switch (action)
        {
            case ActionControl.Fire:
                switch (playerId)
                {
                    case 0:
                        return KeyCode.Joystick1Button5;
                    case 1:
                        return KeyCode.Joystick2Button5;
                    case 2:
                        return KeyCode.Joystick3Button5;
                    case 3:
                        return KeyCode.Joystick4Button5;
                    default:
                        return KeyCode.None;
                }
            case ActionControl.Reload:
                switch (playerId)
                {
                    case 0:
                        return KeyCode.Joystick1Button4;
                    case 1:
                        return KeyCode.Joystick2Button4;
                    case 2:
                        return KeyCode.Joystick3Button4;
                    case 3:
                        return KeyCode.Joystick4Button4;
                    default:
                        return KeyCode.None;
                }
        }
        return KeyCode.None;
    }

    private KeyCode ResolveKeyBoardKeyCode(ActionControl action)
    {
        switch (action)
        {
            case ActionControl.MoveFwd:
                return KeyCode.Z;
            case ActionControl.MoveBck:
                return KeyCode.S;
            case ActionControl.StraffRight:
                return KeyCode.D;
            case ActionControl.StraffLeft:
                return KeyCode.Q;
            case ActionControl.Fire:
                return KeyCode.Mouse0;
            case ActionControl.Reload:
                return KeyCode.R;
            case ActionControl.Action:
                return KeyCode.A;
            case ActionControl.Run:
                return KeyCode.LeftShift;
            case ActionControl.Pause:
                return KeyCode.Escape;
        }
        return KeyCode.None;
    }
}