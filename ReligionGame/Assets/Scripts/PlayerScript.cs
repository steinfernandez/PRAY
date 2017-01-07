using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public GameObject defaultUI;
    public GameObject cityUI;
    GameObject gameManager;

    // Use this for initialization
    void Start ()
    {
        gameManager = this.gameObject;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.collider.gameObject.CompareTag("City"))
                {
                    hit.collider.gameObject.GetComponent<CityScript>().OnSelect();
                }
                else
                {
                    //unselect all cities, open global menu
                    foreach (GameObject c in GameObject.FindGameObjectsWithTag("City"))
                    {
                        c.GetComponent<CityScript>().selected = false;
                    }
                    gameManager.GetComponent<MenuManagerScript>().SetSelectedCity(0);
                    gameManager.GetComponent<MenuManagerScript>().OpenGlobalMenu();
                }
            }
        }
	}

}
