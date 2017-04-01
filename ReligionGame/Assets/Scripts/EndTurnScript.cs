using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnScript : MonoBehaviour {

    [SerializeField]
    GameObject gameManager;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EndTurn()
    {
        if (gameManager.GetComponent<TurnFSMScript>().GetCurrentState() == TurnFSMScript.GameStates.PLAYERTURN)
        {
            gameManager.GetComponent<TurnFSMScript>().SetState(TurnFSMScript.GameStates.GAMETURN);

            // destroy visible action queue
            // TODO: Change to map
            Transform actionQueueTran = GameObject.Find("/Canvas/ActionQueue").transform;
            int childCount = actionQueueTran.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Destroy((actionQueueTran.GetChild(i)).gameObject);
            }
        }
    }
}
