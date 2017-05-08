 	using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager
{

	public List<Actions> ActionQueue;
	public List<List<int>> coolDownList;
	private GameObject gm;

	public ActionManager()
	{
		ActionQueue = new List<Actions>();
		coolDownList = new List<List<int>>();
		gm = GameObject.Find("GameManager");
	}

	public void Update()
	{
		ExecuteQueuedActions();
		UpdateCoolDown();
	}

	void ExecuteQueuedActions()
	{
		//execute queued actions and calculate results
		int length = ActionQueue.Count;
		if (length > 0)
		{
			for (int i = 0; i < length; i++)
			{
				Actions action = ActionQueue[0];
				ActionQueue.RemoveAt(0);
				action.Effect();
				Debug.Log(action);
				// if it has cool down then add to cool down list
				if (action.coolDown > 0)
				{
					List<int> cd = new List<int>() {action.actionID, action.selectedCity, action.coolDown};
					coolDownList.Add(cd);
					// let the button be deactive so player can't press it during cooldown

				}
			}
			//empty playeractionqueue
			ActionQueue.Clear();
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



	public void GenerateActionQueue()
	{
		if (gm.GetComponent<TurnFSMScript>().GetCurrentState() == TurnFSMScript.GameStates.PLAYERTURN)
		{

			// destroy visible action queue

			Transform actionQueueTran = GameObject.Find("/Canvas/ActionQueue").transform;
			int childCount = actionQueueTran.childCount;
			for (int i = 0; i < childCount; i++)
			{
				// create action queue according to user adjust orders
				ActionQueue.Add(actionQueueTran.GetChild(i).GetComponent<DragHandler>().GetAction());
				GameObject.Destroy((actionQueueTran.GetChild(i)).gameObject);
			}

			// transition to game turn
			gm.GetComponent<TurnFSMScript>().SetState(TurnFSMScript.GameStates.GAMETURN);
		}
	}
}
