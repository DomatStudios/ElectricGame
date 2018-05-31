using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBuilding : MonoBehaviour {

    [SerializeField]
    private GameObject room;

    public void CreateBuilding(int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for(int y = 0; y <height; y++)
            {
                GameObject newRoom = Instantiate(room);
                newRoom.transform.position = new Vector3(x*newRoom.transform.GetChild(0).lossyScale.y*2, y * newRoom.transform.GetChild(0).lossyScale.z*2);
            }
        }
    }
}
