using UnityEngine;
using System.Collections;

public enum RoomType {Empty=0, CheapApartment, AverageApartment, ExpensiveApartment, Penthouse, Corridor, Restroom, Elevator, Stairwell, Foyer, Reception}

public class Room {
	public RoomType type {get; protected set;}
	public int width {get; protected set;}
	public int height {get; protected set;}
	public int price {get; protected set;}

	Entity entity;

	public Room(RoomType type, int width, int height, int price) {
		this.width = width;
		this.height = height;
		this.price = price;
		this.type = type;
	}

	public static Room BuildRoom(Room proto) {
		return new Room(proto.type, proto.width, proto.height, proto.price);
	}
		
	public void SetEntity(Entity entity){
		this.entity = entity;
	}

	public bool IsEmpty() {
		return (entity == null);
	}
}

