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

    private GameStates currentState;

	// Use this for initialization
	void Start ()
    {
        currentState = GameStates.START;
        minimumGameTurnTime = 1f;
        timeTracker = 0f;
        runningInvokes = 0;
        monadFunctionExecution = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(currentState);
        switch (currentState)
        {
            case (GameStates.START):
                //populate all cities and initialize base follower population
                this.gameObject.GetComponent<populationScript>().PopulateCities();
                //update global display
                this.gameObject.GetComponent<MenuManagerScript>().UpdateGlobalFollowerDisplay();
                //switching to first player turn
                currentState = GameStates.PLAYERTURN;
                break;
            case (GameStates.PLAYERTURN):
                break;
            case (GameStates.GAMETURN):
                if(timeTracker>minimumGameTurnTime)
                {
                    ExecuteQueuedActions();
                    //update followers and calculate income
                    if (monadFunctionExecution == false)    //these functions should only be executed once before next playerturn 
                    {
                        UpdateIncome();
                        this.gameObject.GetComponent<PlayerScript>().RegenerateActionPoints();
                        this.gameObject.GetComponent<MenuManagerScript>().UpdateGlobalFollowerDisplay();
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
        string[] tempQueue;
        int length = this.gameObject.GetComponent<PlayerScript>().playerActionQueue.Count;
        if (length > 0)
        {
            tempQueue = new string[10];
            this.gameObject.GetComponent<PlayerScript>().playerActionQueue.CopyTo(tempQueue, 0);
            for (int i = 0; i < length; i++)
            {
                Debug.Log("invoking " + tempQueue[i]);
                this.gameObject.GetComponent<PlayerScript>().Invoke(tempQueue[i], 0);
                IncrementRunningInvokes();
            }
            //empty playeractionqueue
            this.gameObject.GetComponent<PlayerScript>().ClearPlayerActionQueue();
        }
    }

    void UpdateIncome()
    {
        float totalIncome = 0;
        GameObject[] cities;
        cities = GameObject.FindGameObjectsWithTag("City");
        foreach (GameObject c in cities)
        {
            c.GetComponent<GenerateCity>().UpdateFollowerPopulation();
            totalIncome += c.GetComponent<GenerateCity>().CalculateIncome();
        }
        this.gameObject.GetComponent<moneyManager>().AddPlayerMoney(totalIncome);
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
