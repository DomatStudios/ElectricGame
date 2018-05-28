using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateClassList : MonoBehaviour
{
    [SerializeField]
    int NumCitizens;

    [SerializeField]
    int AgeMin = 20;

    [SerializeField]
    int AgeMax = 80;
    

    public List<Citizen> Population = new List<Citizen>();

    List<string> NamesList = new List<string>();

    int currentCitizen = 0;
    // Use this for initialization
    void Start ()
    {
        StartList();

        for (int i = 0; i < NumCitizens; i++)
        {
            Population.Add(new Citizen(NamesList[Random.Range(0, NamesList.Count - 1)],Random.Range(AgeMin,AgeMax)));
        }

	}

    void Update()
    {
    }

    void StartList()
    {
        NamesList.Add("Ryan");
        NamesList.Add("Marcella");
        NamesList.Add("Shalon");
        NamesList.Add("Abram");
        NamesList.Add("Almeta");
        NamesList.Add("Luetta");
        NamesList.Add("Bethann");
        NamesList.Add("Judson");
        NamesList.Add("Alida");
        NamesList.Add("Shayla");
        NamesList.Add("Trista");
        NamesList.Add("Albert");
        NamesList.Add("Ramonita");
        NamesList.Add("Ngan");
        NamesList.Add("Rebeca");
        NamesList.Add("Cedrick");
        NamesList.Add("Shamika");
        NamesList.Add("Particia");
        NamesList.Add("Rana");
        NamesList.Add("Ligia");
        NamesList.Add("Markus");
        NamesList.Add("Genia");
        NamesList.Add("Kara");
        NamesList.Add("Zonia");
        NamesList.Add("Eusebio");
        NamesList.Add("Leeann");
        NamesList.Add("Kerrie");
        NamesList.Add("Sherryl");
        NamesList.Add("Shan");
        NamesList.Add("Elda");
        NamesList.Add("Ula");
        NamesList.Add("Hugo");
        NamesList.Add("Thora");
        NamesList.Add("Demetric"); 
        NamesList.Add("Stefania");
        NamesList.Add("Sha");
        NamesList.Add("Nelida");
        NamesList.Add("Amos");
        NamesList.Add("Lashell");
        NamesList.Add("Billye");
        NamesList.Add("Bok");
        NamesList.Add("Kurtis");
        NamesList.Add("Yuki");
        NamesList.Add("Margert");
        NamesList.Add("Sung");
        NamesList.Add("Anamaria"); 
        NamesList.Add("Hermelinda");
        NamesList.Add("Claris");
        NamesList.Add("Ashly");
        NamesList.Add("Delores");
    }

    public void NextCit()
    {
        currentCitizen++;
    }
    public void PrevCit()
    {
        currentCitizen--;
    }
}
