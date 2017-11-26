using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField]
    private AudioClip CrashEnnemy;
    [SerializeField]
    private AudioClip CrashWall;
    [SerializeField]
    private GameObject audioScream;

    public int playerID;

    AudioSource audiosource;
    


    public float bulletSpeed = 5f;

    public void SetPlayerId(int iD)
    {
        playerID = iD;
    }

    private void Awake()
    {
        
    }

    // Use this for initialization
    void Start() {
        audiosource = GetComponent<AudioSource>();
        Destroy(gameObject, 3);
        
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(Vector3.forward);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject actualPlayer = null;

        if (collision.gameObject.tag == "Player")
        {
            return;
        }
        if (collision.gameObject.tag == "Enemy")
        {
            GameObject inst = Instantiate(audioScream, transform.parent, true);
            inst.GetComponent<AudioSource>().PlayOneShot(CrashEnnemy, 0.001f);

            switch (playerID)
            {
                case 0:
                   actualPlayer= GameManager.instance.Player1;
                    break;
                case 1:
                    actualPlayer = GameManager.instance.Player1;
                    break;
                case 2:
                    actualPlayer = GameManager.instance.Player1;
                    break;
                case 3:
                    actualPlayer = GameManager.instance.Player1;
                    break;
                
            }

            actualPlayer.GetComponentInChildren<HUDManager>().ScoreIncrement(10);
            actualPlayer.GetComponentInChildren<HUDManager>().NeutralisationIncrement();
            int neutr = actualPlayer.GetComponentInChildren<HUDManager>().GetNeutralisation();
            float res = neutr / (actualPlayer.GetComponent<PlayerController>().nbTir + 1);
            actualPlayer.GetComponentInChildren<HUDManager>().SetPrecisionValue(res);

            Destroy(inst, 2.0f);

            Destroy(collision.gameObject);
        }
        else
        {
            GameObject inst = Instantiate(audioScream, transform.parent, true);
            inst.GetComponent<AudioSource>().PlayOneShot(CrashWall, 0.2f);
            Destroy(inst, 2.0f);


            Destroy(gameObject);
        }
    }
}
