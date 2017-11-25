using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour {

    public float playerTotalLife;
    public float playerCurrentLife;

    private float timeBeforeHeal;
    private float recoveryTimeHeal = 2;
    public Sprite bullesTab;
    public Bulle bulleCree;
    public ParticleSystem explosion;

    // Use this for initialization
    void Start () {       
        playerCurrentLife = playerTotalLife;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            TakeDamage(5);
        } if (Input.GetKeyDown(KeyCode.Space)) {
            BulleCreation("Cool");
        }
        else
        {
            ReloadLife(0.1f);
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
    void ReloadLife(float life)
    {
        if (playerCurrentLife < playerTotalLife && timeBeforeHeal < Time.time)
        {            
            playerCurrentLife += life;
        }            
        GameObject lifeBar = GameObject.Find("LifeBarCurrent");
        lifeBar.transform.localScale = new Vector3((playerCurrentLife / playerTotalLife), 1, 1);
    }

    void BulleCreation(string text)
    {
        Transform transClone = this.transform;
        transClone.localScale = new Vector3(1, 1, 1);
        Bulle clone = (Bulle)Instantiate(bulleCree, this.transform, true);
        clone.timeoutDestructor = 1.5f;
        clone.SetText(text);
    }

    public void BulleExplosion()
    {
        ParticleSystem clone = (ParticleSystem)Instantiate(explosion, this.transform, true);
    }
}