using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnScript : MonoBehaviour {

    [SerializeField]
    GameObject gameManager;
    private List<Actions> playerActionQueue;

	// Use this for initialization
	void Start ()
	{
	    playerActionQueue = GameObject.Find("GameManager").GetComponent<PlayerScript>().playerActionQueue;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EndTurn()
    {
        if (gameManager.GetComponent<TurnFSMScript>().GetCurrentState() == TurnFSMScript.GameStates.PLAYERTURN)
        {

            // destroy visible action queue

            Transform actionQueueTran = GameObject.Find("/Canvas/ActionQueue").transform;
            int childCount = actionQueueTran.childCount;
            for (int i = 0; i < childCount; i++)
            {
                // create action queue according to user adjust orders
                playerActionQueue.Add(actionQueueTran.GetChild(i).GetComponent<DragHandler>().GetAction());
                Destroy((actionQueueTran.GetChild(i)).gameObject);
            }

            // transition to game turn
            gameManager.GetComponent<TurnFSMScript>().SetState(TurnFSMScript.GameStates.GAMETURN);
        }
    }
}
