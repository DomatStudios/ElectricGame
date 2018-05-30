using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGrid {
    public List<List<float>> powerGrid = new List<List<float>>();
    
	public PowerGrid(int width, int height) {
		for (int x = 0; x < width; x++) {
            List<float> powerValues = new List<float>();
            for (int y = 0; y < height; y++) {
                powerValues.Add(100);
            }
            powerGrid.Add(powerValues);
        }
    }
}
