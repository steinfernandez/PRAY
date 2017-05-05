
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnFSMScript : MonoBehaviour {

    public enum GameStates
    {
        SETUP,
        START,
        PLAYERTURN,
        GAMETURN,
        LOSS,
        WIN
    }


    public List<List<int>> coolDownList;

    private GameStates currentState;

	// Use this for initialization
	void Start ()
    {
        currentState = GameStates.START;
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
                PopulateCities();
                //update global display of followers and loyalty
                gameObject.GetComponent<MenuManagerScript>().UpdateGlobalFollowerDisplay();
                gameObject.GetComponent<MenuManagerScript>().UpdateGlobalLoyaltyDisplay();

                //switching to first player turn
                currentState = GameStates.PLAYERTURN;
                break;
            case (GameStates.PLAYERTURN):
                Service.playerScript.Update();
                Service.moneyManager.Update();
                break;
            case (GameStates.GAMETURN):

                ExecuteQueuedActions();
                UpdateCoolDown();
                UpdateIncome();
                Service.playerScript.RegenerateActionPoints();
                gameObject.GetComponent<MenuManagerScript>().UpdateGlobalFollowerDisplay();
                gameObject.GetComponent<MenuManagerScript>().UpdateGlobalLoyaltyDisplay();
                gameObject.GetComponent<MenuManagerScript>().UpdateLocalDisplay();
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
        int length = Service.playerScript.playerActionQueue.Count;
        if (length > 0)
        {
            for (int i = 0; i < length; i++)
            {
                Actions action = (Actions)Service.playerScript.playerActionQueue[0];
                Service.playerScript.playerActionQueue.RemoveAt(0);
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
            Service.playerScript.ClearPlayerActionQueue();
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

    void PopulateCities()
    {
        //Initialize a random city as your starting point and grant you followers there
        GameObject[] cities = GameObject.FindGameObjectsWithTag("City");
        int randstart = Random.Range(0, 5);
        Debug.Log(cities[randstart].gameObject.name);
        cities[randstart].GetComponent<CityScript>().city.InitializeBaseFollowers();
    }


}
