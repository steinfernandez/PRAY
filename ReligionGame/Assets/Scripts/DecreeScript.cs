using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreeScript : MonoBehaviour {

	GameObject gameManager;

	void Start()
	{
		gameManager = gameObject;
	}

	public void SupportGroupDecree() {
		SupportGroup decree = new SupportGroup (6);
		gameManager.GetComponent<PlayerScript>().QueuePlayerAction(decree);
	}

	public void YouthClubDecree() {
		YouthClub decree = new YouthClub (6);
		gameManager.GetComponent<PlayerScript>().QueuePlayerAction(decree);
	}

	public void RelibiousCollegeDecree() {
		ReligiousColleges decree = new ReligiousColleges (6);
		gameManager.GetComponent<PlayerScript>().QueuePlayerAction(decree);
	}
		
}
