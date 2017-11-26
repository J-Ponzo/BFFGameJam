using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour {
    [SerializeField]
    private bool enable = false;
    [SerializeField]
    private GameObject door;

    public bool Enable
    {
        get
        {
            return enable;
        }

        set
        {
            if (value)
            {
                door.SetActive(false);
            } else
            {
                door.SetActive(true);
            }
            enable = value;
        }
    }

    // Use this for initialization
    void Start () {
        door = transform.Find("Curtain").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
