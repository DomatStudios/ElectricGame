using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day_Night_Cycle : MonoBehaviour {

   public delegate void timeZone();
   public static timeZone newDay;

    [SerializeField]
    private float cycleSpeed = 5f;
    [SerializeField]
    GameObject sun;
    private float noRepeatTimer = 1f;
    private bool doNotRepeat = false;
	// Use this for initialization
	void Start () {

        newDay += Test;
      
	}
	
    void Update() {

    }

	// Update is called once per frame
	void FixedUpdate () {
        sun.transform.RotateAround(Vector3.zero, Vector3.right, cycleSpeed * Time.deltaTime);
        sun.transform.LookAt(Vector3.zero);
        if (sun.transform.position.y > 0f && sun.transform.position.y < cycleSpeed && sun.transform.localRotation.y == 0 && !doNotRepeat)
        {
            newDay();
            doNotRepeat = true;
        }
        if(doNotRepeat)
        {
            noRepeatTimer -= Time.deltaTime;
        }
        if(noRepeatTimer < 0)
        {
            noRepeatTimer = 1f;
            doNotRepeat = false;
        }
    }

    void Test()
    {
        Debug.Log("g");
    }
    
}
