using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour {
	[SerializeField]
	int money = 10000;
	
	//taxRate is percentage out of 100
	[SerializeField]
	int taxRate = 10;

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
			money += 50 + taxRate * (worldManager.building.rooms[worldManager.population[i].aptX][worldManager.population[i].aptY].price);
		}
		if(moneyText != null){
			moneyText.text = "Money: " + money.ToString();
		}
	}

	public void AddMoney(int amt){
		money += amt;
		if(moneyText != null){
			moneyText.text = "Money: " + money.ToString();
		}
	}
}
