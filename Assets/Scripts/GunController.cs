using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    public Transform bullet;
    public float ShootRate = 0.5f;
    private float nextfire = 0f;
    // Use this for initialization
    void Start() {
        Debug.Log(bullet.transform.position);
    }

    // Update is called once per frame
    void Update() {
    }

    public void Shoot() {
        if(Time.time > nextfire) {
            nextfire = Time.time + ShootRate;
            Instantiate(bullet, transform.position, transform.rotation);
        }
    }


    
}

