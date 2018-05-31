using UnityEngine;
using System.Collections.Generic;

public class Building {
	Dictionary<string, Room> roomProtos = new Dictionary<string, Room>();
	public List<List<Room>> rooms = new List<List<Room>>();

	MoneyManager moneyManager;

	PowerGrid powerGrid;

	public Building(MoneyManager moneyManager, int width, int height) {
		this.moneyManager = moneyManager;
		LoadRoomProtos();
		powerGrid = new PowerGrid(width, height);
		for (int x = 0; x < width; x++) {
			List<Room> roomValues = new List<Room>();
			for (int y = 0; y < height; y++) {
				roomValues.Add(Room.BuildRoom(roomProtos["CheapApartment"]));
			}
			rooms.Add(roomValues);
		}
	}

	public void MoveIn(Entity entity, int x, int y) {
		for(int i = 0; i < rooms[x][y].width; i++){
			for(int j = 0; j < rooms[x][y].height; j++){
				rooms[i][j].SetEntity(entity);
			}
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
				moneyManager.AddMoney(-newRoom.price);
			}
		}
	}
}
