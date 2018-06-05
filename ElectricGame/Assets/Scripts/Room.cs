using UnityEngine;
using System.Collections;

public enum RoomType {Empty=0, CheapApartment, AverageApartment, ExpensiveApartment, Penthouse, Corridor, Restroom, Elevator, Stairwell, Foyer, Reception}

public class Room {
	public RoomType type {get; protected set;}
	public int width {get; protected set;}
	public int height {get; protected set;}
	public int price {get; protected set;}
	public GameObject prefab {get; protected set;}

	public Building building;
	public int x;
	public int y;

	Entity entity;

	public Room(RoomType type, int width, int height, int price, GameObject prefab) {
		this.width = width;
		this.height = height;
		this.price = price;
		this.type = type;
		this.prefab = prefab;
	}

	public static Room BuildRoom(Room proto, int x, int y, Building building) {
		Room newRoom = new Room(proto.type, proto.width, proto.height, proto.price, proto.prefab);
		newRoom.x = x;
		newRoom.y = y;
		newRoom.building = building;
		return newRoom;
	}
		
	public void SetEntity(Entity entity) {
		if(this.entity != null) {
			this.entity.SetRoom(null);
		}
		this.entity = entity;
		if(entity != null) {
			entity.SetRoom(this);
		}
	}

	public bool IsEmpty() {
		return (entity == null);
	}
}

