using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {
    public bool isBroken = false;
    public Transform bullet;
    public float ShootRate = 0.5f;
    private float lastShoot = 0f;
    [SerializeField]
    private AudioClip shootSound;
    private AudioSource source;
    [SerializeField]
    private float vollowRange = 0.5f;
    [SerializeField]
    private float volHighRange = 1.0f;
    [SerializeField]
    private float chance = 0.13f;

    // Use this for initialization
    void Start() {
        Debug.Log(bullet.transform.position);
        source = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update() {
        lastShoot += Time.deltaTime;
    }

    public bool Shoot(int playerId) {
        if (lastShoot > ShootRate)
        {
            if (isBroken)
            {
                float rand = Random.value;
                if (rand > chance)
                {
                    return false;
                }
            }
            Transform clone = Instantiate(bullet, transform.position + transform.forward, transform.rotation);
            Bullet bulletClone = clone.GetComponent<Bullet>();
            bulletClone.SetPlayerId(playerId);
            lastShoot = 0f;
            float vol = Random.Range(vollowRange, volHighRange);
            source.PlayOneShot(shootSound, vol);
            return true;
        }
        return false;
    }


    
}

