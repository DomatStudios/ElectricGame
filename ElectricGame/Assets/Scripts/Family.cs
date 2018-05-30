using UnityEngine;
using System.Collections.Generic;

public class Household : Entity {
	protected List<Entity> members;

	public Household(string lastName = "Smith") : base(lastName) {
		members = new List<Entity>();
	}

	public Household(List<Entity> members, string lastName = "Smith") : base(lastName) {
		this.members = members;
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
