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

    private GameStates currentState;

	// Use this for initialization
	void Start ()
    {
        currentState = GameStates.START;
        minimumGameTurnTime = 1f;
        timeTracker = 0f;
        runningInvokes = 0;
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
                //switching to first player turn
                currentState = GameStates.PLAYERTURN;
                break;
            case (GameStates.PLAYERTURN):
                break;
            case (GameStates.GAMETURN):
                if(timeTracker>minimumGameTurnTime)
                {
                    Debug.Log(timeTracker);
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
                    //regenerate action points and hand control back to player
                    this.gameObject.GetComponent<PlayerScript>().RegenerateActionPoints();
                    if (runningInvokes == 0)
                    {
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
