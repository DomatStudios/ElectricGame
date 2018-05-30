using UnityEngine;
using System.Collections;

public abstract class Entity {
	public string name {get; protected set;}

	public Entity(string name) {
		this.name = name;
	}
}

