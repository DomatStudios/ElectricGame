﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour {
	[SerializeField]
	int money = 10000;

	[SerializeField]
	Text moneyText;

	[SerializeField]
	WorldManager worldManager;

	void Start() {
		if(moneyText != null){
			moneyText.text = "Money: " + money.ToString();
		}
	}

	public void AddIncome() {
		for(int i = 0; i < worldManager.GetNumCitizens(); i++) {
			money += 50 * (int)worldManager.building.rooms[worldManager.population[i].x][worldManager.population[i].y].type;
		}
		if(moneyText != null){
			moneyText.text = "Money: " + money.ToString();
		}
	}
}