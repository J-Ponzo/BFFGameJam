using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShkumunManager : MonoBehaviour {
    public enum Malus
    {
        None,
        Legg,
        ShityGun,
        Invert
    }

    /// <summary>
    /// The static instance of the Singleton for external access
    /// </summary>
    public static ShkumunManager instance = null;

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
    private float shkumunFrequecy = 0.1f;
    [SerializeField]
    private float noShkumunSince = 0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        noShkumunSince += Time.deltaTime;
        if (noShkumunSince > 1f / shkumunFrequecy)
        {
            noShkumunSince = 0f;
            ThrowShkumun();
        }
	}

    private void ThrowShkumun()
    {
        Malus malus = Malus.None;
        int rand = UnityEngine.Random.Range(1, 4);
        switch(rand)
        {
            case 1:
                malus = Malus.Legg;
                break;
            case 2:
                malus = Malus.ShityGun;
                break;
            case 3:
                malus = Malus.Invert;
                break;
        }

        int playerId = UnityEngine.Random.Range(1, 4);
        PlayerController playerCtrl = null;
        switch (playerId)
        {
            case 0:
                playerCtrl = GameManager.instance.Player1.GetComponent<PlayerController>();
                break;
            case 1:
                playerCtrl = GameManager.instance.Player2.GetComponent<PlayerController>();
                break;
            case 2:
                playerCtrl = GameManager.instance.Player3.GetComponent<PlayerController>();
                break;
            case 3:
                playerCtrl = GameManager.instance.Player4.GetComponent<PlayerController>();
                break;
        }
        playerCtrl.SetShumun(malus);
    }
}
