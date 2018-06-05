using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CitizenJob {Doctor=0, SportsPlayer, Teacher, ConstructionWorker, Musician, Fireman, Butcher, OfficeWorker, Student, Baker, Policeman, Lawyer, Pilot, Soldier}

public class Citizen : Entity {
	public int age {get; protected set;}

	public CitizenJob job  {get; protected set;}

	public float startWorkTime;
	public float endWorkTime;

	public static float cycleSpeed;
	public static float timeOfDay;

	Queue<Vector2> path;
	Vector2 curr;
	Vector2 dest;

	float movePercent;

	public Citizen(string name = "Bob", int x = 0, int y = 0, int age = 18, CitizenJob job = CitizenJob.Doctor) : base(name, x, y) {
        this.age = age;
		this.job = job;
 		startWorkTime = Random.Range(0, 25);
		endWorkTime = Random.Range(0, 25);
		path = new Queue<Vector2>();
		curr = new Vector2(x, y);
		dest = new Vector2(-1, -1);
		movePercent = 0;
    }

	public override bool Update() {
		//TODO: we must optimize this (when you test with one, the motions are very smooth, but now they are chunky; Unity 2018's job system can fix this, watch the GDC talk about it for more info)
		if(happiness < happinessMin){
			return false;
		}
		obj.SetActive(true);
		if(dest == new Vector2(-1, -1) && path.Count == 0){
			Move();
		}
		if(dest == new Vector2(-1, -1) && path.Count != 0) {
			dest = path.Dequeue();
		}
		if(LerpMove(curr, dest, ref movePercent)){
			curr = dest;
			if(path.Count != 0) {
				dest = path.Dequeue();
			} else {
				dest = new Vector2(-1, -1);
			}
		}
		//FIXME: they like to cut corners (they must go to the elevators marked in purple, take it down to the main floor, go the blue foyer, and disappear)
		obj.transform.position = new Vector3(Mathf.Lerp(curr.x, dest.x, movePercent) * 2, Mathf.Lerp(curr.y, dest.y, movePercent) * 2 - 0.6f, 0);
		if(obj.transform.position.x < 0 || obj.transform.position.y < 0 || obj.transform.position.x > room.building.width * 2 || obj.transform.position.y > (room.building.height * 2) - 0.6f){
			obj.SetActive(false);
		}
		return true;
	}

	void Move(){
		if(timeOfDay > endWorkTime - (10f * cycleSpeed) && timeOfDay < endWorkTime + (10f * cycleSpeed)) {
			//FIXME: these guys just do not want to come home... weirdos (this is called and a path is sort of generated; try testing with just 1 character, might be a problem with the amount of processing power required; also try to speeding them up by editng the speed variable in Entity)
			//come home
			path = room.building.GetPath(curr, new Vector2(aptX, aptY));
			if(path.Count > 0){
				dest = path.Dequeue();
			}
			obj.SetActive(true);
			return;
		} else if(timeOfDay > startWorkTime + (10f * cycleSpeed) && timeOfDay < startWorkTime - (10f * cycleSpeed)) {
			//leave for work
			path = room.building.GetPath(curr, RoomType.Foyer);
			if(path.Count > 0){
				dest = path.Dequeue();
			}
			return;
		} else {
			//TODO: just move around the house (probably just find the width of the room and generate random x coords inside of it)
			if(curr.x != aptX || curr.y != aptY){
				obj.SetActive(false);
				return;
			}
			return;
		}
	}
}
