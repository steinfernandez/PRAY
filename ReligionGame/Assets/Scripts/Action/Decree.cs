﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decree : Actions
{

	public int decreeID;

	public Decree(int city) : base(city)
	{
		
	}

	public virtual void Effect()
	{
		//Override
	}

	protected void WholePopulation(float amount)
	{
		foreach (GameObject c in GameObject.FindGameObjectsWithTag("City"))
		{
			GenerateCity currentCity = c.GetComponent<CityScript>().city;
			for (int i = 0; i <= currentCity.GetPopulation(); i++)
			{
				SetCollectivist(currentCity.populationArray[i], amount);
			}
		}
	}

	protected void SetCollectivist(GenerateCity.Person p, float amount)
	{
		if (p.individualistCollectivist >= 0.5f)
		{
			p.loyalty += amount;
		}

	}

}

public class SupportGroup : Decree 
{
	
	public SupportGroup(int city) : base(city)
	{
		decreeID = 0;
		cost = 300;
		AP = 4;
		coolDown = 2;
		description = "Collectivist +30, female +30, authoritarian +20, poor +20, spiritualist +30\nRich -20, Individualist -50";
		printName = "Support Group";
	}

	public override void Effect()
	{
		WholePopulation(0.3f);
	}
}

public class YouthClub : Decree 
{
	
	public YouthClub(int city) : base(city)
	{
		decreeID = 1;
		cost = 500;
		AP = 4;
		coolDown = 3;
		description = "Set up youth club";
		printName = "Youth Club";
		selectedCity = 6;
	}

	public override void Effect()
	{

	}
}

public class ReligiousColleges : Decree 
{

	public ReligiousColleges(int city) : base(city)
	{
		decreeID = 2;
		cost = 800;
		AP = 4;
		coolDown = 4;
		description = "Set up religious college";
		selectedCity = 6;
	}

	public override void Effect()
	{

	}
}

public class ConventSchools : Decree 
{

	public ConventSchools(int city) : base(city)
	{
		decreeID = 3;
		cost = 600;
		AP = 4;
		coolDown = 4;
		description = "covent school";
		selectedCity = 6;
	}

	public override void Effect()
	{

	}

}
