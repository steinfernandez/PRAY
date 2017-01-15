using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moneyManager : MonoBehaviour {

    float playerMoney;
    [SerializeField]
    GameObject moneyUI;
    [SerializeField]
    GameObject gameManager;

	// Use this for initialization
	void Start ()
    {
        playerMoney = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //change to invokeRepeating for added efficiency later
        moneyUI.GetComponent<Text>().text = ((int)playerMoney).ToString();
    }

    public void AddPlayerMoney(float c)
    {
        playerMoney += c;
    }
}
