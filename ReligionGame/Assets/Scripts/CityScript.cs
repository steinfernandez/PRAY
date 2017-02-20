using UnityEngine;
using System.Collections;

public class CityScript : MonoBehaviour {
    public bool selected = false;
    int cityID;
    [SerializeField]
    GameObject defaultUI;
    [SerializeField]
    GameObject gameManager;
	public GenerateCity city;
	private int populationBase = 5000;

    // Use this for initialization
    void Start ()
    {
        cityID = this.gameObject.name[4] - 48;
		int tmpPopulation = populationBase + Random.Range(-2000, 2000);
		city = new GenerateCity(tmpPopulation);
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateSelectionCircle();
        //Debug.Log("CityID:" + cityID + "follwers:" + city.GetFollowers());
	}

    public void OnSelect()
    {
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
        //update local stats
        gameManager.GetComponent<MenuManagerScript>().UpdateLocalDisplay(); //TODO: update local submenus
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

}
