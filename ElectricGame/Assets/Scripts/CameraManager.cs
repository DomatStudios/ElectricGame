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
	}

	void Update(){
		currFramePos = cam.ScreenToViewportPoint(Input.mousePosition);
		UpdateCameraPan();
		UpdateCameraZoom();
        UpdateCameraRotation();
		lastFramePos = cam.ScreenToViewportPoint(Input.mousePosition);
	}

	void UpdateCameraPan(){
		if(Input.GetAxis("Fire3") != 0){//middle mouse button
			Vector3 diff = (lastFramePos - currFramePos) * panSpeed;;
			cam.transform.Translate(new Vector3(diff.x, diff.y, 0.0f));
		}
        if(Input.GetAxis("Horizontal") != 0)
        {
            cam.transform.Translate(new Vector3(Time.deltaTime * panSpeed * Input.GetAxisRaw("Horizontal"), 0 ));
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            cam.transform.Translate(new Vector3(0, Time.deltaTime * panSpeed * Input.GetAxisRaw("Vertical")));
        }
    }

	void UpdateCameraZoom(){
		if(inverseZoom){
			cam.fieldOfView += Input.GetAxis("Mouse ScrollWheel") * cam.fieldOfView / 2;
		} else{
			cam.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * cam.fieldOfView / 2;
		}
        if(Input.GetKey(KeyCode.Q))
        {
            cam.fieldOfView += Time.deltaTime * cam.fieldOfView/2;
        }

        if (Input.GetKey(KeyCode.E))
        {
            cam.fieldOfView -= Time.deltaTime * cam.fieldOfView / 2;
        }
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minZoom, maxZoom);
	}

    void UpdateCameraRotation() {


        if (Input.GetKeyDown(KeyCode.Z))
        {
            cam.transform.Rotate(Vector3.up * -45);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            cam.transform.Rotate(Vector3.up * 45); ;
        }

       
    }
    


}
