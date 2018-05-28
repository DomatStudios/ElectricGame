using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
	public int minZoom = 50;
	public int maxZoom = 150;

	public bool inverseZoom = false;

	public int panSpeed = 4;

	Vector3 lastFramePos;
	Vector3 currFramePos;

	Camera cam;

	void Start(){
		cam = Camera.main;
		lastFramePos = cam.ScreenToViewportPoint(Input.mousePosition);
	}

	void Update(){
		currFramePos = cam.ScreenToViewportPoint(Input.mousePosition);
		UpdateCameraPan();
		UpdateCameraZoom();
		lastFramePos = cam.ScreenToViewportPoint(Input.mousePosition);
	}

	void UpdateCameraPan(){
		if(Input.GetMouseButton(2)){//middle mouse button
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
