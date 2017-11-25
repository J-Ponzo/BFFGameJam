using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Bulle : MonoBehaviour {

    private HUDManager hUDManager;

    public float timeoutDestructor;
    public Text textBulle;
    public RawImage imageBulle;
    public ParticleSystem explosion;

    // Use this for initialization
    void Start () {
        hUDManager = transform.parent.gameObject.GetComponent<HUDManager>();
        Destroy(this.gameObject, timeoutDestructor);
	}
	
	// Update is called once per frame
	void Update () {

        if (this.transform.position.z > 30.00) {         
            this.transform.Translate(new Vector3(0, 0, -2.5f));
        }        
;	}

    private void OnDestroy()
    {
        hUDManager.BulleExplosion();
    }

    public void SetText(string textIN)
    {
        textBulle.text = textIN;
    }
}