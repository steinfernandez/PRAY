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
		Service.playerScript.QueuePlayerAction(action);
	}

	public void RentBillboardAction() {
		GameObject gameManager = GameObject.Find("GameManager");
		int cityID = gameManager.GetComponent<MenuManagerScript>().GetSelectedCity();
		RentBillboard action = new RentBillboard(cityID);
		Service.playerScript.QueuePlayerAction(action);
	}

    public void HireTVSlotAction() {
        GameObject gameManager = GameObject.Find("GameManager");
        int cityID = gameManager.GetComponent<MenuManagerScript>().GetSelectedCity();
        HireTVSlot action = new HireTVSlot(cityID);
	    Service.playerScript.QueuePlayerAction(action);
    }

    public void PosterCampaignAction() {
        GameObject gameManager = GameObject.Find("GameManager");
        int cityID = gameManager.GetComponent<MenuManagerScript>().GetSelectedCity();
        PosterCampaign action = new PosterCampaign(cityID);
	    Service.playerScript.QueuePlayerAction(action);
    }


}
