
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

                Service.actionManager.Update();
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
