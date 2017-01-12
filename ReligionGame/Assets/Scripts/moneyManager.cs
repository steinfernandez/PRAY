using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moneyManager : MonoBehaviour {

    int playerMoney = 0;
    [SerializeField]
    GameObject moneyUI;

	// Use this for initialization
	void Start ()
    {
        moneyUI.GetComponent<Text>().text = "0";
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
