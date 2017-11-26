using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

    public float playerTotalLife;
    public float playerCurrentLife;

    private float timeBeforeHeal;
    private float recoveryTimeHeal = 2;
   
    public Bulle bulleCree;
    public ParticleSystem explosion;

    enum Declencheurs {FLECHETTES, SHOOT, K_COOP, K_SOLO, MIN, CLIENT1, TREIZE, STUN, STUCK, CLIENT2, POURCENT1, PARTIE1, PARTIE2, GEEK, WOMEN, MEN, SECONDE, RECOVER, DISTRI, DOORS, CALL};

    // Use this for initialization
    void Start () {       
        playerCurrentLife = playerTotalLife;
	}
	
	// Update is called once per frame
	void Update () {
                            
        if (Input.GetKeyDown(KeyCode.Space)) {
            BulleCreation();
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

    //Création de la popup
    void BulleCreation()
    {
        Transform transClone = this.transform;
        transClone.localScale = new Vector3(1, 1, 1);
        Bulle clone = (Bulle)Instantiate(bulleCree, this.transform, true);
        clone.timeoutDestructor = 1.5f;

        int rand = Random.Range(0,(int)Declencheurs.CALL);

        clone.SetText(TextResolver((Declencheurs)rand));
    }

    //Création de lexplosion lors de la destruction de la popup
    public void BulleExplosion()
    {
        ParticleSystem clone = (ParticleSystem)Instantiate(explosion, this.transform, true);
        Destroy(clone.gameObject, 5);
    }

    string TextResolver(Declencheurs textEnum)
    {
        string textRes = "";
        switch (textEnum)
        {
            case Declencheurs.CALL:
                textRes = "13 APPELS DU CHEF";                
                break;
            case Declencheurs.CLIENT1:
                textRes = "13 CLIENTS";                
                break;
            case Declencheurs.CLIENT2:
                textRes = "13 CLIENTS 2";                
                break;
            case Declencheurs.DISTRI:
                textRes = "13 LIVRAISONS";                
                break;
            case Declencheurs.DOORS:
                textRes = "13 PORTES";                
                break;
            case Declencheurs.FLECHETTES:
                textRes = "13 FLECHETTES";                
                break;
            case Declencheurs.GEEK:
                textRes = "13 GEEK";                
                break;
            case Declencheurs.K_COOP:
                textRes = "13 STOP COOP";                
                break;
            case Declencheurs.K_SOLO:
                textRes = "13 STOP SOLO";                
                break;
            case Declencheurs.MEN:
                textRes = "13 HOMMES";                
                break;
            case Declencheurs.MIN:
                textRes = "13 MINUTES";                
                break;
            case Declencheurs.PARTIE1:
                textRes = "13 PARTIE";                
                break;
            case Declencheurs.PARTIE2:
                textRes = "13 PARTIE 2";                
                break;
            case Declencheurs.POURCENT1:
                textRes = "13 POURCENTS";                
                break;
            case Declencheurs.RECOVER:
                textRes = "13 RECOVER";                
                break;
            case Declencheurs.SECONDE:
                textRes = "13 SECONDES";                
                break;
            case Declencheurs.SHOOT:
                textRes = "13 SHOOTS";                
                break;
            case Declencheurs.STUCK:
                textRes = "13 GUN FAILS";                
                break;
            case Declencheurs.STUN:
                textRes = "13 STUN";                
                break;
            case Declencheurs.TREIZE:
                textRes = "13 x 13";               
                break;
            case Declencheurs.WOMEN:
                textRes = "13 FEMMES";                
                break;           
            default:
                Debug.Log("Default case");
                break;
        }

        return textRes;
    }

    public void SetPrecisionValue(float precisionValue)
    {
        GameObject precision = transform.Find("CanvasPrecision").gameObject;
        Text[] precisionText = precision.GetComponentsInChildren<Text>();
        Debug.Log(precisionText[1]);
        precisionText[1].text = precisionValue.ToString()+"%";
    }

    public void SetNeutralisationValue(int neutralisationValue)
    {
        GameObject neutralisation = transform.Find("CanvasNeutralisation").gameObject;
        Text[] neutralisationText = neutralisation.GetComponentsInChildren<Text>();
        Debug.Log(neutralisationText[1]);
        neutralisationText[1].text = neutralisationValue.ToString();
    }

    public void SetScoreValue(int scoreValue)
    {
        GameObject score = transform.Find("CanvasScore").gameObject;
        Text[] scoreText = score.GetComponentsInChildren<Text>();
        Debug.Log(scoreText[1]);
        scoreText[1].text = scoreValue.ToString();
    }

    public void SetAmmoCurrentValue(int ammoCurrentValue)
    {
        GameObject ammoCurrent = transform.Find("CanvasAmmo").gameObject;
        Text[] ammoCurrentText = ammoCurrent.GetComponentsInChildren<Text>();
        Debug.Log(ammoCurrentText[0]);
        ammoCurrentText[0].text = ammoCurrentValue.ToString();
    }

    public void SetAmmoMax(int ammoMax) {
        GameObject totalAmmo = transform.Find("CanvasAmmo").gameObject;
        Text[] totalAmmoText = totalAmmo.GetComponentsInChildren<Text>();
        Debug.Log(totalAmmoText[2]);
        totalAmmoText[2].text = ammoMax.ToString();
    }
}