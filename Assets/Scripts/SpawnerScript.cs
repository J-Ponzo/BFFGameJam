using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnerScript : MonoBehaviour {
    [SerializeField]
    private bool enable = false;
    [SerializeField]  
    private float frequency = 0.1f;
    [SerializeField]
    private float lastSpawn = 0;
    [SerializeField]
    private GameObject[] enemyPfb;

    public bool Enable
    {
        get
        {
            return enable;
        }

        set
        {
            enable = value;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (enable)
        {
            lastSpawn += Time.deltaTime;
            if (lastSpawn > 1f / frequency)
            {
                lastSpawn = 0;
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {
		GameObject enemy = Instantiate(enemyPfb[UnityEngine.Random.Range(0, enemyPfb.Length)], transform.position, transform.rotation);
        EnemyController enemyCtrl = enemy.GetComponent<EnemyController>();
        GameObject shop = SpawnManager.instance.GetRandomShop();
        Vector3 dest = Vector3.zero;
        if (shop != null)
        {
            dest = shop.transform.position;
        }
        enemyCtrl.TrgPos = dest;
    }
}
