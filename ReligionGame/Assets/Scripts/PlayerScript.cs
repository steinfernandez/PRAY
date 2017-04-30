﻿using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public class PlayerScript : MonoBehaviour {

    public GameObject defaultUI;
    public GameObject cityUI;
    GameObject gameManager;
    int actionPoints;
    const int MAXIMUM_ACTION_POINTS = 5;
    [SerializeField]
    GameObject actionPointsUI;
    public List<Actions> playerActionQueue;
    Actions confirmedAction;
    int confirmedActionCost;
    private float money;
    private int confirmedActionGold;
    public GameObject actionQueueUI;
    public GameObject actionQueueContainerUI;


    // Use this for initialization
    void Start ()
    {
        actionPoints = 5;
        gameManager = this.gameObject;
        playerActionQueue = new List<Actions>();
        confirmedAction = null;
        confirmedActionCost = 0;
        money = Service.moneyManager.GetPlayerMoney();
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
                else
                {

                    if (Input.mousePosition.y >= 130)
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

    /*
    void IE_QueuePlayerAction()
    {
        if (gameManager.GetComponent<TurnFSMScript>().GetCurrentState() == TurnFSMScript.GameStates.GAMETURN)
        {
            Debug.Log("executed player action.");
            gameManager.GetComponent<TurnFSMScript>().DecrementRunningInvokes();
        }
    }
*/

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
        //playerActionQueue.Add(confirmedAction);
        actionPoints -= confirmedActionCost;
        Service.moneyManager.AddPlayerMoney(-confirmedActionGold);

        confirmedActionCost = 0;
        confirmedActionGold = 0;
        //confirmationUI.SetActive(false);

        // Visualize the action queue
        GameObject btn = Instantiate(actionQueueUI);
        //string str = confirmedAction.printName;
        btn.GetComponentInChildren<Text>().text = confirmedAction.printName;
        btn.transform.SetParent(actionQueueContainerUI.transform, false);
        btn.GetComponent<DragHandler>().SetAction(confirmedAction);

        confirmedAction = null;
    }


    public void ClearPlayerActionQueue()
    {
        playerActionQueue.Clear();
    }
}
