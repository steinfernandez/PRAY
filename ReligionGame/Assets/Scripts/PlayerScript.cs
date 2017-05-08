using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public class PlayerScript {

    public GameObject defaultUI;
    public GameObject cityUI;
    GameObject gameManager;
    int actionPoints;
    const int MAXIMUM_ACTION_POINTS = 5;


    //public List<Actions> playerActionQueue;
    Actions confirmedAction;
    int confirmedActionCost;
    private float money;
    private int confirmedActionGold;
    GameObject actionQueueUI;
    GameObject actionQueueContainerUI;
    GameObject actionPointsUI;


    public PlayerScript()
    {
        Start();
    }

    // Use this for initialization
    void Start ()
    {
        actionPoints = 5;
        gameManager = GameObject.Find("GameManager");
        //playerActionQueue = new List<Actions>();
        confirmedAction = null;
        confirmedActionCost = 0;
        money = Service.moneyManager.GetPlayerMoney();
        actionPointsUI = GameObject.Find("Canvas/RightBottomPanel/ActionPointDisplay");
        actionQueueContainerUI = GameObject.Find("Canvas/ActionQueue");
        actionQueueUI = Resources.Load<GameObject>("Action1");
    }
	
	// Update is called once per frame
	public void Update ()
    {

        UpdateActionPointDisplay();
        if (gameManager.GetComponent<TurnFSMScript>().GetCurrentState() == TurnFSMScript.GameStates.PLAYERTURN)
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
                else
                {
                    if ((Input.mousePosition.x >= 830) && (Input.mousePosition.y >= 530))
                    {
                        Debug.Log("profile");
                    }
                    else if (Input.mousePosition.y >= 130)
                    {
                        //unselect all cities, open global menu
                        Debug.Log("unselect city");
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

        actionPoints -= confirmedActionCost;
        Service.moneyManager.AddPlayerMoney(-confirmedActionGold);

        confirmedActionCost = 0;
        confirmedActionGold = 0;
        //confirmationUI.SetActive(false);

        // Visualize the action queue
        GameObject btn = GameObject.Instantiate(actionQueueUI);

        btn.GetComponentInChildren<Text>().text = confirmedAction.printName;
        btn.transform.SetParent(actionQueueContainerUI.transform, false);
        btn.GetComponent<DragHandler>().SetAction(confirmedAction);

        confirmedAction = null;
    }

}
