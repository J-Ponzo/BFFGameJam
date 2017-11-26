using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    /// <summary>
    /// The static instance of the Singleton for external access
    /// </summary>
    public static SpawnManager instance = null;

    /// <summary>
    /// Enforce Singleton properties
    /// </summary>
    void Awake()
    {
        //Check if instance already exists and set it to this if not
        if (instance == null)
        {
            instance = this;
        }

        //Enforce the unicity of the Singleton
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    [SerializeField]
    private GameObject[] spawners;

    [SerializeField]
    private GameObject[] shops;

    [SerializeField]
    private float spawnActiveFrequency = 0.01f;
    [SerializeField]
    private float shopActiveFrequency = 0.01f;
    [SerializeField]
    private float lastSpawnOpen = 0f;
    [SerializeField]
    private float lastShopOpen = 0f;

    // Use this for initialization
    void Start () {
        spawners = GameObject.FindGameObjectsWithTag("Spawner");
        shops = GameObject.FindGameObjectsWithTag("Shop");
    }
	
	// Update is called once per frame
	void Update () {
        lastSpawnOpen += Time.deltaTime;
        if (lastSpawnOpen > 1f / spawnActiveFrequency)
        {
            lastSpawnOpen = 0;
            OpenSpawn();
        }

        lastShopOpen += Time.deltaTime;
        if (lastShopOpen > 1f / shopActiveFrequency)
        {
            lastShopOpen = 0;
            OpenShop();
        }
    }

    private void OpenShop()
    {
        List<GameObject> closedShops = GetClosedShops();
        if (closedShops.Count > 0)
        {
            int ind = (int)UnityEngine.Random.value * closedShops.Count;
            closedShops[ind].GetComponent<ShopScript>().Enable = true;
        }
    }

    private List<GameObject> GetClosedShops()
    {
        List<GameObject> closed = new List<GameObject>();
        foreach (GameObject shop in shops)
        {
            if (!shop.GetComponent<ShopScript>().Enable)
            {
                closed.Add(shop);
            }
        }
        return closed;
    }

    private List<GameObject> GetOpenShops()
    {
        List<GameObject> open = new List<GameObject>();
        foreach (GameObject shop in shops)
        {
            if (shop.GetComponent<ShopScript>().Enable)
            {
                open.Add(shop);
            }
        }
        return open;
    }

    private void OpenSpawn()
    {
        List<GameObject> closedSpawns = GetClosedSpawns();
        if (closedSpawns.Count > 0)
        {
            int ind = (int)UnityEngine.Random.value * closedSpawns.Count;
            closedSpawns[ind].GetComponent<SpawnerScript>().Enable = true;
        }
    }

    private List<GameObject> GetClosedSpawns()
    {
        List<GameObject> closed = new List<GameObject>();
        foreach (GameObject spawn in spawners)
        {
            if (!spawn.GetComponent<SpawnerScript>().Enable)
            {
                closed.Add(spawn);
            }
        }
        return closed;
    }

    public GameObject GetRandomShop()
    {
        List<GameObject> openShops = GetOpenShops();
        if (openShops.Count > 0)
        {
            int ind = (int) (UnityEngine.Random.value * (float) openShops.Count);
            return openShops[ind];
        }
        return null;
    }
}
