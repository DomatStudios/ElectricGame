using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum Modes {
	Build,
	Erase
}

public class BuildModeManager : MonoBehaviour {
	public Transform parent;
	GameObject prefab;

	Modes mode = Modes.Build;

	[SerializeField]
	Dropdown roomTypes;
	[SerializeField]
	WorldManager worldManager;

	void Start(){
		List<string> options = System.Enum.GetNames(typeof(RoomType)).ToList();
		options.Remove("Empty");
		roomTypes.AddOptions(options);
		prefab = worldManager.building.roomProtos[System.Enum.GetNames(typeof(RoomType))[roomTypes.value + 1]].prefab;
	}

	void Update() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Input.GetMouseButtonDown(0)) {
			if(Physics.Raycast(ray, out hit)) {
				if(mode == Modes.Build) {
					Vector3 spawnPos = hit.collider.transform.position + hit.normal;
					spawnPos = new Vector3(Mathf.RoundToInt(spawnPos.x), Mathf.RoundToInt(spawnPos.y), Mathf.RoundToInt(spawnPos.z));
					if(hit.normal.x != 0) {
						spawnPos.x += hit.normal.x * hit.transform.GetComponent<Renderer>().bounds.extents.x;
					}
					if(hit.normal.y != 0) {
						spawnPos.y += hit.normal.y * hit.transform.GetComponent<Renderer>().bounds.extents.y;
					}
					if(hit.normal.z != 0) {
						spawnPos.z += hit.normal.z * hit.transform.GetComponent<Renderer>().bounds.extents.z;
					}
					if(prefab != null) {
						GameObject obj = Instantiate(prefab, spawnPos, prefab.transform.rotation);
						obj.transform.SetParent(parent);
						obj.name = "Room_" + spawnPos.x + "_" + spawnPos.y + "_" + spawnPos.z;
					}
					worldManager.building.BuildRoom(Mathf.RoundToInt(spawnPos.x), Mathf.RoundToInt(spawnPos.z), (RoomType) (roomTypes.value + 1));
				}
				if(mode == Modes.Erase) {
					if(parent.parent.childCount > 1) {
						worldManager.building.BuildRoom(Mathf.RoundToInt(hit.collider.gameObject.transform.parent.position.x), Mathf.RoundToInt(hit.collider.gameObject.transform.parent.position.z), RoomType.Empty);
						Destroy(hit.collider.gameObject.transform.parent.gameObject);
					}
				}
			}
		}
	}

	public void SetModeBuild() {
		mode = Modes.Build;
		roomTypes.gameObject.SetActive(true);
	}

	public void SetModeErase() {
		mode = Modes.Erase;
		roomTypes.gameObject.SetActive(false);
	}

	public void SetPrefab() {
		prefab = worldManager.building.roomProtos[System.Enum.GetNames(typeof(RoomType))[roomTypes.value + 1]].prefab;
	}
}
