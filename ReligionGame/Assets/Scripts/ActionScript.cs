using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionScript : MonoBehaviour
{
    public int actionID;

	// Use this for initialization
	void Start ()
	{
	    actionID = gameObject.transform.GetSiblingIndex();
	    GameObject gameManager = GameObject.Find("GameManager");

	    Button btn = GetComponent<Button>();
	    btn.onClick.AddListener(() =>
	    {
	        gameManager.GetComponent<PlayerScript>().QueuePlayerAction(actionID);
	    });
	}
	

}
