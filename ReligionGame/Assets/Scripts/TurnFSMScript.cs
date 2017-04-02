using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnFSMScript : MonoBehaviour {

    public enum GameStates
    {
        SETUP,
        CUSTOMIZE,
        START,
        PLAYERTURN,
        GAMETURN,
        LOSS,
        WIN
    }

    private float minimumGameTurnTime;
    private float timeTracker;
    private int runningInvokes;
    private bool monadFunctionExecution;
    public List<List<int>> coolDownList;

    private GameStates currentState;

	// Use this for initialization
	void Start ()
    {
        currentState = GameStates.START;
        minimumGameTurnTime = 1f;
        timeTracker = 0f;
        runningInvokes = 0;
        monadFunctionExecution = false;
        coolDownList = new List<List<int>>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Debug.Log(currentState);
        switch (currentState)
        {
            case (GameStates.START):
                //populate all cities and initialize base follower population
                this.gameObject.GetComponent<populationScript>().PopulateCities();
                //update global display of followers and loyalty
                this.gameObject.GetComponent<MenuManagerScript>().UpdateGlobalFollowerDisplay();
                this.gameObject.GetComponent<MenuManagerScript>().UpdateGlobalLoyaltyDisplay();

                //switching to first player turn
                currentState = GameStates.PLAYERTURN;
                break;
            case (GameStates.PLAYERTURN):
                Service.moneyManager.Update();
                break;
            case (GameStates.GAMETURN):
                /*
                if (timeTracker > minimumGameTurnTime)
                {
                    ExecuteQueuedActions();
                    UpdateCoolDown();

                    //update followers and calculate income
                    if (monadFunctionExecution == false
                    ) //these functions should only be executed once before next playerturn
                    {
                        UpdateIncome();
                        this.gameObject.GetComponent<PlayerScript>().RegenerateActionPoints();
                        this.gameObject.GetComponent<MenuManagerScript>().UpdateGlobalFollowerDisplay();
                        this.gameObject.GetComponent<MenuManagerScript>().UpdateGlobalLoyaltyDisplay();
                        this.gameObject.GetComponent<MenuManagerScript>().UpdateLocalDisplay();
                        monadFunctionExecution = true;
                    }
                    if (runningInvokes == 0)
                    {
                        monadFunctionExecution = false;
                        currentState = GameStates.PLAYERTURN;
                    }
                    timeTracker = 0f;
                }
                timeTracker += Time.deltaTime;
                 */
                ExecuteQueuedActions();
                UpdateCoolDown();
                UpdateIncome();
                this.gameObject.GetComponent<PlayerScript>().RegenerateActionPoints();
                this.gameObject.GetComponent<MenuManagerScript>().UpdateGlobalFollowerDisplay();
                this.gameObject.GetComponent<MenuManagerScript>().UpdateGlobalLoyaltyDisplay();
                this.gameObject.GetComponent<MenuManagerScript>().UpdateLocalDisplay();
                Service.moneyManager.Update();
                UpdateMenu();
                currentState = GameStates.PLAYERTURN;
                break;
            case (GameStates.WIN):
                break;
            case (GameStates.LOSS):
                break;
        }

    }

    void ExecuteQueuedActions()
    {
        //execute queued actions and calculate results
        int length = this.gameObject.GetComponent<PlayerScript>().playerActionQueue.Count;
        if (length > 0)
        {
            for (int i = 0; i < length; i++)
            {
                Actions action = (Actions)gameObject.GetComponent<PlayerScript>().playerActionQueue[0];
                gameObject.GetComponent<PlayerScript>().playerActionQueue.RemoveAt(0);
                action.Effect();
                //Debug.Log(action);
                // if it has cool down then add to cool down list
                if (action.coolDown > 0)
                {
                    List<int> cd = new List<int>() {action.actionID, action.selectedCity, action.coolDown};
                    coolDownList.Add(cd);
                    // let the button be deactive so player can't press it during cooldown

                }
            }
            //empty playeractionqueue
            this.gameObject.GetComponent<PlayerScript>().ClearPlayerActionQueue();
        }
    }


    void UpdateCoolDown()
    {
        // iterate through the cool down list and check if it's done
        for (int i = coolDownList.Count - 1; i >= 0; --i)
        {
            coolDownList[i][2] -= 1;
            Debug.Log("action" + coolDownList[i][0] + "on city" + coolDownList[i][1] + " remaining:" + coolDownList[i][2]);
            if (coolDownList[i][0] == 0)
            {
                // set the button active again
                //GetComponent<ActionScript>().DisableActionButton(coolDownList[i][0]);
                coolDownList.RemoveAt(i);
            }
        }
    }

    void UpdateIncome()
    {
        float totalIncome = 0;
        GameObject[] cities;
        cities = GameObject.FindGameObjectsWithTag("City");
        foreach (GameObject c in cities)
        {
			//Debug.Log("before update:" + c.GetComponent<CityScript>().city.GetFollowers());
            c.GetComponent<CityScript>().city.UpdateFollowerPopulation();
            //Debug.Log("after update:" + c.GetComponent<CityScript>().city.GetFollowers());
			totalIncome += c.GetComponent<CityScript>().city.CalculateIncome();
        }
        Service.moneyManager.AddPlayerMoney(totalIncome);
    }

    void UpdateMenu()
    {
        gameObject.GetComponent<MenuManagerScript>().OpenGlobalMenu();

    }

    public GameStates GetCurrentState()
    {
        return currentState;
    }

    public void SetState(GameStates gs)
    {
        currentState = gs;
    }

    void IncrementRunningInvokes()
    {
        runningInvokes++;
    }

    public void DecrementRunningInvokes()
    {
        runningInvokes--;
    }
}
