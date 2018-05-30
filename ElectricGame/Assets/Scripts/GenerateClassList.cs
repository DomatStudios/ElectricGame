using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GenerateClassList : MonoBehaviour {
    [SerializeField]
    int numCitizens = 20;

    [SerializeField]
    int ageMin = 18;

    [SerializeField]
    int ageMax = 80;
    

	public List<Entity> population = new List<Entity>();

    List<string> namesList;

    int currentCitizen = 0;

	void Start() {
		GenerateNames();
		for (int i = 0; i < numCitizens; i++) {
			population.Add(new Citizen(namesList[Random.Range(0, namesList.Count-1)], Random.Range(ageMin, ageMax), (CitizenJob) Random.Range(0, System.Enum.GetValues(typeof(CitizenJob)).Length)));
       }
	}

	void GenerateNames() {
		string names = File.ReadAllText(Application.streamingAssetsPath + "/names.txt");
		namesList = new List<string>(names.Split('\n'));
    }

    public void NextCitizen() {
        currentCitizen++;
    }

    public void PrevCitizen() {
        currentCitizen--;
    }
}
