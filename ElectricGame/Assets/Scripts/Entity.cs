using UnityEngine;
using System.Collections;

public abstract class Entity {
	public string name {get; protected set;}

	public int aptX {get; protected set;}
	public int aptY {get; protected set;}

	protected int happiness;
	protected int happinessMin;

	public GameObject obj;

	public Room room {get; protected set;}

	protected int speed;

	public Entity(string name, int x, int y) {
		this.name = name;
		this.aptX = x;
		this.aptY = y;
		happiness = 100;
		happinessMin = Random.Range(10, 20);
		speed = 1;
	}

	public void SetRoom(Room room) {
		this.room = room;
		if(room != null){
			happinessMin = Random.Range(10 * (int) room.type, 20 * (int) room.type);
		} else {
			happinessMin = Random.Range(10, 20);
		}
	}

	public abstract bool Update();

	protected bool LerpMove(Vector2 curr, Vector2 next, ref float movePercent) {
		float dist = Vector2.Distance(curr, next);
		if(dist == 0){
			return true;
		}
		float moveThisFrame = speed * Time.deltaTime;
		float percentThisFrame = moveThisFrame / dist;
		if(percentThisFrame == Mathf.Infinity) {
			percentThisFrame = 0;
		}
		movePercent += percentThisFrame;
		if(movePercent >= 1) {
			movePercent = 0;
			return true;
		}
		return false;
	}
}

