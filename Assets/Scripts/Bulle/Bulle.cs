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
    public Transform player;

    // Use this for initialization
    void Start () {
        hUDManager = transform.parent.gameObject.GetComponent<HUDManager>();
        Destroy(this.gameObject, timeoutDestructor);
	}
	
	// Update is called once per frame
	void Update () {

        Debug.Log(Mathf.Abs(this.transform.position.z - player.position.z));
        Debug.Log(this.transform.position.z);
        Debug.Log(player.position.z);
        if (Mathf.Abs(this.transform.position.z - player.position.z) > 0.35) {            
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