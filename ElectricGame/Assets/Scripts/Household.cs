using UnityEngine;
using System.Collections.Generic;

public class Household : Entity {
	protected List<Entity> members;

	public Household(string lastName = "Smith", int x = 0, int y = 0) : base(lastName, x, y) {
		members = new List<Entity>();
	}

	public Household(List<Entity> members, string lastName = "Smith", int x = 0, int y = 0) : base(lastName, x, y) {
		this.members = members;
	}

	public override bool Update() {
		bool happy = true;
		foreach(Entity entity in members) {
			if(!entity.Update()) {
				happy = false;
			}
		}
		return happy;
	}

	public void AddFamily(Entity member) {
		members.Add(member);
	}

	public void RemoveFamily(int i) {
		members.RemoveAt(i);
	}

	public Entity GetFamily(int i) {
		return members[i];
	}
}
