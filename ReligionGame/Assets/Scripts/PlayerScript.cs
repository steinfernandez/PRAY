using System;
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
    //[SerializeField]
    //GameObject confirmationUI;
    public Queue playerActionQueue;
    Actions confirmedAction;
    int confirmedActionCost;
    private float money;
    private int confirmedActionGold;


    // Use this for initialization
    void Start ()
    {
        actionPoints = 5;
        gameManager = this.gameObject;
        playerActionQueue = new Queue();
        confirmedAction = null;
        confirmedActionCost = 0;
        money = gameManager.GetComponent<moneyManager>().GetPlayerMoney();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Debug.Log("queue size:" + playerActionQueue.Count);
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

	public void QueuePlayerAction(Actions action)
    {
        
		//Debug.Log ("action triggered~!!!!");
        int APcost = action.AP;
        int gold = action.cost;

        if ((actionPoints >= APcost) && (money >= gold))
        {
            confirmedAction = action;
            confirmedActionCost = APcost;
            confirmedActionGold = gold;
            //confirmationUI.SetActive(true);
            OnConfirmAction();
        }
        else
        {
            //not enough action points or gold
            // TODO: alert window?
            Debug.Log("not enough action points or gold.");
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
        playerActionQueue.Enqueue(confirmedAction);
        actionPoints -= confirmedActionCost;
        gameManager.GetComponent<moneyManager>().AddPlayerMoney(-confirmedActionGold);
        confirmedAction = null;
        confirmedActionCost = 0;
        confirmedActionGold = 0;
        //confirmationUI.SetActive(false);
    }

    /*
    public void OnCancelAction()
    {
        confirmationUI.SetActive(false);
    }
    */

    public void ClearPlayerActionQueue()
    {
        playerActionQueue.Clear();
    }
}
