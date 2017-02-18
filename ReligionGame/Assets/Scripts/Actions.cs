using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions
{
    public int cost;
    public int AP;
    public int coolDown;
    public String description;
    public int actionID;

    public Actions(int city)
    {
        //constructor
    }

    public virtual void Effect()
    {
        //Override
    }

}


public class SendMissionary : Actions
{
    public SendMissionary(int city): base(city)
    {
        actionID = 0;
        cost = -300; // I am making this negative because currently player doesn't have any gold...
        AP = 2;
        coolDown = 4;
        description = "Send missionary to this city to increase non-follower loyalty to your religion by 10.";
    }

    public override void Effect()
    {
        // do the effect
        // show description

        // handle cost and AP
        GameObject gameManager = GameObject.Find("GameManager");
        gameManager.GetComponent<moneyManager>().AddPlayerMoney(-cost);
        // handle cooldown
    }




}

public class RentBillboard : Actions
{
    public RentBillboard(int city): base(city)
    {
        actionID = 1;
        cost = 150;
        AP = 3;
        coolDown = 5;
        description = "Rent a billboard in this city to proclaim salvation to the masses.";
    }

    public override void Effect()
    {
        // do the effect
        // show description

        // handle cost and AP
        GameObject gameManager = GameObject.Find("GameManager");
        gameManager.GetComponent<moneyManager>().AddPlayerMoney(-cost);
        // handle cooldown
    }




}

