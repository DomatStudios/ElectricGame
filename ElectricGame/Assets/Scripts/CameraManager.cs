using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
	[SerializeField]
	int minZoom = 50;
	[SerializeField]
	int maxZoom = 150;

	[SerializeField]
	bool inverseZoom = false;

	[SerializeField]
	int panSpeed = 4;

	Vector3 lastFramePos;
	Vector3 currFramePos;

	Camera cam;

	void Start(){
		cam = Camera.main;
		lastFramePos = cam.ScreenToViewportPoint(Input.mousePosition);
		new Building(5, 5);
	}

	void Update(){
		currFramePos = cam.ScreenToViewportPoint(Input.mousePosition);
		UpdateCameraPan();
		UpdateCameraZoom();
		lastFramePos = cam.ScreenToViewportPoint(Input.mousePosition);
	}

	void UpdateCameraPan(){
		if(Input.GetAxis("Fire3") != 0){//middle mouse button
			Vector3 diff = (lastFramePos - currFramePos) * panSpeed;;
			cam.transform.Translate(new Vector3(diff.x, diff.y, 0.0f));
		}
	}

	void UpdateCameraZoom(){
		if(inverseZoom){
			cam.fieldOfView += Input.GetAxis("Mouse ScrollWheel") * cam.fieldOfView / 2;
		} else{
			cam.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * cam.fieldOfView / 2;
		}
		cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minZoom, maxZoom);
	}
}
