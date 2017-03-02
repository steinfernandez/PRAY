using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionScript : MonoBehaviour
{
    // These are the onClick functions for action menu buttons!

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

    public void HireTVSlotAction() {
        GameObject gameManager = GameObject.Find("GameManager");
        int cityID = gameManager.GetComponent<MenuManagerScript>().GetSelectedCity();
        HireTVSlot action = new HireTVSlot(cityID);
        gameManager.GetComponent<PlayerScript>().QueuePlayerAction(action);
    }

    public void PosterCampaignAction() {
        GameObject gameManager = GameObject.Find("GameManager");
        int cityID = gameManager.GetComponent<MenuManagerScript>().GetSelectedCity();
        PosterCampaign action = new PosterCampaign(cityID);
        gameManager.GetComponent<PlayerScript>().QueuePlayerAction(action);
    }


}
