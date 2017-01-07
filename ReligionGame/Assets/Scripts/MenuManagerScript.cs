using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    int selectedCity;

	// Use this for initialization
	void Start ()
    {
        selectedCity = 0;	
	}
	
	// Update is called once per frame
	void Update () {
		
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
