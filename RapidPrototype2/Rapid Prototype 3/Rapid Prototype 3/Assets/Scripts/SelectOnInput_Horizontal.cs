using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectOnInput_Horizontal : MonoBehaviour {

    public EventSystem eventSystem;
    public GameObject selectedObject;

    private bool bButtonSelected;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetAxisRaw("Horizontal") != 0 && !bButtonSelected)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            bButtonSelected = true;
        }
	}

    private void OnDisable()
    {
        bButtonSelected = false;
    }
}
