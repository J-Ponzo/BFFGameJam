using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    public Transform bullet;
    public float ShootRate = 0.5f;
    private float nextfire = 0f;
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
        if(Time.time > nextfire) {
            Instantiate(bullet, transform.position, transform.rotation);
            nextfire = Time.time + ShootRate;
            source.PlayOneShot(shootSound, 1F);
    }


    
}

