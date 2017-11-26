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
    private bool affichage = true;

    // Use this for initialization
    void Start () {
        hUDManager = transform.parent.gameObject.GetComponent<HUDManager>();        
        this.transform.Translate(new Vector3(0, 0, 0.36f));
        Destroy(this.gameObject, timeoutDestructor);
	}

    // Update is called once per frame
    void Update()
    {

        //Debug.Log("Distance entre la bulle et la position du joueur" + Mathf.Abs(this.transform.position.z - player.position.z));
        //if (this.transform.position.z - player.position.z < 0.35)
        //{
        //    this.transform.Translate(new Vector3(0, 0, 0.1f));
        //}
        //else if (this.transform.position.z - player.position.z > 0.35)
        //{
        //    this.transform.Translate(new Vector3(0, 0, -0.1f));
        //}
    }

    private void OnDestroy()
    {
        hUDManager.BulleExplosion();
    }

    public void SetText(string textIN)
    {
        textBulle.text = textIN;
    }
}