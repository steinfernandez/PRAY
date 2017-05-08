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
    [SerializeField]
    GameObject GlobalAverageFollowerLoyaltyDisplay;
    [SerializeField]
    GameObject LocalFollowerNumDisplay;
    [SerializeField]
    GameObject LocalAverageFollowerLoyaltyDisplay;
    [SerializeField]
    GameObject LocalInfo;
    [SerializeField]
    GameObject CityName;

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
        //Debug.Log("Updating global follower display");
        int globalFollowerPopulation = 0;
        int globalPopulation = 0;
        foreach(GameObject c in cities)
        {
			globalFollowerPopulation += c.GetComponent<CityScript>().city.GetFollowers();
			globalPopulation += c.GetComponent<CityScript>().city.GetPopulation();
        }
        //Debug.Log("global followers: " + globalFollowerPopulation.ToString() + "out of " + globalPopulation.ToString());
        float followerPercentage = (float) globalFollowerPopulation * 100f / globalPopulation;
        //Debug.Log(followerPercentage);
        GlobalFollowerNumDisplay.GetComponent<Text>().text = "Followers: \n" + globalFollowerPopulation.ToString() + " (" + followerPercentage.ToString("F2") + "%)";
        //GlobalFollowerNumDisplay.GetComponent<Text>().text = "BOOOOBIES";
    }

    public void UpdateGlobalLoyaltyDisplay()
    {
        float globalLoyalty = 0;
        int divisor = 0;
        foreach(GameObject c in cities)
        {
			if (c.GetComponent<CityScript>().city.GetFollowers() > 0)
            {
				globalLoyalty += c.GetComponent<CityScript>().city.CalculateMeanLoyalty();
                divisor++;
            }
        }
        globalLoyalty = globalLoyalty / divisor;
        //Debug.Log(globalLoyalty);
        GlobalAverageFollowerLoyaltyDisplay.GetComponent<Text>().text = "Avg. Follower Loyalty: " + globalLoyalty.ToString("F2") + "%";
    }

    public void UpdateLocalDisplay()
    {
        Debug.Log(selectedCity);
        string selectedCityName = "City" + selectedCity.ToString();
        GameObject currentCity = GameObject.Find(selectedCityName); //eventually display city names
        CityName.GetComponent<Text>().text = selectedCityName;

        //get followers, calculate percentage, update display
        int localFollowers = currentCity.GetComponent<CityScript>().city.GetFollowers();
        float localFollowerPercentage = (float) localFollowers * 100f / currentCity.GetComponent<CityScript>().city.GetPopulation();
        LocalFollowerNumDisplay.GetComponent<Text>().text = "Followers: \n" + localFollowers.ToString() + " (" + localFollowerPercentage.ToString("F2") + "%)";

        //get local loyalty, update display
        float localLoyalty = currentCity.GetComponent<CityScript>().city.CalculateMeanLoyalty();
        LocalAverageFollowerLoyaltyDisplay.GetComponent<Text>().text = "Avg. Follower Loyalty: " + localLoyalty.ToString("F2") + "%";
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
        ActionMenu.SetActive(false);
        PolicyMenu.SetActive(false);
        LocalUpgradeMenu.SetActive(false);
        GlobalUpgradeMenu.SetActive(false);
        LocalInfo.SetActive(false);
    }

    public void OpenLocalMenu()
    {
        CloseAllMenus();
        LocalInfo.SetActive(true);
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

        // check cool down
        List<List<int>> cooldowns = GetComponent<TurnFSMScript>().coolDownList;
        foreach (List<int> cd in cooldowns)
        {
            if (cd[1] == selectedCity)
            {
                DisableActionButton(cd[0]);
            }
            else
            {
                EnableActionButton(cd[0]);
            }
        }
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

    // make a certain button disabled. Will be called in TurnFSM script.
    void DisableActionButton(int actionID)
    {
        // do something
        GameObject btn = GameObject.Find("Canvas/ActionMenu/ButtonPanel").transform.GetChild(actionID - 1).gameObject;
        btn.GetComponent<Button>().interactable = false;
    }


    // make a certain button enabled again. Will be called in TurnFSM script.
    void EnableActionButton(int actionID)
    {
        // do something
        GameObject btn = GameObject.Find("Canvas/ActionMenu/ButtonPanel").transform.GetChild(actionID - 1).gameObject;
        btn.GetComponent<Button>().interactable = true;
    }
}
