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

    AudioSource audiosource;
    


    public float bulletSpeed = 5f;

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
        if (collision.gameObject.tag == "Player")
        {
            return;
        }
        if (collision.gameObject.tag == "Enemy")
        {
            GameObject inst = Instantiate(audioScream, transform.parent, true);
            inst.GetComponent<AudioSource>().PlayOneShot(CrashEnnemy, 0.001f);
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
