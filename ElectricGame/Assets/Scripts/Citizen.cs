using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen
{
    public string name;
    public int age;
    public int happiness;
    
    public Citizen(string newName, int newAge)
    {
        name = newName;
        age = newAge;
        happiness = 100;
    }
}
