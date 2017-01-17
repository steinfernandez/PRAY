using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManagerScript : MonoBehaviour {

    [SerializeField]
    GameObject LocalMenu;
    [SerializeField]
    GameObject GlobalMenu;
    [SerializeField]
    GameObject ActionMenu;
    [SerializeField]
    GameObject PolicyMenu;
    [SerializeField]
    GameObject LocalUpgradeMenu;
    [SerializeField]
    GameObject GlobalUpgradeMenu;
    [SerializeField]
    GameObject GlobalFollowerNumDisplay;

    GameObject[] cities;


    int selectedCity;

	// Use this for initialization
	void Start ()
    {
        selectedCity = 0;
        cities = GameObject.FindGameObjectsWithTag("City");
    }
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    public void UpdateGlobalFollowerDisplay()
    {
        Debug.Log("Updating global follower display");
        int globalFollowerPopulation = 0;
        int globalPopulation = 0;
        foreach(GameObject c in cities)
        {
            globalFollowerPopulation += c.GetComponent<GenerateCity>().GetFollowers();
            globalPopulation += c.GetComponent<GenerateCity>().GetPopulation();
        }
        //Debug.Log("global followers: " + globalFollowerPopulation.ToString() + "out of " + globalPopulation.ToString());
        float followerPercentage = (float) globalFollowerPopulation * 100f / globalPopulation;
        //Debug.Log(followerPercentage);
        GlobalFollowerNumDisplay.GetComponent<Text>().text = "Followers: \n" + globalFollowerPopulation.ToString() + " (" + followerPercentage.ToString("F2") + "%)";
        //GlobalFollowerNumDisplay.GetComponent<Text>().text = "BOOOOBIES";

    }

    public int GetSelectedCity()
    {
        return selectedCity;
    }

    public void SetSelectedCity(int c)
    {
        selectedCity = c;
    }

    void CloseAllMenus()
    {
        LocalMenu.SetActive(false);
        GlobalMenu.SetActive(false);
        ActionMenu.SetActive(false);
        PolicyMenu.SetActive(false);
        LocalUpgradeMenu.SetActive(false);
        GlobalUpgradeMenu.SetActive(false);
    }

    public void OpenLocalMenu()
    {
        CloseAllMenus();
        LocalMenu.SetActive(true);
    }

    public void OpenGlobalMenu()
    {
        CloseAllMenus();
        GlobalMenu.SetActive(true);
    }

    public void OpenActionMenu()
    {
        CloseAllMenus();
        ActionMenu.SetActive(true);
    }
    public void OpenPolicyMenu()
    {
        CloseAllMenus();
        PolicyMenu.SetActive(true);
    }
    public void OpenLocalUpgradeMenu()
    {
        CloseAllMenus();
        LocalUpgradeMenu.SetActive(true);
    }
    public void OpenGlobalUpgradeMenu()
    {
        CloseAllMenus();
        GlobalUpgradeMenu.SetActive(true);
    }
}
