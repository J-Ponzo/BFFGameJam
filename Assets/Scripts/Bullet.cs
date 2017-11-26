using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {


    public float bulletSpeed = 5f;
    // Use this for initialization
    void Start() {
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
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
}
