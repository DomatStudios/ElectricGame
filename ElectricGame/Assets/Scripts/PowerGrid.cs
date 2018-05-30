using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGrid : MonoBehaviour {
    
    public List<List<float>> powerGrid = new List<List<float>>();
    
    public void MakePowerGrid (int buildingLength, int buildingHeight) {
        for (int x = 0; x < buildingLength; x++)
        {
            List<float> powerValues = new List<float>();
            for (int y = 0; y < buildingHeight; y++)
            {
                powerValues.Add(100);
            }
            powerGrid.Add(powerValues);
        }
    }
}
