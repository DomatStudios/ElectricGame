using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day_Night_Cycle : MonoBehaviour {

   delegate void myDelegate();
    myDelegate my;

    public float cycleSpeed = 5f;


	// Use this for initialization
	void Start () {

        my += CycleInfo;
      
	}
	
    void Update() {
        my();

    }

	// Update is called once per frame
	void FixedUpdate () {
        transform.RotateAround(Vector3.zero, Vector3.right, cycleSpeed * Time.deltaTime);
        transform.LookAt(Vector3.zero);
    }

    void CycleInfo()
    {
        
        if (GameObject.FindGameObjectWithTag("Sun").transform.position.y < 0f)
        {
            Debug.Log("it's night!");
        }
      
    }
}
