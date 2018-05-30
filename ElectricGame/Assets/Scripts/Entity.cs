using UnityEngine;
using System.Collections;

public abstract class Entity {
	public string name {get; protected set;}

	public int x {get; protected set;}
	public int y {get; protected set;}

	public Entity(string name, int x, int y) {
		this.name = name;
		this.x = x;
		this.y = y;
	}
}

