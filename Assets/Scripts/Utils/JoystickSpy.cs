using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickSpy : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            Debug.Log("0");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            Debug.Log("1");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            Debug.Log("2");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            Debug.Log("3");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton4))
        {
            Debug.Log("4");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton5))
        {
            Debug.Log("5");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton6))
        {
            Debug.Log("6");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            Debug.Log("7");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton8))
        {
            Debug.Log("8");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton9))
        {
            Debug.Log("9");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton10))
        {
            Debug.Log("10");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton11))
        {
            Debug.Log("11");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton12))
        {
            Debug.Log("12");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton13))
        {
            Debug.Log("13");
        }

        float val = Input.GetAxis("Spy_Axis");
        if (val > 0.1f ||  val < 0.1f)
        {
            Debug.Log(val);
        }
    }
}