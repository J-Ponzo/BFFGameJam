using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField]
    private AudioClip CrashEnnemy;
    [SerializeField]
    private AudioClip CrashWall; 

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
        if (collision.gameObject.tag == "Enemy")
        {
            audiosource.PlayOneShot(CrashEnnemy, 0.2F); 
            Destroy(collision.gameObject);
        }
        audiosource.PlayOneShot(CrashWall, 0.2F);
        Destroy(gameObject);
    }
}
