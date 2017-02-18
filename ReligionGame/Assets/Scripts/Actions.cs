using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* To do:
 * - coolDown system
 * - show description
 * - Actual Effects
*/


public class Actions
{
    public int cost;
    public int AP;
    public int coolDown;
    public String description;
	protected int selectedCity;

    public Actions(int city)
    {
        //constructor
		selectedCity = city;
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
        cost = -300; // I am making this negative because currently player doesn't have any gold...
        AP = 2;
        coolDown = 4;
        description = "Send missionary to this city to increase non-follower loyalty to your religion by 10.";
    }

    public override void Effect()
    {
        // do the effect
		string selectedCityName = "City" + selectedCity.ToString();
        GenerateCity currentCity = GameObject.Find(selectedCityName).GetComponent<CityScript>().city;
        currentCity.IncreaseNonfollowerLoyalty(0.1f);

        // handle cooldown
    }

}

public class RentBillboard : Actions
{
    public RentBillboard(int city): base(city)
    {
        cost = 150;
        AP = 3;
        coolDown = 5;
        description = "Rent a billboard in this city to proclaim salvation to the masses. ";
    }

    public override void Effect()
    {
        // do the effect
		string selectedCityName = "City" + selectedCity.ToString();
        GenerateCity currentCity = GameObject.Find(selectedCityName).GetComponent<CityScript>().city;
        currentCity.IncreaseEveryoneLoyalty(0.04f);

        // handle cooldown
    }
		
}


public class HireTVSlot : Actions
{
	public HireTVSlot(int city): base(city)
	{
		cost = 200; 
		AP = 3;
		coolDown = 2;
		description = "Hire a random TV timeslot to advertise your religion.";
	}

	public override void Effect()
	{
		// do the effect
	    // Loyalty +4 to everyone. +30% boost to all TV slots and televangelists hired for the next 2 turns.

		string selectedCityName = "City" + selectedCity.ToString();
		GenerateCity currentCity = GameObject.Find(selectedCityName).GetComponent<CityScript>().city;
        currentCity.IncreaseEveryoneLoyalty(0.06f);
		// handle cooldown
	}

}


public class PosterCampaign : Actions
{
    public PosterCampaign(int city): base(city)
    {
        cost = 50;
        AP = 2;
        coolDown = 2;
        description = "Put up posters proclaiming hellfire and damnation to those not of your religion.";
    }

    public override void Effect()
    {
        // do the effect
        //  Loyalty+6 to those with loyalty above 50. Loyalty -6 to everyone else.
        string selectedCityName = "City" + selectedCity.ToString();
        GenerateCity currentCity = GameObject.Find(selectedCityName).GetComponent<CityScript>().city;
        for (int i = 0; i <= currentCity.GetPopulation(); i++)
        {
            if (currentCity.populationArray[i].loyalty >= 0.5f)
            {
                currentCity.populationArray[i].loyalty += 0.06f;
            }
            else
            {
                currentCity.populationArray[i].loyalty -= 0.06f;
            }
        }

        // handle cooldown
    }

}

