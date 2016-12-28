using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public GameObject defaultUI;
    public GameObject cityUI;

    // Use this for initialization
    void Start ()
    {
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
                    Debug.Log("You selected a city!");
                    hit.collider.gameObject.GetComponent<CityScript>().OnSelect();
                }
            }
        }
	}

}
