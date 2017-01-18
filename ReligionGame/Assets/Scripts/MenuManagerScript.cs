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
            globalFollowerPopulation += c.GetComponent<GenerateCity>().GetFollowers();
            globalPopulation += c.GetComponent<GenerateCity>().GetPopulation();
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
            if (c.GetComponent<GenerateCity>().GetFollowers() > 0)
            {
                globalLoyalty += c.GetComponent<GenerateCity>().CalculateMeanLoyalty();
                divisor++;
            }
        }
        globalLoyalty = globalLoyalty / divisor;
        //Debug.Log(globalLoyalty);
        GlobalAverageFollowerLoyaltyDisplay.GetComponent<Text>().text = "Avg. Follower Loyalty: " + globalLoyalty.ToString("F2") + "%";
    }

    public void UpdateLocalDisplay()
    {
        string selectedCityName = "City" + selectedCity.ToString();
        GameObject currentCity = GameObject.Find(selectedCityName); //eventually display city names
        //get followers, calculate percentage, update display
        int localFollowers = currentCity.GetComponent<GenerateCity>().GetFollowers();
        float localFollowerPercentage = (float) localFollowers * 100f / currentCity.GetComponent<GenerateCity>().GetPopulation();
        LocalFollowerNumDisplay.GetComponent<Text>().text = "Followers: \n" + localFollowers.ToString() + " (" + localFollowerPercentage.ToString("F2") + "%)";
        //get local loyalty, update display
        float localLoyalty = currentCity.GetComponent<GenerateCity>().CalculateMeanLoyalty();
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
