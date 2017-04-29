﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileScript : MonoBehaviour
{
	private GenerateCity.Person[] population;
	[SerializeField]
	GameObject ProfileMenu;

	private int i = 1;



	public void ShowProfile()
	{
		ProfileMenu.SetActive(true);
		population = GameObject.Find("City3").GetComponent<CityScript>().city.populationArray;

		GenerateCity.Person p = population[100];

		GameObject.Find("/Canvas/Profile/ProfilePanel/Slider1").GetComponent<Slider>().value = p.maleFemale;
		GameObject.Find("/Canvas/Profile/ProfilePanel/Slider2").GetComponent<Slider>().value = p.straightGay;
		GameObject.Find("/Canvas/Profile/ProfilePanel/Slider3").GetComponent<Slider>().value = p.individualistCollectivist;
		GameObject.Find("/Canvas/Profile/ProfilePanel/Slider4").GetComponent<Slider>().value = p.liberalConservative;
		GameObject.Find("/Canvas/Profile/ProfilePanel/Slider6").GetComponent<Slider>().value = p.authoritarianAnarchist;
		GameObject.Find("/Canvas/Profile/ProfilePanel/Slider7").GetComponent<Slider>().value = p.uneducatedEducated;
		GameObject.Find("/Canvas/Profile/ProfilePanel/Slider8").GetComponent<Slider>().value = p.poorRich;
		GameObject.Find("/Canvas/Profile/ProfilePanel/Slider9").GetComponent<Slider>().value = p.youngOld;
		GameObject.Find("/Canvas/Profile/ProfilePanel/Slider5").GetComponent<Slider>().value = p.rationalistSpiritual;
	}

	public void NextProfile()
	{
		GenerateCity.Person p = population[100 + i * 5];
		i += 1;

		GameObject.Find("/Canvas/Profile/ProfilePanel/Slider1").GetComponent<Slider>().value = p.maleFemale;
		GameObject.Find("/Canvas/Profile/ProfilePanel/Slider2").GetComponent<Slider>().value = p.straightGay;
		GameObject.Find("/Canvas/Profile/ProfilePanel/Slider3").GetComponent<Slider>().value = p.individualistCollectivist;
		GameObject.Find("/Canvas/Profile/ProfilePanel/Slider4").GetComponent<Slider>().value = p.liberalConservative;
		GameObject.Find("/Canvas/Profile/ProfilePanel/Slider6").GetComponent<Slider>().value = p.authoritarianAnarchist;
		GameObject.Find("/Canvas/Profile/ProfilePanel/Slider7").GetComponent<Slider>().value = p.uneducatedEducated;
		GameObject.Find("/Canvas/Profile/ProfilePanel/Slider8").GetComponent<Slider>().value = p.poorRich;
		GameObject.Find("/Canvas/Profile/ProfilePanel/Slider9").GetComponent<Slider>().value = p.youngOld;
		GameObject.Find("/Canvas/Profile/ProfilePanel/Slider5").GetComponent<Slider>().value = p.rationalistSpiritual;
	}

	public void CloseProfile()
	{
		ProfileMenu.SetActive(false);
	}
}
