using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WorldManager : MonoBehaviour {
    [SerializeField]
    int numCitizens = 20;

    [SerializeField]
    int ageMin = 18;
    [SerializeField]
    int ageMax = 80;
    
	public Building building {get; protected set;}
	[SerializeField]
	int buildingWidth;
	[SerializeField]
	int buildingHeight;
    [SerializeField]
    private GenerateBuilding generateBuilding;

	[SerializeField]
	MoneyManager moneyManager;

	public List<Entity> population = new List<Entity>();
    List<string> namesList;

    int currentCitizen = 0;

	void Start() {

        generateBuilding.CreateBuilding(buildingWidth, buildingHeight);
        building = new Building(moneyManager, buildingWidth, buildingHeight);
		GenerateCitizenName();
        for (int x = 0; x < buildingWidth; x++)
        {
            for (int y = 0; y < buildingHeight; y++)
            {
                if (building.rooms[x][y].type != RoomType.Empty)
                {
                    population.Add(new Citizen(namesList[Random.Range(0, namesList.Count - 1)], x, y, Random.Range(ageMin, ageMax), (CitizenJob)Random.Range(0, System.Enum.GetValues(typeof(CitizenJob)).Length)));
                    building.MoveIn(population[population.Count - 1], x, y);
                }
            }
        }
    }


	void GenerateCitizenName() {
		string names = File.ReadAllText(Application.streamingAssetsPath + "/names.txt");
		namesList = new List<string>(names.Split('\n'));
    }

    public void NextCitizen() {
        currentCitizen++;
    }

    public void PrevCitizen() {
        currentCitizen--;
    }

	public int GetNumCitizens(){
		return population.Count;
	}
}
