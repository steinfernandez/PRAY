﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCity {

	int populationCity;
	public Person[] populationArray;
    [SerializeField]
    int followers;

	public struct Person
	{
        public bool following;
        public float loyalty;
		public float maleFemale;
		public float straightGay;
		public float individualistCollectivist;
		public float liberalConservative;
		public float authoritarianAnarchist;
		public float uneducatedEducated;
		public float poorRich;
		public float youngOld;
		public float rationalistSpiritual;
	}


	// Use this for initialization
	public void GenerateCityInit () {

		populationArray = new Person[populationCity];
		for(int i=0; i<populationCity; i++) {
			Person temp = new Person ();
            temp.following = false;
            temp.loyalty = Random.Range(0f, 40f);
			temp.maleFemale = Random.Range (0f, 1f);
			temp.straightGay = Random.Range (0f, 1f);
			temp.individualistCollectivist = Random.Range (0f, 1f);
			temp.liberalConservative = Random.Range (0f, 1f);
			temp.authoritarianAnarchist = Random.Range (0f, 1f);
			temp.uneducatedEducated = Random.Range (0f, 1f);
			temp.poorRich = Random.Range (0f, 1f);
			temp.youngOld = Random.Range (0.2f, 1f);
			temp.rationalistSpiritual = Random.Range (0f, 1f);
			populationArray [i] = temp;
		}
	}

	public GenerateCity(int population)
    {
        followers = 0;
		populationCity = population;
		GenerateCityInit ();
	}



    public void UpdateFollowerPopulation()
    {
        followers = 0;
        for(int i = 0; i < populationCity; i++)
        {
            /*
            if((populationArray[i].loyalty>20)&&(populationArray[i].following == false))
            {
                populationArray[i].following = true;
            }
            else if((populationArray[i].loyalty<40)&&(populationArray[i].following == true))
            {
                populationArray[i].following = false;
            }
            if (populationArray[i].following == true)
            {
                followers++;
            }
            */


            if (populationArray[i].loyalty >= 40f)
            {
                populationArray[i].following = true;
                followers++;
            }
            else
            {
                populationArray[i].following = false;
            }

        }
    }

    public void InitializeBaseFollowers()
    {
        Debug.Log("initializing base followers in starting city");
        for(int i=0;i<(populationCity/3);i++)
        {
            //populationArray[i].loyalty = Mathf.Clamp(populationArray[i].loyalty+40f, -100f, 100f);
            populationArray[i].loyalty = 40f;
            populationArray[i].following = true;
            followers++;
        }
    }

    public float CalculateIncome()
    {
        float income = 0;
        for(int i=0;i<populationCity;i++)
        {
            if (populationArray[i].loyalty > 40)
            {
                income += Mathf.Pow(((populationArray[i].loyalty - 40)*5f), 2); //change this formula
            }
        }
        //Debug.Log("income:"+income+" from population:"+populationCity);
        return income;
    }

    public int GetFollowers()
    {
        return followers;
    }

    public int GetPopulation()
    {
        return populationArray.Length;
    }

    public void IncreaseEveryoneLoyalty(float amount)
    {
        for (int i = 0; i < populationCity; i++) {
            populationArray[i].loyalty = Mathf.Clamp(populationArray[i].loyalty + amount, -100f, 100f);
        }
    }

    public void IncreaseNonfollowerLoyalty(float amount)
    {
        Debug.Log("follwer before:" + GetFollowers());
        for (int i = 0; i < populationCity; i++) {
            if (!populationArray[i].following)
                populationArray[i].loyalty = Mathf.Clamp(populationArray[i].loyalty + amount, -100f, 100f);
        }
        Debug.Log("Follower after: "+GetFollowers());
    }

    public float CalculateMeanLoyalty()
    {
        //todo: calculate median instead
        if (followers != 0)
        {
            float totalLoyalty = 0;
            for (int i = 0; i < populationCity; i++)
            {
                if (populationArray[i].following == true)
                {
                    totalLoyalty += populationArray[i].loyalty;
                }
            }
            return (totalLoyalty / followers);
        }
        else
            return 0;
    }
}
