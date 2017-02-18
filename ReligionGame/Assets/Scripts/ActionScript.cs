﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionScript : MonoBehaviour
{

	public void SendMissionaryAction() {
		GameObject gameManager = GameObject.Find("GameManager");
		int cityID = gameManager.GetComponent<MenuManagerScript>().GetSelectedCity();
		SendMissionary action = new SendMissionary(cityID);
		gameManager.GetComponent<PlayerScript>().QueuePlayerAction(action);
	}

	public void RentBillboardAction() {
		GameObject gameManager = GameObject.Find("GameManager");
		int cityID = gameManager.GetComponent<MenuManagerScript>().GetSelectedCity();
		RentBillboard action = new RentBillboard(cityID);
		gameManager.GetComponent<PlayerScript>().QueuePlayerAction(action);
	}



}
