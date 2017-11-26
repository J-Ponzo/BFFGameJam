using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    public Transform bullet;
    // Use this for initialization
    void Start() {
        Debug.Log(bullet.transform.position);
    }

    // Update is called once per frame
    void Update() {
    }

    public void Shoot() {
        Instantiate(bullet, transform.position,transform.rotation);
    }


    
}

