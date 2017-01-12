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

    private GameStates currentState;

	// Use this for initialization
	void Start ()
    {
        currentState = GameStates.START;
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
}
