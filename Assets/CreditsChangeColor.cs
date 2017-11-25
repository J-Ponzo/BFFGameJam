using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

public class CreditsChangeColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField]
    private GameObject image;
    private Color color;
    private Color saveColor; 
	// Use this for initialization
	void Start () {
        color = new Color(182,182,182,255); 
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        saveColor = image.GetComponent<Image>().color; 
        image.GetComponent<Image>().color = color;
         
      
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        image.GetComponent <Image>().color = saveColor;
    }

   
   
}

