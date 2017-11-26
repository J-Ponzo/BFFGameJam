using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDCoopManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        SetTimer(Time.time);
        SetKillCoop(0);
	}

    public void SetTimer(float time)
    {
        GameObject timer = transform.Find("TimerValue").gameObject;
        Text timerText = timer.GetComponent<Text>();
        Debug.Log(timerText);
        timerText.text = ((int)time).ToString();
    }

    public void SetKillCoop(int kill)
    {
        GameObject kill_Coop = transform.Find("k_Coop_Value").gameObject;
        Text kill_Coop_text = kill_Coop.GetComponent<Text>();
        Debug.Log(kill_Coop_text);
        kill_Coop_text.text = (kill).ToString();
    }
}
