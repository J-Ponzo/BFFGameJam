using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    // Game Instance Singleton
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    [SerializeField]
    private GameObject inputManagerPfb;

    // Use this for initialization
    void Start()
    {
        GameObject inputManager = Instantiate(inputManagerPfb);
        inputManager.transform.parent = this.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
