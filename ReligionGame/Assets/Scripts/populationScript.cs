using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class populationScript : MonoBehaviour {

	//int populationMax = 10000;
	int populationBase = 5000;


	// Use this for initialization
	void Start () {
		foreach (GameObject c in GameObject.FindGameObjectsWithTag("City")) {
			int tmpPopulation = populationBase + Random.Range (-2000, 2000);
			c.GetComponent<GenerateCity>().GenerateCityInit(tmpPopulation);
			Debug.Log (tmpPopulation);

		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
