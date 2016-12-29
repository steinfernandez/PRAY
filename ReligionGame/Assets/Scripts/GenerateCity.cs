using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCity: MonoBehaviour {

	int populationCity;
	public Person[] populationArray;

	public struct Person
	{
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
	public void GenerateCityInit (int population) {
		populationCity = population;
		populationArray = new Person[populationCity];
		for(int i=0; i<populationCity; i++) {
			Person temp = new Person ();
			temp.maleFemale = Random.Range (0, 1);
			temp.straightGay = Random.Range (0, 1);
			temp.individualistCollectivist = Random.Range (0, 1);
			temp.liberalConservative = Random.Range (0, 1);
			temp.authoritarianAnarchist = Random.Range (0, 1);
			temp.uneducatedEducated = Random.Range (0, 1);
			temp.poorRich = Random.Range (0, 1);
			temp.youngOld = Random.Range (0, 1);
			temp.rationalistSpiritual = Random.Range (0, 1);
			populationArray [i] = temp;
		}
	}

	void Start() {
		
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
