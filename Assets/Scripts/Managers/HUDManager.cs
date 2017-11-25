using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour {

    public float playerTotalLife;
    public float playerCurrentLife;

    private float timeBeforeHeal;
    private float recoveryTimeHeal = 3;

    // Use this for initialization
    void Start () {       
        playerCurrentLife = playerTotalLife;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            TakeDamage(5);
        } else
        {
            TakeLife(0.1f);
        }
	}


    //Diminue la bar de Stun du joueur
    void TakeDamage(/*Player player,*/ float damage)
    {
        if (playerCurrentLife > 0)
        {
            playerCurrentLife = Mathf.Max(playerCurrentLife-damage, 0);
        }
        
        GameObject lifeBar = GameObject.Find("LifeBarCurrent");
        lifeBar.transform.localScale = new Vector3((playerCurrentLife / playerTotalLife), 1, 1);
        timeBeforeHeal = Time.time + recoveryTimeHeal;
    }

    //Recharche la bar de Stun du joueur
    void TakeLife(float life)
    {
        if (playerCurrentLife < playerTotalLife && timeBeforeHeal < Time.time)
        {            
            playerCurrentLife += life;
        }            
        GameObject lifeBar = GameObject.Find("LifeBarCurrent");
        lifeBar.transform.localScale = new Vector3((playerCurrentLife / playerTotalLife), 1, 1);
    }
}