using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public GameObject defaultUI;
    public GameObject cityUI;
    GameObject gameManager;
    int actionPoints;


    // Use this for initialization
    void Start ()
    {
        actionPoints = 5;
        gameManager = this.gameObject;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (this.gameObject.GetComponent<TurnFSMScript>().GetCurrentState() == TurnFSMScript.GameStates.PLAYERTURN)
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

    public void QueuePlayerAction()
    {
        //StartCoroutine(IE_QueuePlayerAction());
        InvokeRepeating("IE_QueuePlayerAction",0.1f,0.1f);
        gameManager.GetComponent<TurnFSMScript>().IncrementRunningInvokes();
    }

    void IE_QueuePlayerAction()
    {
        if (gameManager.GetComponent<TurnFSMScript>().GetCurrentState() == TurnFSMScript.GameStates.GAMETURN)
        {
            Debug.Log("executed player action.");
            CancelInvoke("IE_QueuePlayerAction");
            gameManager.GetComponent<TurnFSMScript>().DecrementRunningInvokes();
        }
    }
}
