using UnityEngine;
using System.Collections.Generic;

public class Building {
	Dictionary<string, Room> roomProtos = new Dictionary<string, Room>();
	public List<List<Room>> rooms = new List<List<Room>>();

	//0 = residential, 1 = elevator or stairs, 2 = foyer
	List<List<int>> roomMap = new List<List<int>>();

	MoneyManager moneyManager;

	PowerGrid powerGrid;

	public int width {get; protected set;}
	public int height {get; protected set;}

	public Building(MoneyManager moneyManager, int width, int height) {
		this.moneyManager = moneyManager;
		this.width = width;
		this.height = height;
		LoadRoomProtos();
		powerGrid = new PowerGrid(width, height);
		for (int x = 0; x < width; x++) {
			List<Room> roomValues = new List<Room>();
			List<int> roomMapValues = new List<int>();
			for (int y = 0; y < height; y++) {
				if(x == 0 && y == 0) {
					roomValues.Add(Room.BuildRoom(roomProtos["Foyer"], x, y, this));
					roomMapValues.Add(2);
				} else if(x == 1) {
					roomValues.Add(Room.BuildRoom(roomProtos["Elevator"], x, y, this));
					roomMapValues.Add(1);
				} else {
					roomValues.Add(Room.BuildRoom(roomProtos["CheapApartment"], x, y, this));
					roomMapValues.Add(0);
				}
			}
			rooms.Add(roomValues);
			roomMap.Add(roomMapValues);
		}
	}

	public void MoveIn(Entity entity, int x, int y) {
		for(int i = 0; i < rooms[x][y].width; i++){
			for(int j = 0; j < rooms[x][y].height; j++){
				rooms[i + x][j + y].SetEntity(entity);
			}
		}
	}

	void LoadRoomProtos() {
		//TODO: probably need to load these from an XML file
		roomProtos.Add("Empty", new Room(RoomType.Empty, 1, 1, 0, null));
		roomProtos.Add("CheapApartment", new Room(RoomType.CheapApartment, 1, 1, 150, Resources.Load("CheapApartment") as GameObject));
		roomProtos.Add("AverageApartment", new Room(RoomType.AverageApartment, 2, 1, 300, null));
		roomProtos.Add("ExpensiveApartment", new Room(RoomType.ExpensiveApartment, 3, 1, 500, null));
		roomProtos.Add("Penthouse", new Room(RoomType.Penthouse, 2, 2, 1000, null));
		roomProtos.Add("Corridor", new Room(RoomType.Corridor, 2, 1, 100, null));
		roomProtos.Add("Restroom", new Room(RoomType.Restroom, 1, 1, 500, null));
		roomProtos.Add("Elevator", new Room(RoomType.Elevator, 1, 1, 500, Resources.Load("Elevator") as GameObject));
		roomProtos.Add("Stairwell", new Room(RoomType.Stairwell, 1, 1, 250, null));
		roomProtos.Add("Foyer", new Room(RoomType.Foyer, 1, 1, 100, Resources.Load("Foyer") as GameObject));
		roomProtos.Add("Reception", new Room(RoomType.Reception, 2, 1, 500, null));
	}

	public void BuildRoom(int x, int y, RoomType type) {
		//x and y are bottom left hand corner of room
		Room newRoom = Room.BuildRoom(roomProtos[type.ToString()], x , y, this);
		for(int i = 0; i < newRoom.width; i++){
			for(int j = 0; j < newRoom.height; j++){
				rooms[i][j] = newRoom;
				moneyManager.AddMoney(-newRoom.price);
			}
		}
	}

	public Queue<Vector2> GetPath(Vector2 curr, Vector2 dest) {
		Queue<Vector2> path = new Queue<Vector2>();
		curr = new Vector2(Mathf.RoundToInt(curr.x), Mathf.RoundToInt(curr.y));
		dest = new Vector2(Mathf.RoundToInt(dest.x), Mathf.RoundToInt(dest.y));
		if(curr.y == dest.y) {
			if(curr.x > dest.x) {
				for(int i = (int) curr.x; i >= (int) dest.x; i--) {
					path.Enqueue(new Vector2(i, curr.y));
				}
			} else{
				for(int i = (int) curr.x; i <= (int) dest.x; i++) {
					path.Enqueue(new Vector2(i, curr.y));
				}
			}
			return path;
		}
		if(curr.y > dest.y) {
			int x = (int) curr.x;
			for(int i = (int) curr.y; i >= (int) dest.y; i--) {
				Vector2 closestRoom = FindClosestRoomOfType(new Vector3(x, i), RoomType.Elevator, "descending");
				if(closestRoom.y != i) {
					closestRoom = FindClosestRoomOfType(new Vector3(x, i), RoomType.Stairwell, "descending");
				}
				if(closestRoom.y != i) {
					//no path
					return new Queue<Vector2>();
				}
				if(x > closestRoom.x) {
					for(int j = x; j >= (int) closestRoom.x; j--){
						path.Enqueue(new Vector2(j, i));
					}
				} else {
					for(int j = x; j <= (int) closestRoom.x; j++){
						path.Enqueue(new Vector2(j, i));
					}
				}
				x = (int) closestRoom.x;
			}
		} else {
			int x = (int) curr.x;
			for(int i = (int) curr.y; i <= (int) dest.y; i--) {
				Vector2 closestRoom = FindClosestRoomOfType(new Vector3(x, i), RoomType.Elevator, "ascending");
				if(closestRoom.y != i) {
					closestRoom = FindClosestRoomOfType(new Vector3(x, i), RoomType.Stairwell, "ascending");
				}
				if(closestRoom.y != i) {
					//no path
					return new Queue<Vector2>();
				}
				if(x > closestRoom.x) {
					for(int j = x; j >= (int) closestRoom.x; j--) {
						path.Enqueue(new Vector2(j, i));
					}
				} else {
					for(int j = x; j <= (int) closestRoom.x; j++) {
						path.Enqueue(new Vector2(j, i));
					}
				}
				x = (int) closestRoom.x;
			}
		}
		return path;
	}

	public Queue<Vector2> GetPath(Vector2 curr, RoomType dest) {
		Vector2 closestDest = FindClosestRoomOfType(curr, dest);
		if(closestDest != new Vector2(-1, -1)){
			return GetPath(curr, closestDest);
		}
		return new Queue<Vector2>();
	}

	private Vector2 FindClosestRoomOfType(Vector2 curr, RoomType dest, string dir = "none") {
		Vector2 closestDest = new Vector2(-1, -1);
		foreach (List<Room> roomValues in rooms) {
			foreach (Room room in roomValues) {
				if(dir == "ascending" && room.y < curr.y) {
					continue;
				}
				if(dir == "descending" && room.y > curr.y) {
					continue;
				}
				if(room.type == dest){
					if(closestDest == new Vector2(-1, -1)){
						closestDest = new Vector2(room.x, room.y);
						continue;
					}
					if(Vector2.Distance(curr, closestDest) > Vector2.Distance(curr, new Vector2(room.x, room.y))){
						closestDest = new Vector2(room.x, room.y);
						if(Vector2.Distance(curr, closestDest) == 1){
							//closest it can be
							return closestDest;
						}
					}
				}
			}
		}
		return closestDest;
	}
}
