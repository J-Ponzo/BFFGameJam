using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloWorld : MonoBehaviour {

    [SerializeField]
    private GameObject _PlayMenu; 
    private bool _isPlayMenuActive = false;
    [SerializeField]
    private GameObject _MainMenu;
	

    public void ActivePlayMenu()
    {
        _PlayMenu.SetActive(true);
        _isPlayMenuActive = true; 
    }

    public void ActiveMainMenu()
    {
        _isPlayMenuActive = false;
        _MainMenu.SetActive(true);

    }
    public bool IsPlayMenuActive
    {
        get
        {
            return _isPlayMenuActive;
        }
        
    }
}
