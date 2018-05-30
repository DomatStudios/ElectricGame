using UnityEngine;
using System.Collections.Generic;

public class Building {
	Dictionary<string, Room> roomProtos = new Dictionary<string, Room>();
	List<List<Room>> rooms = new List<List<Room>>();

	PowerGrid powerGrid;

	public Building(int width, int height) {
		LoadRoomProtos();
		powerGrid = new PowerGrid(width, height);
		for (int x = 0; x < width; x++) {
			List<Room> roomValues = new List<Room>();
			for (int y = 0; y < height; y++) {
				roomValues.Add(Room.BuildRoom(roomProtos["Empty"]));
			}
			rooms.Add(roomValues);
		}
	}

	void LoadRoomProtos() {
		//TODO: probably need to load these from an XML file
		roomProtos.Add("Empty", new Room(RoomType.Empty, 1, 1, 0));
		roomProtos.Add("CheapApartment", new Room(RoomType.CheapApartment, 1, 1, 0));
		roomProtos.Add("AverageApartment", new Room(RoomType.AverageApartment, 2, 1, 0));
		roomProtos.Add("ExpensiveApartment", new Room(RoomType.ExpensiveApartment, 3, 1, 0));
		roomProtos.Add("Penthouse", new Room(RoomType.Penthouse, 2, 2, 0));
		roomProtos.Add("Corridor", new Room(RoomType.Corridor, 2, 1, 0));
		roomProtos.Add("Restroom", new Room(RoomType.Restroom, 1, 1, 0));
		roomProtos.Add("Elevator", new Room(RoomType.Elevator, 1, 1, 0));
		roomProtos.Add("Stairwell", new Room(RoomType.Stairwell, 1, 1, 0));
		roomProtos.Add("Foyer", new Room(RoomType.Foyer, 1, 1, 0));
		roomProtos.Add("Reception", new Room(RoomType.Reception, 2, 1, 0));
	}

	public void BuildRoom(int x, int y, RoomType type) {
		//x and y are bottom left hand corner of room
		Room newRoom = Room.BuildRoom(roomProtos[type.ToString()]);
		for(int i = 0; i < newRoom.width; i++){
			for(int j = 0; j < newRoom.height; j++){
				rooms[i][j] = newRoom;
				//TODO: Subtract newRoom.price from money
			}
		}
	}
}
