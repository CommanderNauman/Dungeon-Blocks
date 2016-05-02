using UnityEngine;
using System.Collections.Generic;

/*

0 = Nothing;
1 = Floor;
2 = Door;
3 = Door with no Floor...?;
4 = Wall...?
5 = Stairs;
6 = Bridge...?;
7 = Ladder Top...?;
8 = Ladder Bottom...?;
9 = Elevators...?;

*/

//***************************************************************************************************

public class Room : MonoBehaviour {

    public bool Visualize;
    public bool Geometry;
    public bool StartRoom;
    public bool DestroyOnStart;
    
    public int TileDimension = 10;

    public FloorEnforcers FloorEnforcer;
    public enum FloorEnforcers
    {
        Default,
        ForceFloor,
        ForceFloorless,
    }

    public List<RoomLevel> RoomData;

    public Vector3 MinDimensions;
    public Vector3 MaxDimensions;
    public Vector3 RoomDimensions;

    public GameObject Number;
    public RoomModels Models;
    [HideInInspector]
    public GameObject EntrancePoint;
    [HideInInspector]
    public GameObject StartHallway;

    //***************************************************************************************************
    // Use this for initialization
    void Start ()
    {
        RoomData = new List<RoomLevel>();
        Generate();
	}

    //***************************************************************************************************

    void Generate()
    {
        CreateRoomData();
        
        CreateDoors();
        CreatePaths();
        CreateFloors();
        CreateStairs();

        if (StartRoom == false) { CreateEntrancePoint(); }
        
        //Create a start point for the player as they enter each room.

        if (Geometry == true) { CreateGeometry(); }
        
        if (Visualize == true) { CreateVisualization(); }

        if (DestroyOnStart == true) { Destroy(gameObject); }
    }

    //***************************************************************************************************

    void CreateRoomData()
    {
        Room_Data data = new Room_Data(RoomData, MinDimensions, MaxDimensions);
        RoomDimensions = data.Generate();
    }

    //***************************************************************************************************

    void CreatePaths()
    {
        Room_Paths Paths = new Room_Paths(RoomData);
        Paths.Generate();
    }

    //***************************************************************************************************

    void CreateVisualization ()
    {
        Room_Visualization Visuals = new Room_Visualization(RoomData, Number, TileDimension);
        Visuals.Visualize();
    }

    //***************************************************************************************************

    void CreateGeometry ()
    {
        Room_Geometry Geometry = new Room_Geometry(
            RoomData,
            Models,
            TileDimension,
            RoomDimensions,
            StartRoom,
            EntrancePoint,
            StartHallway
            );
        Geometry.Generate();
    }

    //***************************************************************************************************

    void CreateFloors()
    {
        Room_Floors Floors = new Room_Floors(RoomData, (int)FloorEnforcer);
        Floors.Generate();
    }

    //***************************************************************************************************

    void CreateDoors()
    {
        Room_Doors Doors = new Room_Doors(RoomData, RoomDimensions);
        Doors.Generate();
    }

    //***************************************************************************************************

    void CreateEntrancePoint()
    {
        Room_Entrance Entrance = new Room_Entrance(RoomData);
        Entrance.Generate();
    }

    //***************************************************************************************************

    void CreateLadders()
    {
        Room_Ladders Ladders = new Room_Ladders(RoomData, Models.Ladders, TileDimension);
        Ladders.Generate();
    }

    //***************************************************************************************************

    void CreateStairs()
    {
        Room_Stairs Stairs = new Room_Stairs(RoomData);
        Stairs.Generate();
    }

    //***************************************************************************************************
    /*
    void CreateWalls()
    {
        Room_Walls Walls = new Room_Walls(RoomData, RoomDimensions, Models.Wall, TileDimension);
        Walls.Generate();
    }
    */
    //***************************************************************************************************
}





// Here we have the data set format for the Room Data
//***************************************************************************************************
[System.Serializable]
public class RoomPosition
{
    public bool StairLock = false;
    public Vector3 Position;
    public int Value = 0;
    public int Rotation = 0;

    public RoomPosition() { }

    public RoomPosition(int x, int y, int z)
    {
        Position = new Vector3(x, y, z);
    }

}

//***************************************************************************************************
[System.Serializable]
public class RoomLevel
{
    public List<RoomPosition> RoomPositions;

    public RoomLevel() { }
    public RoomLevel(List<RoomPosition> roomPositions)
    {
        RoomPositions = roomPositions;
    }
}

//***************************************************************************************************
[System.Serializable]
public class RoomModels
{
    public List<GameObject> Doors;
    public List<GameObject> DoorsNoFloor;
    public List<GameObject> Floors;
    public List<GameObject> Walls;
    public List<GameObject> Stairs;
    public List<GameObject> Ladders;
    public List<GameObject> Elevators;
}

//***************************************************************************************************