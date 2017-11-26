using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    public Transform bullet;
    [SerializeField]
    private AudioClip shootSound;
    private AudioSource source;
    [SerializeField]
    private float vollowRange = 0.5f;
    [SerializeField]
    private float volHighRange = 1.0f; 

    // Use this for initialization
    void Start() {
        Debug.Log(bullet.transform.position);
        source = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update() {
    }

    public void Shoot() {
        source.PlayOneShot(shootSound, 1F);
        Instantiate(bullet, transform.position,transform.rotation);

    }


    
}

