using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour {
    [SerializeField]
    private bool enable = false;

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
		
	}
}
