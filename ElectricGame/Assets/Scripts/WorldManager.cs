using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WorldManager : MonoBehaviour {
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
	List<RoomType> livableRooms = new List<RoomType>() {RoomType.CheapApartment, RoomType.AverageApartment, RoomType.ExpensiveApartment, RoomType.Penthouse};

	[SerializeField]
	MoneyManager moneyManager;
	[SerializeField]
	BuildModeManager buildModeManager;
	[SerializeField]
	DayNightCycle dayNightCycle;

	public List<Entity> population = new List<Entity>();
    List<string> namesList;

    int currentCitizen = 0;

	[SerializeField]
	GameObject citizen;

	void OnEnable() {
        building = new Building(moneyManager, buildingWidth, buildingHeight);
		CreateBuilding(buildingWidth, buildingHeight);
		GenerateCitizenName();
        for (int x = 0; x < buildingWidth; x++) {
            for (int y = 0; y < buildingHeight; y++) {
				if (livableRooms.Contains(building.rooms[x][y].type)) {
                    population.Add(new Citizen(namesList[Random.Range(0, namesList.Count - 1)], x, y, Random.Range(ageMin, ageMax), (CitizenJob)Random.Range(0, System.Enum.GetValues(typeof(CitizenJob)).Length)));
                    building.MoveIn(population[population.Count - 1], x, y);
                }
            }
        }
		CreateCitizens();
    }

	void FixedUpdate() {
		Citizen.timeOfDay = dayNightCycle.sun.transform.position.y;
		foreach(Entity entity in population) {
			if(!entity.Update()){
				building.MoveIn(null, entity.aptX, entity.aptY);
				population.Remove(entity);
			}
		}
	}

	void GenerateCitizenName() {
		string names = File.ReadAllText(Application.streamingAssetsPath + "/Text/names.txt");
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

	void CreateBuilding(int width, int height) {
		for (int x = 0; x < width; x++) {
			for(int y = 0; y < height; y++) {
				if(building.rooms[x][y].prefab != null) {
					GameObject newRoom = Instantiate(building.rooms[x][y].prefab);
					newRoom.transform.position = new Vector3(x * newRoom.transform.GetChild(0).lossyScale.y * 2, y * newRoom.transform.GetChild(0).lossyScale.z * 2, 0);
					newRoom.transform.parent = buildModeManager.parent;
				}
			}
		}
	}

	void CreateCitizens() {
		Citizen.cycleSpeed = dayNightCycle.cycleSpeed;
		foreach(Entity entity in population){
			entity.obj = Instantiate(citizen, new Vector3(entity.aptX * 2, (entity.aptY * 2) - 0.6f, 0), Quaternion.Euler(0, 180, 0));
		}
	}
}
