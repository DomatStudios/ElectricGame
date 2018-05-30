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
	int buildingWidth = 25;
	[SerializeField]
	int buildingHeight = 25;

	public List<Entity> population = new List<Entity>();
    List<string> namesList;

    int currentCitizen = 0;

	void Start() {
		building = new Building(buildingWidth, buildingHeight);
		GenerateCitizenName();
		int x = 0;
		int y = 0;
		for (int i = 0; i < numCitizens; i++) {
			if(building.rooms[x][y].type == RoomType.CheapApartment || building.rooms[x][y].type == RoomType.AverageApartment || building.rooms[x][y].type == RoomType.ExpensiveApartment || building.rooms[x][y].type == RoomType.Penthouse){
				population.Add(new Citizen(namesList[Random.Range(0, namesList.Count-1)], x, y, Random.Range(ageMin, ageMax), (CitizenJob) Random.Range(0, System.Enum.GetValues(typeof(CitizenJob)).Length)));
			}
			x += building.rooms[x][y].width;
			if(x > buildingWidth){
				y++;
				x = 0;
			}
			if(y > buildingHeight){
				break;
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
