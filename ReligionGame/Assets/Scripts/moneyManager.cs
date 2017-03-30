using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager {

    float playerMoney;
    GameObject moneyUI;


	// Use this for initialization
	public MoneyManager ()
    {
        playerMoney = 0;
        moneyUI = GameObject.Find("/Canvas/MoneyText");
    }
	
	// Update is called by FSMScript
	public void Update ()
    {
        //change to invokeRepeating for added efficiency later
        moneyUI.GetComponent<Text>().text = ((int)playerMoney).ToString();
    }

    public void AddPlayerMoney(float c)
    {
        playerMoney += c;
    }

    public float GetPlayerMoney()
    {
        return playerMoney;
    }
}
