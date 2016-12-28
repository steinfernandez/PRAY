using UnityEngine;
using System.Collections;

public class CityScript : MonoBehaviour {
    public bool selected = false;
    [SerializeField]
    GameObject defaultUI;
    [SerializeField]
    GameObject cityUI;
    GameObject gameManager;

    // Use this for initialization
    void Start ()
    {
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
        //display UI info
        defaultUI.SetActive(false);
        cityUI.SetActive(true);
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
