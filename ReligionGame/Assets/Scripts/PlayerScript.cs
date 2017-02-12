using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

    public GameObject defaultUI;
    public GameObject cityUI;
    GameObject gameManager;
    int actionPoints;
    const int MAXIMUM_ACTION_POINTS = 5;
    [SerializeField]
    GameObject actionPointsUI;
    [SerializeField]
    GameObject confirmationUI;
    public ArrayList playerActionQueue;
    string confirmedAction;
    int confirmedActionCost;


    // Use this for initialization
    void Start ()
    {
        actionPoints = 5;
        gameManager = this.gameObject;
        playerActionQueue = new ArrayList();
        confirmedAction = null;
        confirmedActionCost = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log("queue size:" + playerActionQueue.Count);
        UpdateActionPointDisplay();
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

    public void QueuePlayerAction(int actionID)
    {
        int cityID = gameManager.GetComponent<MenuManagerScript>().GetSelectedCity();
        // just an example. Haven't figured out yet
        SendMissionary action = new SendMissionary(cityID);
        Debug.Log("city:"+cityID+"action:"+actionID);
        int APcost = action.AP;

        if (actionPoints >= APcost)
        {
            confirmedAction = "IE_QueuePlayerAction";
            confirmedActionCost = APcost;
            confirmationUI.SetActive(true);
        }
        else
        {
            //not enough action points
            Debug.Log("not enough action points.");
        }
    }

    void IE_QueuePlayerAction()
    {
        if (gameManager.GetComponent<TurnFSMScript>().GetCurrentState() == TurnFSMScript.GameStates.GAMETURN)
        {
            Debug.Log("executed player action.");
            gameManager.GetComponent<TurnFSMScript>().DecrementRunningInvokes();
        }
    }

    public void RegenerateActionPoints()
    {
        actionPoints = MAXIMUM_ACTION_POINTS;
    }

    void UpdateActionPointDisplay()
    {
        actionPointsUI.GetComponent<Text>().text = actionPoints.ToString();
    }

    public void OnConfirmAction()
    {
        playerActionQueue.Add(confirmedAction);
        actionPoints -= confirmedActionCost;
        confirmedAction = null;
        confirmedActionCost = 0;
        confirmationUI.SetActive(false);
    }

    public void OnCancelAction()
    {
        confirmationUI.SetActive(false);
    }

    public void ClearPlayerActionQueue()
    {
        playerActionQueue.Clear();
    }
}
