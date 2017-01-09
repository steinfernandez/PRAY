using UnityEngine;
using System.Collections;

public class CityScript : MonoBehaviour {
    public bool selected = false;
    int cityID;
    [SerializeField]
    GameObject defaultUI;
    [SerializeField]
    GameObject gameManager;

    // Use this for initialization
    void Start ()
    {
        cityID = this.gameObject.name[4] - 48;
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateSelectionCircle();
	}

    public void OnSelect()
    {
        Debug.Log("OnSelect");
        //change selection of all other units to false
        foreach(GameObject c in GameObject.FindGameObjectsWithTag("City"))
        {
            c.GetComponent<CityScript>().selected = false;
        }
        //change selected status of this unit
        selected = true;
        //activate UI
        gameManager.GetComponent<MenuManagerScript>().OpenLocalMenu();
        //set selected city id
        gameManager.GetComponent<MenuManagerScript>().SetSelectedCity(cityID);

    }

    void UpdateSelectionCircle()
    {
        if(selected)
        {
            this.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
        }
        if(!selected)
        {
            this.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }

    void UpdateFollowerPopulation()
    {
        
    }
}
