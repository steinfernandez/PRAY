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

    private int runningInvokes;
    private float minimumGameTurnTime;
    private float timeTracker;

    private GameStates currentState;

	// Use this for initialization
	void Start ()
    {
        currentState = GameStates.START;
        runningInvokes = 0;
        minimumGameTurnTime = 1f;
        timeTracker = 0f;
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
                    if (runningInvokes == 0)
                    {
                        currentState = GameStates.PLAYERTURN;
                        timeTracker = 0f;
                    }
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

    public void IncrementRunningInvokes()
    {
        runningInvokes++;
    }

    public void DecrementRunningInvokes()
    {
        runningInvokes--;
    }
}
