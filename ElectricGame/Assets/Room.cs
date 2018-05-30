﻿using UnityEngine;
using System.Collections;

public enum RoomType {Empty=0, CheapApartment, AverageApartment, ExpensiveApartment, Penthouse, Corridor, Restroom, Elevator, Stairwell, Foyer, Reception}

public class Room {
	RoomType type;
	public int width {get; protected set;}
	public int height {get; protected set;}
	public int price {get; protected set;}

	public Room(RoomType type, int width, int height, int price) {
		this.width = width;
		this.height = height;
		this.price = price;
		this.type = type;
	}

	public static Room BuildRoom(Room proto) {
		return new Room(proto.type, proto.width, proto.height, proto.price);
	}
}

