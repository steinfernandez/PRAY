using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class populationScript : MonoBehaviour {

	int populationBase = 5000;
    GameObject[] cities;

	// Use this for initialization
	void Start ()
    {
        cities = GameObject.FindGameObjectsWithTag("City");
		foreach (GameObject c in cities)
        {
			int tmpPopulation = populationBase + Random.Range (-2000, 2000);
			c.GetComponent<GenerateCity>().GenerateCityInit(tmpPopulation);
			//Debug.Log (tmpPopulation);
		}
        //Initialize a random city as your starting point and grant you followers there
        int randstart = Random.Range(0, 5);
        Debug.Log(cities[randstart].gameObject.name);
        cities[randstart].GetComponent<GenerateCity>().InitializeBaseFollowers();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
