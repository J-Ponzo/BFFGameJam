using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDCoopManager : MonoBehaviour {

    private float timePerso;
	// Use this for initialization
	void Start () {
        timePerso = Time.time;
        SetKillCoop(20);
    }
	
	// Update is called once per frame
	void Update () {
        SetTimer(Time.time - timePerso);        
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

    public void decrementerVie(int degat)
    {
        GameObject kill_Coop = transform.Find("k_Coop_Value").gameObject;
        Text kill_Coop_text = kill_Coop.GetComponent<Text>();
        Debug.Log(kill_Coop_text);
        int i = Int32.Parse(kill_Coop_text.text);
        if (i-degat >= 0)
        {
            kill_Coop_text.text = (i - degat).ToString();
        }            
        else
        {
            SequenceManager.instance.LoadSequence(SequenceManager.Sequence.Menus);
        }
    }
}
