using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour {
   public delegate void timeZone();
   public static timeZone newDay;

    public float cycleSpeed = 5f;
    public GameObject sun;
    float noRepeatTimer = 1f;
    bool doNotRepeat = false;
	float currTime = 0f;

	[SerializeField]
	MoneyManager moneyManager;

	void OnEnable () {
		newDay += moneyManager.AddIncome;
	}

	void FixedUpdate() {
        sun.transform.RotateAround(Vector3.zero, Vector3.right, cycleSpeed * Time.deltaTime);
		currTime += cycleSpeed * Time.deltaTime;
		//Debug.Log(sun.transform.position.y);
		//dayEnd: 0 - (-10 * cycleSpeed)
		//dayStart: 0 - (10 * cycleSpeed)
        sun.transform.LookAt(Vector3.zero);
        if (sun.transform.position.y > 0f && sun.transform.position.y < cycleSpeed && sun.transform.localRotation.y == 0 && !doNotRepeat) {
            newDay();
            doNotRepeat = true;
        }
        if(doNotRepeat) {
            noRepeatTimer -= Time.deltaTime;
        }
        if(noRepeatTimer < 0) {
            noRepeatTimer = 1f;
            doNotRepeat = false;
        }
    }
}
